using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSuitMVC.ModelClasses
{
    public static class PermissionValidate
    {
        public static bool validatePermission()
        {
            try
            {
                //string role = HttpContext.Current.ApplicationInstance.Session["role"].ToString();
                var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

                bool permission = hasPermission(1, controllerName, actionName);

                if (permission == true)
                    return true;
            }
            catch
            {
                HttpContext.Current.Response.Redirect("/Home/Dashboard", true);
            }
            return false;
        }

        public static bool IsValidatedReport()
        {
            string role = HttpContext.Current.ApplicationInstance.Session["role"].ToString();
            

            if (role == "acr" || role == "authorizer" || role == "supervisor" || role == "approver")
                return true;

            return false;
        }

        public static List<Permission_User> getPermissions(int userID)
        {
            List<Permission_User> permissionList= new List<Permission_User>();
            DBContext DB = new DBContext();
            
                permissionList = DB.Permission_User.Where(x => x.User_Id == userID).ToList();
            

            return permissionList;
        }

        public static bool hasPermission(int userID, string controller, string action)
        {
            var permissionList = getPermissions(userID);

            foreach (var item in permissionList)
            {
                string[] permission = item.Permission.Name.Split('-');
                if (permission[0] == controller.ToLower() && permission[1] == action.ToLower())
                    return true;
               
            }
            return false;
        }
    }
}