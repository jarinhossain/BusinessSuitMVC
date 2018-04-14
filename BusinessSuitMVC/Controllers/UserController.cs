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
    [Authorize]
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
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            ViewData["roleList"] = loadRoleDropdown();
            ViewData["CityList"] = new SourceController().loadDistrictDropdown();
            return View();
        }
        [HttpPost]
        public ActionResult Create(User_Profile User, string UserName, string Password, string ConfirmPassword, int? roleId)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            User_Login login = new User_Login();

            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension != ".jpg")
                {
                    ViewData["msg"] = "Failed to Save User Information! Allowed image format is .jpg";
                    return View();
                }
            }

            string validation = ValidationUser(User, UserName, Password, ConfirmPassword, roleId);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }

            //else if (User.Email == null)
            //{
            //    ViewData["msg"] = "please enter your valid Email";
            //}

            else
            {
                DBContext DB = new DBContext();

                User.Image = file != null && file.ContentLength > 0 ? true : false;
                DB.User_Profile.Add(User);
                DB.SaveChanges();

                login.UserName = UserName;
                login.User_Profile_Id = User.Id;
                login.Password = PasswordEncryption.GetSHA1HashData(Password);
                login.Role_Id = roleId;

                DB.User_Login.Add(login);
                DB.SaveChanges();

                if(file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/User"), "U_" + User.Id + extension);
                    file.SaveAs(path);/// file save
                }

                ViewData["msg"] = "Successfully Saved";

            }
            ViewData["roleList"] = loadRoleDropdown();
            ViewData["CityList"] = new SourceController().loadDistrictDropdown();
            return View();
        }
        public String ValidationUser(User_Profile User,String UserName,String Password,String ConfirmPassword, int? Role)
        {
            if (User.Name == null)
            {
                return "please enter your valid Name";
            }
            else if (User.Gender == null)
            {
                return "please enter your valid Gender";
            }
            else if (User.Mobile == null)
            {
                return "please enter your valid mobile";
            }
            else if (User.Mobile.Length != 11)
            {
                return "Mobile number should be 11 digit";
            }
            else if (User.User_Type == null)
            {
                return "please enter your valid User_Type";
            }
            else if (Role == null)
            {
                return "please enter your valid Role";
            }
            else if (User.Ward == null)
            {
                return "please enter your valid Ward";
            }
          
            else if (User.City == null)
            {
                return "please enter your valid City";
            }
            else if (User.Address == null)
            {
                return "please enter your valid Address";
            }
            else if (UserName == "")
            {
                return "please enter your valid UserName";
            }
            else if (Password == "")
            {
                return "please enter your valid Password";
            }
            else if (ConfirmPassword != Password)
            {
                return "ConfirmPassword doesn't match password";
            }
            //else if (User.Mobile == null)
            //{
            //    return "please enter your valid Mobile";
            //}
           
            return "true";
        }

        [HttpGet]
        public ActionResult Edit(int userId)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            
            DBContext DB = new DBContext();

            User_Profile profile = (from user in DB.User_Profile
                                    where user.Id == userId
                                    select user).FirstOrDefault();

            User_Login user_login = (from user in DB.User_Login
                                     where user.User_Profile_Id == profile.Id
                                     select user).FirstOrDefault();

            ViewData["roleId"] = user_login.Role_Id;
            ViewData["roleList"] = loadRoleDropdown();
            ViewData["CityList"] = new SourceController().loadDistrictDropdown();

            return View(profile);
        }

        [HttpPost]
        public ActionResult Edit(User_Profile profile, int? roleId)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension != ".jpg")
                {
                    ViewData["msg"] = "Failed to Save User Information! Allowed image format is .jpg";
                    return View();
                }
            }
            string validation = ValidationUser(profile,"hi", "hi", "hi",  roleId);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
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
                DBProfile.City = profile.City;
                DBProfile.Image = file != null && file.ContentLength > 0 ? true : profile.Image;

                DB.SaveChanges();

                User_Login DBUserLogin = (from user in DB.User_Login
                                          where user.User_Profile_Id == profile.Id
                                          select user).FirstOrDefault();

              //  DBUserLogin.UserName = UserName;
                DBUserLogin.Role_Id = roleId;
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/User"), "U_" + profile.Id + extension);
                    file.SaveAs(path);/// file save
                }

                ViewData["msg"] = "Successfully Updated";
                ViewData["roleId"] = DBUserLogin.Role_Id;
            }

            
            ViewData["roleList"] = loadRoleDropdown();
            ViewData["CityList"] = new SourceController().loadDistrictDropdown();
            return View(profile);
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            List<User_Profile> userlist = (from user in DB.User_Profile
                                           select user).ToList();
         

            return View(userlist);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            User_Profile user = (from u in DB.User_Profile
                                           where u.Id == id
                                           select u).FirstOrDefault();
      
            return View(user);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            return View();
        }

        public List<SelectListItem> loadRoleDropdown()
        {
            DBContext DB = new DBContext();
            List<Role> role = (from user in DB.Roles
                                       select user).ToList();

            List<SelectListItem> roleDropdown = new List<SelectListItem>();

            foreach (var item in role)
            {

                roleDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return roleDropdown;
        }
    }
}