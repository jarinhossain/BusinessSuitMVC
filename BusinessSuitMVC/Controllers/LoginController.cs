using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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
        public ActionResult PermissionSearch()
        {

            DBContext DB = new DBContext();
            List<Permission> permi = (from per in DB.Permissions
                                           select per).ToList();

            
            return View(permi);
        }

        [HttpGet]
        public ActionResult PermissionDetails(int id)
        {

            DBContext DB = new DBContext();
            Permission per = (from pe in DB.Permissions
                                 where pe.Id == id
                                 select pe).FirstOrDefault();

            return View(per);
        }

        [HttpGet]
        public ActionResult PermissionCreate()
        {
            ViewData["ModuleList"] = loadmodule();
            
            return View();
        }

        [HttpPost]
        public ActionResult PermissionCreate(Permission per)
        {
            String validation = validationCreate(per);
            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                DB.Permissions.Add(per);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            ViewData["ModuleList"] = loadmodule();
            return View();
        }

        [HttpGet]
        public ActionResult PermissionEdit(int id)
        {
            ViewData["ModuleList"] = loadmodule();
            DBContext DB = new DBContext();
            Permission permiss = (from per in DB.Permissions
                                  where per.Id == id
                                  select per).FirstOrDefault();
            return View(permiss);
            
        }
       
        [HttpPost]
        public ActionResult PermissionEdit(Permission per)
        {
            ViewData["ModuleList"] = loadmodule();
            string validation = validationCreate(per);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                Permission permisn = (from pe in DB.Permissions
                                      where pe.Id == per.Id
                                      select pe).FirstOrDefault();

                permisn.Name = per.Name;
                permisn.Display_Name = per.Display_Name;
                permisn.Description = per.Description;
                permisn.Module = per.Module;
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View(per);
        }

        [HttpGet]
        public ActionResult RoleCreate()
        {

            return View();
        }

        [HttpPost]
        public ActionResult RoleCreate(Role rol)
        {
            String validation = validationRoleCreate(rol);
            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                DB.Roles.Add(rol);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }

            return View();
        }

        [HttpGet]
        public ActionResult RoleEdit(int id)
        {
            
            DBContext DB = new DBContext();
            Role rol = (from ro in DB.Roles
                                  where ro.Id == id
                                  select ro).FirstOrDefault();
            

            ViewData["existingPermission"] = DB.Permission_Role.Where(x =>);
            ViewData["permissions"] = DB.Permissions.Include("Module").ToList();
            return View(rol);
        }

        [HttpPost]
        public ActionResult RoleEdit(Role role)
        {
            string validation = validationRoleCreate(role);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                Role roll = (from ro in DB.Roles
                                      where ro.Id == role.Id
                                      select ro).FirstOrDefault();

                roll.Name = role.Name;
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View(role);
        }

        [HttpGet]
        public ActionResult RoleSearch()
        {
            DBContext DB = new DBContext();
            return View(DB.Roles.ToList());
        }

        public List<SelectListItem> loadmodule()
        {
            DBContext DB = new DBContext();
            List<Models.Module> type = (from div in DB.Modules
                                 select div).ToList();
            List<SelectListItem> moduleDropdown = new List<SelectListItem>();
            foreach (var item in type)
            {
                moduleDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return moduleDropdown;
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
        public String validationCreate(Permission perm)
        {
            if (perm.Name == null)
            {
                return "Please enter your valid Name";
            }
            else if (perm.Display_Name == null)
            {
                return "Please enter your valid Display Name";
            }
            else if (perm.Module == null)
            {
                return "Please enter your valid Module";
            }
            return "true";
        }
        public String validationRoleCreate(Role ro)
        {
            if (ro.Name == null)
            {
                return "Please enter your valid Name";
            }
           
            return "true";
        }
    }
}