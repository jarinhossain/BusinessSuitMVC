using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSuitMVC.ModelClasses
{
    public static class PermissionValidate
    {
        private static DBContext DB = new DBContext();
        public static bool validatePermission()
        {
            try
            {
                //string role = HttpContext.Current.ApplicationInstance.Session["role"].ToString();
                var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();
                int loginId = int.Parse(HttpContext.Current.Session["Login_Id"].ToString());
                bool permission = hasPermission(loginId, controllerName, actionName);

                if (permission == true)
                    return true;
            }
            catch
            {
                HttpContext.Current.Response.Redirect("/Login/Logout", true);
            }
            return false;
        }
        
        public static bool hasPermission(int userID, string controller, string action)
        {
            var permissionList = getPermissionsByRoleId(userID);

            foreach (var item in permissionList)
            {
                string[] permission = item.Permission.Name.Split('-');
                if (permission[0] == action.ToLower() && permission[1] == controller.ToLower())
                    return true;
               
            }
            return false;
        }

        public static List<Permission_Role> getPermissionsByRoleId(int roleId)
        {
            List<Permission_Role> permissionList = new List<Permission_Role>();


            permissionList = DB.Permission_Role.Where(x => x.Role_Id == roleId).ToList();


            return permissionList;
        }

    }
}