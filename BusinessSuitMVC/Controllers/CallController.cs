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
            cdr.Created_By = int.Parse(Session["Profile_Id"].ToString());
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

            return View(DB.CDR_Instant.OrderByDescending(x => x.Created_On).ToList());
        }

        [HttpGet]
        public JsonResult fetchdata()
        {

            var numberList = DB.CDR_Instant.Where(x => x.Status == 0).Select(x => new {Id = x.Id, Mobile = x.Mobile }).ToList();

            foreach (var item in numberList)
            {
                var cdr = DB.CDR_Instant.Find(item.Id);

                cdr.Status = 1;///fetched

            }
            DB.SaveChanges();

            return Json(numberList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult fetchdatanew()
        {

            var numberList = new[] {
                new { Id = "1", Mobile = "01676797123" },
                //new { Id = "2", Mobile = "01878196799" }
            };

            //foreach (var item in numberList)
            //{
            //    var cdr = DB.CDR_Instant.Where(x => x.Status == 0).FirstOrDefault();

            //    cdr.Status = 1;//fetched

            //}
            //DB.SaveChanges();

            return Json(numberList, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public string cdrUpdate()
        {
            try
            {
                var keys = Request.Form.AllKeys;
                var count = Request.Form.AllKeys.Count();
                string content = Request.Form["dbcontext"];
                //using (var reader = new StreamReader(Request.InputStream))
                //    content = reader.ReadToEnd();
                return content + "       " + count + "       " + keys;
                //string context = Request.QueryString["dbcontext"].ToString();
                //string disposition = Request.QueryString["disposition"].ToString();

                //if (context != "" || context != null)
                //    return context;
                //else if (disposition != "" || disposition != null)
                //    return disposition;
                //else
                //    return "no data";
            }
            catch
            {
                return "error cached";
            }

            //CDR_Obd cdr = new CDR_Obd();

            //cdr.Context = context;
            //cdr.Disposition = disposition;

            //DB.CDR_Obd.Add(cdr);
            //DB.SaveChanges();
            //return "";

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
            cdrInstant.Status = 2;

            DB.SaveChanges();

            return "successful";
        }
    } 
}