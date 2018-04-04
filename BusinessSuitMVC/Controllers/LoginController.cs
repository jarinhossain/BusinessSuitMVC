using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

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
                ViewData["msg"] = "please enter your valid Username";
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
                    FormsAuthentication.SetAuthCookie(search.UserName, false);
                    return RedirectToAction("Create", "User");
                }

            }
            return View();
        }
        [HttpGet]
        public ActionResult Logout()
        {
            Session.Clear();
            FormsAuthentication.SignOut();
            return Redirect("/User/Dashboard");
        }

        [HttpGet]
        public ActionResult ChangePassword()
        {
            //User_Login userLogin  = (from login in DB.User_Login
            //                     where login.Password == userLogin.Password
            //                     && login.Id == userLogin.Id
            //                     select login).FirstOrDefault();
            return View();
        }
        [HttpGet]
        public ActionResult newLogin()
        {
            return View();
        }
        [HttpPost]
        public ActionResult newLogin(User_Login model)
        {
            if (model.UserName == null)
            {
                ViewData["msg"] = "please enter your valid Username";
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

    }
}