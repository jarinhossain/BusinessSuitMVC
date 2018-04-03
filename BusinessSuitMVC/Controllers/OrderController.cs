using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class OrderController : Controller
    {
        // GET: Order
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult SmsMarketingCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult SmsMarketingCreate(Online_Order_Detalis online)
        {
            string validation = validationCreate(online);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
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
        public String validationCreate(Online_Order_Detalis online)
        {
            if (online.Advertisement_Area == null)
            {
                return "Please enter your valid Advertisement Area";
            }
            else if (online.Ward == null)
            {
                return "Please enter your valid Ward";
            }
            else if (online.Estimated_Reach_Ordered == null)
            {
                return "Please enter your valid Number Of SMS";
            }
            else if (online.Promotion_Date_From == null)
            {
                return "Please enter your valid Promotion Date From";
            }
            else if (online.Sms_Content == null)
            {
                return "Please enter your valid Sms Content";
            }
            else if (online.Price_Per_Piece == null)
            {
                return "Please enter your valid Price Per SMS";
            }


            return "true";

        }
        [HttpGet]
        public ActionResult TelephonyMarketingCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult TelephonyMarketingCreate(Online_Order_Detalis online)
        {
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
            string validation = validationTelephonyCreate(online);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                online.Obd_Voice_File = file != null && file.ContentLength > 0 ? true : false;

                DB.Online_Order_Detalis.Add(online);
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/TelephonyOrder"), "T_" + online.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
        public String validationTelephonyCreate(Online_Order_Detalis online)
        {
            if (online.Advertisement_Area == null)
            {
                return "Please enter your valid Advertisement Area";
            }
            else if (online.Ward == null)
            {
                return "Please enter your valid Ward";
            }
            else if (online.Estimated_Reach_Ordered == null)
            {
                return "Please enter your valid Number Of SMS";
            }
            else if (online.Promotion_Date_From == null)
            {
                return "Please enter your valid Promotion Date From";
            }
          
            else if (online.Price_Per_Piece == null)
            {
                return "Please enter your valid Price Per SMS";
            }


            return "true";

        }
        [HttpGet]
        public ActionResult VoterSlipCreate()
        {

            return View();
        }
        [HttpPost]
        public ActionResult VoterSlipCreate(Offline_Order_Detalis offline)
        {
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
           
            string validation = validationVoterCreate(offline);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                offline.Passport_Image = file != null && file.ContentLength > 0 ? true : false;
                DB.Offline_Order_Detalis.Add(offline);
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/VoterSlip"), "V_" + offline.Id + extension);
                    file.SaveAs(path);/// file save
                }
               
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }       
        public String validationVoterCreate(Offline_Order_Detalis offline)
        {
            if (offline.Slip_Format_Id == null)
            {
                return "Please enter your valid Slip Format";
            }
            else if (offline.Estimated_Voters == null)
            {
                return "Please enter your valid Estimated Voters";
            }
            //else if (offline.Passport_Image == null)
            //{
            //    return "Please enter your valid Passport Size Photo";
            //}
            //else if (offline.Sample_Slip_Image == null)
            //{
            //    return "Please enter your valid Sample Voter Slip";
            //}
            //else if (offline.Is_Cd_Provided == null)
            //{
            //    return "Please enter your valid Cd Provided";
            //}
            //else if (offline.Voter_List_File == null)
            //{
            //    return "Please enter your valid Voter List File";
            //}
            //else if (offline.Center_Name_List == null)
            //{
            //    return "Please enter your valid Center Name List";
            //}
            //else if (offline.Center_Name_List_Image == null)
            //{
            //    return "Please enter your valid Center List Image";
            //}
            //else if (offline.Paid_Blank_Slip == null)
            //{
            //    return "Please enter your valid Blank Slip";
            //}
            //else if (offline.Blank_Slip_Price == null)
            //{
            //    return "Please enter your valid Blank Slip Price";
            //}
            //else if (offline.Marka == null)
            //{
            //    return "Please enter your valid Marka";
            //}
            //else if (offline.Marka_Image == null)
            //{
            //    return "Please enter your valid Marka Image";
            //}
          
            else if (offline.Delivery_Type == null)
            {
                return "Please enter your valid Delivery Type";
            }

            else if (offline.Price_Per_Piece == null)
            {
                return "Please enter your valid Price Per Piece";
            }
            return "true";

        }
     }
}