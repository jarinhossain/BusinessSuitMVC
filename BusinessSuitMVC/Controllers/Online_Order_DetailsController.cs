using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class Online_Order_DetailsController : Controller
    {
        // GET: Online_Order_Details
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public ActionResult Create(Online_Order_Detalis online)
        {
            if (online.Advertisement_Area == null)
            {
                ViewData["msg"] = "Please enter your valid Advertisement_Area";
            }
            else if (online.Estimated_Reach_Ordered == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated_Reach_Ordered";
            }
            else if (online.Estimated_Budget == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated_Budget";
            }
            else if (online.Price_Per_Piece == null)
            {
                ViewData["msg"] = "Please enter your valid Price_Per_Piece";
            }
            else if (online.Age_Group == null)
            {
                ViewData["msg"] = "Please enter your valid Age_Group";
            }
            else if (online.Duration == null)
            {
                ViewData["msg"] = "Please enter your valid Duration";
            }
            else if (online.Obd_Voice_Provided == null)
            {
                ViewData["msg"] = "Please enter your valid Obd_Voice_Provided";
            }
            else if (online.Obd_Vioce_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Obd_Vioce_Content";
            }
            else if (online.Sms_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Sms_Content";
            }
            else if (online.Status == null)
            {
                ViewData["msg"] = "Please enter your valid Status";
            }
            else if (online.Discount == null)
            {
                ViewData["msg"] = "Please enter your valid Discount";
            }
            else if (online.Sms_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Sms_Content";
            }
            else
            {
                DBContext DB = new DBContext();
                DB.Online_Order_Detalis.Add(online);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
    }
}