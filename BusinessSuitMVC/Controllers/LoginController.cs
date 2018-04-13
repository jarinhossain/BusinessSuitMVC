using BusinessSuitMVC.ModelClasses;
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
        private DBContext DB = new DBContext();
        // GET: Login
        //[HttpGet]
        //public ActionResult Index()
        //{
        //    return View();
        //}

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
        public ActionResult Welcome()
        {
            return View();
        }

        [Authorize]
        [HttpGet]
        public ActionResult PermissionSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            var permi = DB.Permissions.Include("Module").OrderBy(x => x.Module_Id).ToList();

            //(from per in DB.Permissions.Include("Module")
            // select per);
            return View(permi);
        }

        [Authorize]
        [HttpGet]
        public ActionResult PermissionDetails(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            Permission per = (from pe in DB.Permissions
                                 where pe.Id == id
                                 select pe).FirstOrDefault();

            return View(per);
        }

        [Authorize]
        [HttpGet]
        public ActionResult PermissionCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["ModuleList"] = loadmodule();
            
            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult PermissionCreate(Permission per)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

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

        [Authorize]
        [HttpGet]
        public ActionResult PermissionEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["ModuleList"] = loadmodule();
            DBContext DB = new DBContext();
            Permission permiss = (from per in DB.Permissions
                                  where per.Id == id
                                  select per).FirstOrDefault();
            return View(permiss);
            
        }
       
        [Authorize]
        [HttpPost]
        public ActionResult PermissionEdit(Permission per)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

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
                permisn.Module_Id = per.Module_Id;
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View(per);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RoleCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            return View();
        }

        [Authorize]
        [HttpPost]
        public ActionResult RoleCreate(Role rol)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

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

        [Authorize]
        [HttpGet]
        public ActionResult RoleEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            //int profileID = 1;// (int)Session["profile_id"];
            DBContext DB = new DBContext();
            Role rol = (from ro in DB.Roles
                                  where ro.Id == id
                                  select ro).FirstOrDefault();


            ViewBag.existingPermission = getAllPermissionsByRoleId(id);
            ViewData["permissionsList"] = getAllPermissions();
            return View(rol);
        }

        [Authorize]
        [HttpPost]
        public ActionResult RoleEdit(Role role)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            string validation = validationRoleCreate(role);
            string[] permissions = Request.Form.GetValues("permission");
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

                var existingPermissionIds = getAllPermissionsByRoleId(role.Id).Select(x => x.Permission_Id).ToList();

                foreach (var item in permissions)
                {
                    int per = int.Parse(item);
                    if (existingPermissionIds.Contains(per) == false)
                    {
                        roll.Permission_Role.Add(new Permission_Role() { Permission_Id = per }); //adding new permission to role
                    }
                }

                foreach (var item in existingPermissionIds)
                {
                    if (permissions.Contains(item.ToString()) == false)
                    {
                        var permissionToRemove = DB.Permission_Role.Where(x => x.Permission_Id == item).FirstOrDefault();
                        //roll.Permission_Role.Remove(permissionToRemove); 
                        DB.Permission_Role.Remove(permissionToRemove);//removin permission from role
                    }
                }

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }

            ViewData["existingPermission"] = 
            ViewData["permissionsList"] = getAllPermissions();

            return View(role);
        }

        [Authorize]
        [HttpGet]
        public ActionResult RoleSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            return View(DB.Roles.ToList());
        }

        public List<Permission_Role> getAllPermissionsByRoleId(int id)
        {
            return DB.Permission_Role.Where(x => x.Role_Id == id).ToList();
        }

        [HttpGet]
        public JsonResult getAllPermissionsByRoleIdJson(int id)
        {
            return Json(getAllPermissionsByRoleId(id).Select(x => x.Permission_Id).ToList(), JsonRequestBehavior.AllowGet);
        }

        public List<Permission> getAllPermissions()
        {
            return DB.Permissions.OrderBy(x => x.Module_Id).ToList();
        }

        public List<SelectListItem> loadmodule()
        {
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
            return Redirect("/Login/Signin");
        }

        [Authorize]
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
        public ActionResult Signin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Signin(User_Login model, string ReturnUrl)
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
                string shaPass = PasswordEncryption.GetSHA1HashData(model.Password);
                User_Login search = (from user in DB.User_Login
                                     where user.UserName == model.UserName
                                     && user.Password == shaPass
                                     select user).FirstOrDefault();

                if (search != null)
                {
                    FormsAuthentication.SetAuthCookie(search.UserName, false);
                    int roleId = (int)search.Role_Id;
                    Session["Profile_Id"] = search.User_Profile_Id;
                    Session["Login_Id"] = search.Id;
                    Session["User_Name"] = search.UserName;
                    Session["Role_Id"] = roleId;

                    if (ReturnUrl != null)
                    {
                        return Redirect(ReturnUrl);
                    }

                    if (roleId >= 6)
                        return RedirectToAction("Dashboard", "Client");
                    else
                        return RedirectToAction("Dashboard", "User");
                }
                else
                    ViewData["msg"] = "wrong username or password";

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
            else if (perm.Module_Id == 0)
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