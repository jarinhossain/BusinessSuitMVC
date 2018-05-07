using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{

    public class CallController : Controller
    {
        private DBContext DB = new DBContext();
        private Numeral_DBContext Num_DB = new Numeral_DBContext();

        [Authorize]
        public ActionResult GenerateCallFile()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            var numberList = DB.Client_List.Select(x => x.Mobile1).ToList();

            foreach (var item in numberList)
            {
                string filename = "source_" + item + "_" + DateTime.Now + ".call";
                FileInfo MyFile = new FileInfo(Server.MapPath("~\\App_Data\\asd.txt"));

            }
            return View();
        }

        [Authorize]
        [HttpGet]
        public string CallMe(string number)
        {
            if (PermissionValidate.validatePermission() == false)
                return "Unauthorized";

            var myNumber = DB.CDR_Instant.Where(x => x.Mobile == number).FirstOrDefault();

            myNumber.Status = 0;
            DB.SaveChanges();

            return myNumber.ToString();
        }

        [Authorize]
        [HttpGet]
        public ActionResult SingleCall()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ///one click test call from client list view
            if (Request.QueryString["number"] != null)
            {
                ViewData["number"] = Request.QueryString["number"].ToString();
                ViewData["remarks"] = "test call ward " + Request.QueryString["remarks"].ToString();
            }

            var isClient = bool.Parse(Session["Is_Client"].ToString());

            if (isClient == true)
            {
                var clientId = int.Parse(Session["Profile_Id"].ToString());
                Client_Inventory clientInventory = DB.Client_Inventory.Where(x => x.Client_Id == clientId).FirstOrDefault();

                ViewData["free_call"] = clientInventory.Free_Call + " remaining call";

            }

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult SingleCall(string number, string remarks)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            if (number == null || number == "" || number.Length != 11)
            {
                ViewData["msg"] = "Please enter a valid number";
                return View();
            }

            bool isDuplicateCall = DB.CDR_Instant.Where(x => x.Mobile == number && x.Status == 0).Any();

            if (isDuplicateCall == true)
            {
                ViewData["msg"] = "Duplicate Call Request";
                return View();
            }

            var isClient = bool.Parse(Session["Is_Client"].ToString());
            CDR_Instant cdr = new CDR_Instant();
            Client_Inventory clientInventory = new Client_Inventory();

            if (isClient == true)
            {
                var clientId = int.Parse(Session["Profile_Id"].ToString());

                clientInventory = DB.Client_Inventory.Where(x => x.Client_Id == clientId).FirstOrDefault();
                ViewData["free_call"] = clientInventory.Free_Call + " remaining call";
                if (clientInventory.Free_Call <= 0)
                {
                    ViewData["msg"] = "You have finished your free calls";
                    return View();
                }

                clientInventory.Call_Sent = clientInventory.Call_Sent + 1;
                clientInventory.Free_Call = clientInventory.Free_Call - 1;
                clientInventory.Updated_By = clientId;
                clientInventory.Updated_On = DateTime.Now;
                cdr.Client_Id = clientId;
                ViewData["free_call"] = clientInventory.Free_Call + " remaining call";
            }



            cdr.Mobile = number;
            cdr.Remarks = remarks;
            cdr.Status = 0;
            cdr.Created_By = int.Parse(Session["Login_Id"].ToString());
            DB.CDR_Instant.Add(cdr);

            DB.SaveChanges();

            ViewData["number"] = null;
            ViewData["msg"] = "Call Placed. Please Wait for the call";

            return View();
        }

        [Authorize]
        public ActionResult SingleCallList()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            int loginID = int.Parse(Session["Login_Id"].ToString());
            int roleID = int.Parse(Session["Role_Id"].ToString());
            var instant = new List<CDR_Instant>();

            if (roleID >= 5)
                instant = DB.CDR_Instant.Where(x => x.Created_By == loginID).OrderByDescending(x => x.Created_On).ToList();
            else
                instant = DB.CDR_Instant.OrderByDescending(x => x.Created_On).ToList();

            return View(instant);
        }


        [Authorize,HttpGet]
        public ActionResult ProceedOBDBulk(int id)///order id
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Order order = DB.Orders.Find(id);

            if(order.Order_Status != 0)
            {
                ViewData["msg"] = "Already proceeded";
                return RedirectToAction("Search", "Order");
            }

            Online_Order_Details onlineOrder = order.Online_Order_Details.FirstOrDefault();

            Obd_Ward_Details wardDetails = new Obd_Ward_Details();

            Obd_Bulk obdBulk = new Obd_Bulk();
            obdBulk.Client_Id = order.Client_Id;
            obdBulk.Order_Id = order.Id;
            obdBulk.Created_By = int.Parse(Session["Login_Id"].ToString());
            obdBulk.Status = 0;
            obdBulk.Is_Active = true;
            obdBulk.Play_File = "";
            obdBulk.Total_Calls = onlineOrder.Estimated_Reach_Ordered;

            wardDetails.Client_Id = order.Client_Id;
            wardDetails.Quantity = onlineOrder.Estimated_Reach_Ordered;///need to change in future
            wardDetails.Ward = onlineOrder.Ward;
            wardDetails.Obd_Bulk_Id = obdBulk.Id;

            order.Order_Status = 1;/// order proceed
            onlineOrder.Status = 1;

            DB.Obd_Ward_Details.Add(wardDetails);
            DB.Obd_Bulk.Add(obdBulk);
            DB.SaveChanges();
            return Redirect("/Order/Search");
        }

        [Authorize]
        public ActionResult ObdRequestList(int? id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            int loginID = int.Parse(Session["Login_Id"].ToString());
            int roleID = int.Parse(Session["Role_Id"].ToString());
            var obdRequest = new List<Obd_Request>();


            obdRequest = DB.Obd_Request.OrderByDescending(x => x.Created_On).ToList();
            
            return View(obdRequest);
        }

        [Authorize,HttpGet]
        public ActionResult IncomingCalls()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            int loginID = int.Parse(Session["Login_Id"].ToString());
            int roleID = int.Parse(Session["Role_Id"].ToString());
            var incomingCalls = new List<Incoming_Calls>();


            incomingCalls = DB.Incoming_Calls.OrderByDescending(x => x.Created_On).ToList();

            return View(incomingCalls);
        }

        [HttpGet]
        public JsonResult fetchdata()
        {

            var numberList = DB.CDR_Instant.Where(x => x.Status == 0).Select(x => new { Id = x.Id, Mobile = x.Mobile }).ToList();

            foreach (var item in numberList)
            {
                var cdr = DB.CDR_Instant.Find(item.Id);

                cdr.Status = 1;///fetched

            }
            DB.SaveChanges();

            return Json(numberList, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult fetchdatanew()
        {
            var obdBulk = DB.Obd_Bulk.Where(x => x.Is_Active == true && x.Status == 0).FirstOrDefault();
            if (obdBulk != null)
            {
                var obdWardDetails = obdBulk.Obd_Ward_Details.FirstOrDefault();///need to change in future

                var sourceList = Num_DB.Sources.Where(x => x.Ward == obdWardDetails.Ward).OrderBy(x => x.Id).Select(x => x.Id).ToList();
                var numberList = Num_DB.Numbers.Where(x => sourceList.Any(y => y == x.Source_Id))
                                                .OrderBy(x => x.Source_Id)
                                                .Select(x => new { Id = x.Id, Mobile = "0" + x.Number1, Source_Id = x.Source_Id })
                                                .ToList();

                int count = 0;
                foreach (var item in numberList)
                {
                    if (DB.Obd_Request.Where(x => x.Mobile == item.Mobile).Any() == false)
                    {
                        DB.Obd_Request.Add(new Obd_Request() { Mobile = item.Mobile, Obd_Bulk_Id = obdBulk.Id, Source_Id = item.Source_Id, Status = 0, Retry_Count = 0 });
                        count++;
                        if (count >= 3)
                            break;
                    }
                }

                //var numberList = new[] {
                //    new { Id = "1", Mobile = "01676797123", PlayFile = "filename.gsm", Context = "obd-call", Retry = "0"},
                //    //new { Id = "2", Mobile = "01878196799" }
                //};

                DB.SaveChanges();
            }

            var finalNumberList = (from request in DB.Obd_Request
                        join bulk in DB.Obd_Bulk on request.Obd_Bulk_Id equals bulk.Id
                        where request.Status == 0 && bulk.Is_Active == true
                        select new { Id = request.Id, Mobile = request.Mobile, Source_Id = request.Source_Id }).ToList();
            
            //var finalNumberList = DB.Obd_Request.Where(x => x.Status == 0).Where(x => x.Obd_Bulk.Is_Active == true)
            //                           .Select(x => new { Id = x.Id, Mobile = x.Mobile, Source_Id = x.Source_Id })
            //                           .ToList();

            //var result = DB.Obd_Request.Where(x => !finalNumberList.Any(y => y.Mobile == x.Mobile));
            foreach (var item in finalNumberList)
            {
                var obdRequest = DB.Obd_Request.Find(item.Id);

                obdRequest.Status = 1;///fetched

            }
            DB.SaveChanges();

            return Json(finalNumberList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string cdrUpdate()
        {
            int obd_request_id = int.Parse(Request.Form["obd_request_id"]);
            string call_unique_id = Request.Form["call_unique_id"];
            int call_duration = int.Parse(Request.Form["call_duration"]);
            int billsec = int.Parse(Request.Form["billsec"]);
            string start_time = Request.Form["start_time"];
            string answer_time = Request.Form["answer_time"];
            string end_time = Request.Form["end_time"];
            string clid = Request.Form["clid"];
            string disposition = Request.Form["disposition"];
            string context = Request.Form["context"];
            string lastapp = Request.Form["lastapp"];
            string server = Request.Form["server"];
            //string last_transmission_time = Request.Form["last_transmission_time"];
            //string amaflags = Request.Form["amaflags"];
            //string src = Request.Form["src"];
            //string dst = Request.Form["dst"];
            //string lastdata = Request.Form["lastdata"];

            //DateTime startTime = DateTime.Now;
            //DateTime endTime = DateTime.Now;
            //DateTime answerTime = DateTime.Now;

            //DateTime.TryParse(start_time, out startTime);
            //DateTime.TryParse(end_time, out endTime);
            //DateTime.TryParse(answer_time, out answerTime);

            Obd_Request obdRequest = DB.Obd_Request.Find(obd_request_id);

            obdRequest.Unique_Id = call_unique_id;
            obdRequest.Bill_Sec = billsec;
            //obdRequest.Start_Time = startTime;
            //obdRequest.End_Time = endTime;
            //obdRequest.Answer_Time = answerTime;
            obdRequest.Disposition = disposition;
            obdRequest.Context = context;
            obdRequest.Duration = call_duration;
            obdRequest.Last_App = lastapp;
            obdRequest.Server = server;

            if (disposition == "ANSWERED" || obdRequest.Retry_Count == 1)
            {
                obdRequest.Status = 2;
            }
            else if (disposition != "ANSWERED" && obdRequest.Retry_Count != 1)
            {
                obdRequest.Retry_Count = 1;
                obdRequest.Status = 0;///for re-fetch data to 
                obdRequest.Retry_Schedule = DateTime.Now.AddMinutes(5);
            }

            DB.SaveChanges();

            return "successful-" + answer_time + " " + obdRequest.Mobile;

        }

        [HttpPost]
        public string cdrInstantUpdate()
        {
            int cdr_instant_id = int.Parse(Request.Form["cdr_instant_id"]);
            int retry_count = int.Parse(Request.Form["retry_count"]);
            string call_unique_id = Request.Form["call_unique_id"];
            int call_duration = int.Parse(Request.Form["call_duration"]);
            int billsec = int.Parse(Request.Form["billsec"]);
            string last_transmission_time = Request.Form["last_transmission_time"];
            string start_time = Request.Form["start_time"];
            string answer_time = Request.Form["answer_time"];
            string end_time = Request.Form["end_time"];
            string clid = Request.Form["clid"];
            string src = Request.Form["src"];
            string dst = Request.Form["dst"];
            string disposition = Request.Form["disposition"];
            string amaflags = Request.Form["amaflags"];
            string context = Request.Form["context"];
            string lastapp = Request.Form["lastapp"];
            string lastdata = Request.Form["lastdata"];

            CDR_Instant cdrInstant = DB.CDR_Instant.Find(cdr_instant_id);

            cdrInstant.Disposition = disposition;
            cdrInstant.Duration = call_duration;
            cdrInstant.Bill_Sec = billsec;
            cdrInstant.Context = context;
            cdrInstant.Last_App = lastapp;
            cdrInstant.Start_Time = DateTime.Parse(start_time);
            cdrInstant.Status = 2;

            DB.SaveChanges();

            return "successful-" + cdrInstant.Mobile;
        }

        [HttpPost]
        public string saveIncomingCall()
        {
            Incoming_Calls incoming = new Incoming_Calls();
            //try
            //{
                incoming.Mobile = Request.Form["src"];
                incoming.Context = Request.Form["context"];
            incoming.Duration = int.Parse(Request.Form["call_duration"]);
                incoming.Bill_Sec = int.Parse(Request.Form["billsec"]);
            incoming.Disposition = Request.Form["disposition"];
            incoming.Server = Request.Form["server"];
                incoming.Call_Unique_Id = Request.Form["call_unique_id"];
            incoming.Answer_Time = DateTime.Parse(Request.Form["answer_time"]);

            DB.Incoming_Calls.Add(incoming);
                DB.SaveChanges();
                return "successful incoming - " + incoming.Mobile;
            //}
            //catch(Exception ex)
            //{
            //    return "failed - " + ex.Message;
            //}


        }
        [HttpGet]
        public ActionResult AddPlayFile()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddPlayFile(String filename)
        {
            HttpPostedFileBase file = null;
           
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension != ".mp3" && extension != ".wav")
                {
                    ViewData["msg"] = "Failed to Save User Information! Allowed image format is .mp3 and .wav";
                    return View();
                }
            }


            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                string path = Path.Combine(Server.MapPath("~/Music"), "" + filename+extension);
                file.SaveAs(path);/// file save
            }
            return View();
        }
    }
}