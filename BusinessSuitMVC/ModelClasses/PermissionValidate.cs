using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BusinessSuitMVC.ModelClasses
{
    public static class PermissionValidate
    {
        public static bool IsValidatedRole()
        {
            try
            {
                string role = HttpContext.Current.ApplicationInstance.Session["role"].ToString();
                var controllerName = HttpContext.Current.Request.RequestContext.RouteData.Values["controller"].ToString();
                var actionName = HttpContext.Current.Request.RequestContext.RouteData.Values["action"].ToString();

                if (controllerName == "Person" && (role != "acr" || role != "authorizer"))
                    return true;

                if (controllerName == "Supervisor" && role == "supervisor")
                    return true;

                if (controllerName == "Institute" && role == "approver")
                    return true;

                if (controllerName == "Admin" && (role == "acr" || role == "authorizer"))
                    return true;
            }
            catch
            {
                HttpContext.Current.Response.Redirect("/", true);
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

        public static void getPermissions()
        {
            DBContext DB = new DBContext();

            

        }
    }
}