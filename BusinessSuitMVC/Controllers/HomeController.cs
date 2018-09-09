using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult Index(ContactTB con)
        {
          
            DBContext DB = new DBContext();
            DB.ContactTBs.Add(con);
            DB.SaveChanges();
            
            return Json("true", JsonRequestBehavior.AllowGet);
        }
    }
}