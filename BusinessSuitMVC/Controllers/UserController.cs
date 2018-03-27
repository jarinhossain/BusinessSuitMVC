using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class UserController : Controller
    {
        public ActionResult LayoutTest()
        {
            return View();
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {
            //List<SelectListItem> role = new List<SelectListItem>();
            //role.Add(new SelectListItem() { Value = "1", Text = "Super Admin" });
            //role.Add(new SelectListItem() { Value = "2", Text = "Admin" });
            //role.Add(new SelectListItem() { Value = "3", Text = "Moderator" });
            //role.Add(new SelectListItem() { Value = "4", Text = "User" });
            //role.Add(new SelectListItem() { Value = "5", Text = "Guest" });
            //ViewData["role"] = role;
            return View();
        }
        [HttpPost]
        public ActionResult Create(User_Profile User, string UserName, string Password, string ConfirmPassword, int? Role)
        {

            if (User.Name == null)
            {
                ViewData["msg"] = "please enter your valid Name";
            }
            else if (User.Gender == null)
            {
                ViewData["msg"] = "please enter your valid Gender";
            }
            else if (User.Ward == null)
            {
                ViewData["msg"] = "please enter your valid Ward";
            }
            else if (User.User_Type == null)
            {
                ViewData["msg"] = "please enter your valid User_Type";
            }
            else if (User.City == null)
            {
                ViewData["msg"] = "please enter your valid City";
            }
            else if (UserName == "")
            {
                ViewData["msg"] = "please enter your valid UserName";
            }
            else if (Password == "")
            {
                ViewData["msg"] = "please enter your valid Password";
            }
            else if (ConfirmPassword != Password)
            {
                ViewData["msg"] = "please enter your valid Confirm Password";
            }
            else if (User.Mobile == null)
            {
                ViewData["msg"] = "please enter your valid Mobile";
            }
            else if (Role == null)
            {
                ViewData["msg"] = "please enter your valid Role";
            }
            else if (User.Address == null)
            {
                ViewData["msg"] = "please enter your valid Address";
            }
           
            //else if (User.Email == null)
            //{
            //    ViewData["msg"] = "please enter your valid Email";
            //}
          
            else
            {
                DBContext DB = new DBContext();

                DB.User_Profile.Add(User);
                DB.SaveChanges();

                User_Login login = new User_Login();
                login.UserName = UserName;
                login.Password = Password;
                login.Role = Role;

                DB.User_Login.Add(login);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
               
            }
            //List<SelectListItem> role = new List<SelectListItem>();
            //role.Add(new SelectListItem() { Value = "1", Text = "Super Admin" });
            //role.Add(new SelectListItem() { Value = "2", Text = "Admin" });
            //role.Add(new SelectListItem() { Value = "3", Text = "Moderator" });
            //role.Add(new SelectListItem() { Value = "4", Text = "User" });
            //role.Add(new SelectListItem() { Value = "5", Text = "Guest" });
            //ViewData["role"] = role;
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int userId)
        {
            DBContext DB = new DBContext();

            User_Profile profile = (from user in DB.User_Profile
                                    where user.Id == userId
                                    select user).FirstOrDefault();

            User_Login user_login = (from user in DB.User_Login
                                     where user.User_Profile_Id == profile.Id
                                     select user).FirstOrDefault();

            ViewData["user_name"] = user_login.UserName;
            ViewData["Role"] = user_login.Role;

            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(User_Profile profile, string UserName, int? Role)
        {
            DBContext DB = new DBContext();
            User_Profile DBProfile = (from user in DB.User_Profile
                                      where user.Id == profile.Id
                                      select user).FirstOrDefault();

            DBProfile.Name = profile.Name;
            DBProfile.Mobile = profile.Mobile;
            DBProfile.Phone = profile.Phone;
            DBProfile.Address = profile.Address;
            DBProfile.Email = profile.Email;

            DB.SaveChanges();

            User_Login DBUserLogin = (from user in DB.User_Login
                                      where user.User_Profile_Id == profile.Id
                                      select user).FirstOrDefault();

            DBUserLogin.UserName = UserName;
            DBUserLogin.Role = Role;

            return View();
        }
        [HttpGet]
        public ActionResult Search()
        {

            DBContext DB = new DBContext();
            List<User_Profile> userlist = (from user in DB.User_Profile
                                           select user).ToList();
         

            return View(userlist);
        }

        [HttpGet]
        public ActionResult Show(int id)
        {

            DBContext DB = new DBContext();
            User_Profile user = (from u in DB.User_Profile
                                           where u.Id == id
                                           select u).FirstOrDefault();


            return View(user);
        }

    }
}