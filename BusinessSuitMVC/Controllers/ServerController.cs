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
    public class ServerController : Controller
    {
        // GET: ServerCreate
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ServerCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            return View();
        }
        [HttpPost]
        public ActionResult ServerCreate(ServerTB server)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            db.ServerTBs.Add(server);
            db.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
            return View();

        }
        [HttpGet]
        public ActionResult ServerEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();
            ServerTB expens = (from user in DB.ServerTBs
                                where user.Id == id
                                    select user).FirstOrDefault();

            return View(expens);
        }

        [HttpPost]
        public ActionResult ServerEdit(ServerTB expens)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();
            ServerTB expense = (from user in DB.ServerTBs
                                    where user.Id == expens.Id
                                    select user).FirstOrDefault();

            expense.Name = expens.Name;
            expense.Local_IP = expens.Local_IP;
            expense.Real_IP = expens.Real_IP;
            expense.Description = expens.Description;
            DB.SaveChanges();
            ViewData["msg"] = "Successfully Updated";

            return View(expens);
        }
        [HttpGet]
        public ActionResult ServerLogSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();

            List<Server_log> expense = (from client in DB.Server_log
                                             select client).ToList();

            return View(expense);
        }

    }
}