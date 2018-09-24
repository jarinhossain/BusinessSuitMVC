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
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ModuleCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            return View();
        }
        [HttpPost]
        public ActionResult ModuleCreate(Module server)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            db.Modules.Add(server);
            db.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
            return View();

        }
        [HttpGet]
        public ActionResult ModuleEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            Module account = (from ac in db.Modules
                                       where ac.Id == id
                                       select ac).FirstOrDefault();


            return View(account);
        }
        [HttpPost]
        public ActionResult ModuleEdit(Module account)
        {

            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            Module accoun = (from ac in db.Modules
                                      where ac.Id == account.Id
                                      select ac).FirstOrDefault();

            accoun.Name = account.Name;

            db.SaveChanges();

            ViewData["msg"] = "Successfully Updated";
            return View(account);
        }
        [HttpGet]
        public ActionResult ModuleSearch()
        {

            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();

            List<Module> expense = (from client in DB.Modules
                                             select client).ToList();

            return View(expense);
        }
    }
}