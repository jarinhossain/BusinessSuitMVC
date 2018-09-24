using BusinessSuitMVC.Controllers;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.ModelClasses
{
    public static class PermissionValidate
    {
        private static DBContext DB = new DBContext();
        public static bool validatePermission()
        {
            string controllerName = "User", actionName="Dashboard";
            try
            {
                //string role = HttpContext.Current.ApplicationInstance.Session["role"].ToString();
                controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
                //int loginId = int.Parse(HttpContext.Current.Session["Login_Id"].ToString());
                int roleId = int.Parse(HttpContext.Current.Session["Role_Id"].ToString());
                //if (roleId == 1)
                //    return true; ///skip super admin permission check
                bool permission = hasPermission(roleId, controllerName, actionName);

                if (permission == true)
                    return true;
            }
            catch(Exception ex)
            {
                string url= string.Format("/Login/Signin?ReturnUrl=%2f{0}%2f{1}", controllerName, actionName);
                HttpContext.Current.Response.Redirect(url, true);
            }
            return false;
        }
        
        public static bool hasPermission(int roleId, string controller, string action)
        {
           
            var permissionList = getPermissionsByRoleId(roleId);

            foreach (var item in permissionList)
            {
                string[] permission = item.Permission.Name.Split('-');
                if (permission[0] == action.ToLower() && permission[1] == controller.ToLower())
                    return true;
               
            }
            return false;
        }

        [OutputCacheAttribute(VaryByParam = "*", Duration = 0, NoStore = true)]
        public static List<Permission_Role> getPermissionsByRoleId(int roleId)
        {
            List<Permission_Role> permissionList = new List<Permission_Role>();

            if (HttpContext.Current.Application["PermissionList"] == null)
            {
                HttpContext.Current.Application["PermissionList"] = permissionList = DB.Permission_Role.ToList();
            }
            else
                permissionList = (List<Permission_Role>)HttpContext.Current.Application["PermissionList"];

            permissionList = permissionList.Where(x => x.Role_Id == roleId).ToList();

            return permissionList;
        }

        public static void updatePermissionCache()
        {
            HttpContext.Current.Application["PermissionList"] = DB.Permission_Role.Include("Permission").ToList(); ///updating permission list cache
        }

    }
}