using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class OfflineOrderController : Controller
    {
        // GET: OfflineOrder
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
        public ActionResult Create(Offline_Order_Detalis order)
        {
            if (order.Price_Per_Piece == null)
            {
                ViewData["msg"] = "Please enter your valid Price_Per_Piece";
            }
            else if (order.Estimated_Voters == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated_Voters";
            }

            else if (order.Format_No == null)
            {
                ViewData["msg"] = "Please enter your valid Format_No";
            }

            else if (order.Free_Blank_Slip == null)
            {
                ViewData["msg"] = "Please enter your valid Free_Blank_Slip";
            }
            else if (order.Paid_Blank_Slip == null)
            {
                ViewData["msg"] = "Please enter your valid Paid_Blank_Slip";
            }
            else if (order.Slip_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Slip_Content";
            }
            else if (order.Voter_List_Print == null)
            {
                ViewData["msg"] = "Please enter your valid Voter_List_Print";
            }
            else if (order.Voter_List_Print_Comment == null)
            {
                ViewData["msg"] = "Please enter your valid Voter_List_Print_Comment";
            }
            else if (order.Discount == null)
            {
                ViewData["msg"] = "Please enter your valid Discount";

            }
            else if (order.Status == null)
            {
                ViewData["msg"] = "Please enter your valid Status";
            }
            else if (order.Remarks == null)
            {
                ViewData["msg"] = "Please enter your valid Remarks";

            }
            else if (order.Marka == null)
            {
                ViewData["msg"] = "Please enter your valid Marka";
            }
            else
            {
                DBContext DB = new DBContext();
                DB.Offline_Order_Detalis.Add(order);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }

    }
}