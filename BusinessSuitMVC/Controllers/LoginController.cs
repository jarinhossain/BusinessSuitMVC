using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(User_Login model)
        {
            if (model.UserName == null)
            {
                ViewData["msg"] = "please enter your valid UserName";
            }
            else if (model.Password == null)
            {
                ViewData["msg"] = "please enter your valid  Password";

            }
            else
            {
                DBContext DB = new DBContext();

                User_Login search = (from user in DB.User_Login
                                     where user.UserName == model.UserName
                                     && user.Password == model.Password
                                     select user).FirstOrDefault();

                if (search != null)
                {
                    return RedirectToAction("Create", "User");
                }

            }
            return View();
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
          
            return View();
        }
        [HttpPost]
        public ActionResult ChangePassword(User_Login userLogin)
        {
            DBContext DB = new DBContext();
            User_Login insert = (from login in DB.User_Login
                                 where login.Password == userLogin.Password
                                 && login.Id == userLogin.Id
                                 select login).FirstOrDefault();


            DB.SaveChanges();

            return View();
        }
    }
}