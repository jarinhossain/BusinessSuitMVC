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
    }
}