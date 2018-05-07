﻿using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class SourceController : Controller
    {
        // GET: Source
        [HttpGet]
        public ActionResult Create()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["SourceList"] = loadTypeDropDown();
            ViewData["District"] = loadDistrictDropdown();
            ViewData["Division"] = loadDivisionDropDown();


            return View();
            //  return View();
        }

        [HttpPost]
        public ActionResult Create(Source source)
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

            String validation = ValidationSource(source);
            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                Numeral_DBContext DB = new Numeral_DBContext();
                source.Image = file != null && file.ContentLength > 0 ? true : false;
                DB.Sources.Add(source);
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/Source"), "S_" + source.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Saved";
            }
            ViewData["SourceList"] = loadTypeDropDown();
            ViewData["District"] = loadDistrictDropdown();
            ViewData["Division"] = loadDivisionDropDown();
            return View();
        }
        public String ValidationSource(Source source)
        {
            if (source.Contact_Name == null)
            {
                return "Please enter your valid Contact Name";
            }
            else if (source.Source_Type_Id == null)
            {
                return "Please enter your valid Source Type";
            }
            //else if (source.Mobile1 == null)
            //{
            //    return "Please enter your valid Mobile1";
            //}
            else if (source.Mobile1 == null || source.Mobile1.Length != 11)
            {
                return "Please enter your valid Mobile1";
            }
            else if (source.Division_Id == null)
            {
                return "Please enter your valid Division";
            }
            else if (source.District_Id == null)
            {
                return "Please enter your valid District";
            }
            else if (source.Ward == null)
            {
                return "Please enter your valid Ward";
            }
            else if (source.Address == null)
            {
                return "Please enter your valid Address";
            }
            return "true";
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["SourceList"] = loadTypeDropDown();
            ViewData["District"] = loadDistrictDropdown();
            ViewData["Division"] = loadDivisionDropDown();
            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from user in DB.Sources
                             where user.Id == id
                             select user).FirstOrDefault();

            return View(source);
        }
        [HttpPost]
        public ActionResult Edit(Source source)
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
            String validation = ValidationSource(source);
            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {

                Numeral_DBContext DB = new Numeral_DBContext();
                Source Source = (from user in DB.Sources
                                 where user.Id == source.Id
                                 select user).FirstOrDefault();

                Source.Contact_Name = source.Contact_Name;
                Source.Company_Name = source.Company_Name;
                Source.Source_Type = source.Source_Type;
                Source.Mobile1 = source.Mobile1;
                Source.Mobile2 = source.Mobile2;
                Source.Division_Id = source.Division_Id;
                Source.District_Id = source.District_Id;
                Source.Ward = source.Ward;
                Source.Address = source.Address;
                Source.Image = file != null && file.ContentLength > 0 ? true : source.Image;
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/Source"), "S_" + source.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Updated";
            }
            ViewData["SourceList"] = loadTypeDropDown();
            ViewData["District"] = loadDistrictDropdown();
            ViewData["Division"] = loadDivisionDropDown();
            return View(source);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from u in DB.Sources
                             where u.Id == id
                             select u).FirstOrDefault();

            return View(source);
        }

        [HttpGet]
        public ActionResult Search()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Numeral_DBContext DB = new Numeral_DBContext();
            List<Source> source = (from user in DB.Sources
                                   select user).ToList();


            return View(source);
        }

        [HttpGet]
        public ActionResult SourceNumberCreate(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from sourc in DB.Sources
                             where sourc.Id == id
                             select sourc).FirstOrDefault();

            ///if the source not found in the database
            if (source == null)
            {
                ViewData["msg"] = "no source found";
                return RedirectToAction("Search", "Source");
            }

            if (TempData["msg"] != null)
            {
                ViewData["msg"] = TempData["msg"];
            }
            ViewData["SourceList"] = loadTypeDropDown();
            return View(source);
        }

        [HttpPost]
        public ActionResult SourceNumberCreate(int SourceId, string MobileNumber)
        {
            if (PermissionValidate.validatePermission() == false)
                return Json("Unauthorized", JsonRequestBehavior.AllowGet);

            Numeral_DBContext DB = new Numeral_DBContext();
            bool isExists = DB.Numbers.Where(x => x.Source_Id == SourceId && "0" + x.Number1.ToString()  == MobileNumber).Any();
            int mobileNumber = 0;

            if (int.TryParse(MobileNumber, out mobileNumber) == false)
            {
                //TempData["msg"] = "Invalid mobile number";
                return Json("Invalid mobile number", JsonRequestBehavior.AllowGet);
            }
            else if(isExists == true)
            {
                return Json("Duplicate Number in same source", JsonRequestBehavior.AllowGet);
            }
            else
            {
                
                Number number = new Number();
                number.Number1 = mobileNumber;
                number.Source_Id = SourceId;
                number.Created_By = int.Parse(Session["Login_Id"].ToString());
                // Source.Mobile1 = source.Mobile1;
                DB.Numbers.Add(number);
                try
                {
                    DB.SaveChanges();
                    //TempData["msg"] = "Successfully Added";
                    return Json("true", JsonRequestBehavior.AllowGet);
                }
                catch
                {
                    return Json("Unknown Error Occoured", JsonRequestBehavior.AllowGet);
                }
            }
            //Source/SourceNumerCreate?id=4
        }

        [HttpGet]
        public ActionResult SourceNumberSearch(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Numeral_DBContext Numeral_DB = new Numeral_DBContext();

            List<Number> numberList = (from num in Numeral_DB.Numbers
                                       where num.Source_Id == id
                                            select num).ToList();
            Source source = Numeral_DB.Sources.Find(id);
            ViewData["ward"] = source.Ward;
            ViewData["contactName"] = source.Contact_Name;
            return View(numberList);
        }

        [HttpGet]
        public ActionResult AllSourcesNumberSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Numeral_DBContext Numeral_DB = new Numeral_DBContext();

            List<Number> numberList = (from num in Numeral_DB.Numbers
                                       select num).ToList();

            return View(numberList);
        }

        public List<SelectListItem> loadDistrictDropdown()
        {
            DBContext DB = new DBContext();
            List<District> district = (from user in DB.Districts
                                       select user).ToList();

            List<SelectListItem> districtDropdown = new List<SelectListItem>();

            foreach (var item in district)
            {

                districtDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return districtDropdown;
        }
        public List<SelectListItem> loadDivisionDropDown()
        {
            DBContext DB = new DBContext();
            List<Division> division = (from div in DB.Divisions
                                       select div).ToList();
            List<SelectListItem> divisiondropdown = new List<SelectListItem>();
            foreach (var item in division)
            {
                divisiondropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return divisiondropdown;
        }

        public List<SelectListItem> loadTypeDropDown()
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            List<Source_Type> type = (from typ in DB.Source_Type
                                       select typ).ToList();
            List<SelectListItem> typeDropdown = new List<SelectListItem>();
            foreach (var item in type)
            {
                typeDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return typeDropdown;
        }
    }
}