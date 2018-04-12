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
        // GET: Call
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GenerateCallFile()
        {
            DBContext DB = new DBContext();

            var numberList = DB.Client_List.Select(x => x.Mobile1).ToList();

            foreach (var item in numberList)
            {
                string filename = "source_" + item + "_" + DateTime.Now + ".call";
                FileInfo MyFile = new FileInfo(Server.MapPath("~\\App_Data\\asd.txt"));

            }
            return View();
        }

        [HttpGet]
        public string CallMe(string number)
        {
            DBContext DB = new DBContext();
            var myNumber = DB.CDR_Instant.Where(x => x.Mobile == number).FirstOrDefault();

            myNumber.Status = 0;
            DB.SaveChanges();

            return myNumber.ToString();
        }

        [HttpGet]
        public ActionResult SingleCall()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SingleCall(string number, string remarks)
        {
            if(number == null)
            {
                ViewData["msg"] = "Please enter a valid number";
                return View();
            }

            DBContext DB = new DBContext();

            CDR_Instant cdr = new CDR_Instant();

            cdr.Mobile = number;
            cdr.Remarks = remarks;
            cdr.Status = 0;

            DB.CDR_Instant.Add(cdr);

            DB.SaveChanges();

            ViewData["number"] = null;
            ViewData["msg"] = "Call Placed. Please Wait for the call";

            return View();
        }

        public ActionResult SingleCallList()
        {
            DBContext DB = new DBContext();
            return View(DB.CDR_Instant.OrderByDescending(x => x.Created_On).ToList());
        }

        [HttpGet]
        public JsonResult fetchdata()
        {
            DBContext DB = new DBContext();

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