using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    [Authorize]
    public class ContactUsController : Controller
    {
        // GET: ContactUs
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ContactUsSearch()
        {


            DBContext DB = new DBContext();

            List<ContactTB> expense = (from client in DB.ContactTBs
                                     select client).ToList();

            return View(expense);
        }
        [HttpGet]
        public ActionResult ContactDetails(int id)
        {
            //if (PermissionValidate.validatePermission() == false)
            //    return View("Unauthorized");

            DBContext DB = new DBContext();
            ContactTB client = (from u in DB.ContactTBs
                                  where u.Id == id
                                  select u).FirstOrDefault();

            return View(client);
        }
    }
}