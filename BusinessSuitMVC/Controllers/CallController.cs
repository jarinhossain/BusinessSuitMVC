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

            bool hasDuplicateCall = DB.CDR_Instant.Where(x => x.Mobile == number && x.Status == 0).Any();

            if(hasDuplicateCall == true)
            {
                ViewData["msg"] = "Duplicate Call Request";
                return View();
            }

            CDR_Instant cdr = new CDR_Instant();

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
    }
}