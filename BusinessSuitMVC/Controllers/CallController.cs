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

            if(isDuplicateCall == true)
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

            var numberList = DB.CDR_Instant.Where(x => x.Status == 0).Select(x => x.Mobile).ToList();

            foreach (var item in numberList)
            {
                var cdr = DB.CDR_Instant.Where(x => x.Status == 0).FirstOrDefault();
                
                cdr.Status = 1;//fetched

            }
            DB.SaveChanges();
            
            return Json(numberList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult fetchdatanew()
        {

            var numberList = new[] {
                new { Id = "1", Mobile = "01676797123" },
                new { Id = "2", Mobile = "01624156585" },
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
        public void cdrUpdate(FormCollection collection)
        {

            CDR_Obd cdr = new CDR_Obd();

            cdr.Context = collection["context"];

            DB.CDR_Obd.Add(cdr);
            DB.SaveChanges();
            
        }
    }
}