using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class ModuleController : Controller
    {
        // GET: Module
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            
            return View();
        }
        [HttpPost]
        public JsonResult Create(string Name)
        {
            DBContext DB = new DBContext();
            Module mod = new Module();
            mod.Name = Name;
            DB.Modules.Add(mod);
            DB.SaveChanges();
           // ViewData["msg"] = "Successfully Saved";
            return Json("true",JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DBContext DB = new DBContext();
            Module module = (from modul in DB.Modules
                             where modul.Id == id
                             select modul).FirstOrDefault();


            return View(module);
        }
        public JsonResult Edit(Module mod)
        {
            DBContext DB = new DBContext();
            Module module = (from modul in DB.Modules
                               where modul.Id == mod.Id
                               select modul).FirstOrDefault();

            module.Name = mod.Name;
            DB.SaveChanges();
           // ViewData["msg"] = "Successfully Updated";
            return Json("true",JsonRequestBehavior.AllowGet);
        }
    }
}