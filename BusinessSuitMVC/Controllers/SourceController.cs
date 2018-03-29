using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
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
            return View();
        }
        [HttpPost]
        public ActionResult Create(Source source)
        {
            if (source.Contact_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Contact Name";
            }
            else if (source.Company_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Company Name";
            }
            else if (source.Source_Type == null)
            {
                ViewData["msg"] = "Please enter your valid Source Type";
            }
            else if (source.Mobile1 == null)
            {
                ViewData["msg"] = "Please enter your valid Mobile1";
            }
            else if (source.Division_Id == null)
            {
                ViewData["msg"] = "Please enter your valid Division";
            }
            else if (source.District_Id == null)
            {
                ViewData["msg"] = "Please enter your valid District";
            }
            else if (source.Ward == null)
            {
                ViewData["msg"] = "Please enter your valid Ward";
            }
            else if (source.Address == null)
            {
                ViewData["msg"] = "Please enter your valid Address";
            }
            else
            {
                Numeral_DBContext DB = new Numeral_DBContext();
                DB.Sources.Add(source);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from user in DB.Sources
                              where user.Id == id
                                  select user).FirstOrDefault();

            return View(source);
        }
        [HttpPost]
        public ActionResult Edit(Source source)
        {
            if (source.Contact_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Contact Name";
            }
            else if (source.Company_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Company Name";
            }
            else if (source.Source_Type == null)
            {
                ViewData["msg"] = "Please enter your valid Source Type";
            }
            else if (source.Mobile1 == null)
            {
                ViewData["msg"] = "Please enter your valid Mobile1";
            }
            else if (source.Division_Id == null)
            {
                ViewData["msg"] = "Please enter your valid Division";
            }
            else if (source.District_Id == null)
            {
                ViewData["msg"] = "Please enter your valid District";
            }
            else if (source.Ward == null)
            {
                ViewData["msg"] = "Please enter your valid Ward";
            }
            else if (source.Address == null)
            {
                ViewData["msg"] = "Please enter your valid Address";
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
                Source.Division_Id = source.Division_Id;
                Source.District_Id = source.District_Id;
                Source.Ward = source.Ward;
                Source.Address = source.Address;

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Details(int id)
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from u in DB.Sources
                             where u.Id == id
                             select u).FirstOrDefault();

            return View(source);
        }

        [HttpGet]
        public ActionResult Search()
        {

            Numeral_DBContext DB = new Numeral_DBContext();
            List<Source> source = (from user in DB.Sources
                                   select user).ToList();


            return View(source);
        }

        [HttpGet]
        public ActionResult SourceNumberCreate(int id)
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            Source source = (from user in DB.Sources
                             where user.Id == id
                             select user).FirstOrDefault();

            return View(source);
        }
        [HttpPost]
        public ActionResult SourceNumberCreate(Source source)
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            Source Source = (from user in DB.Sources
                             where user.Id == source.Id
                             select user).FirstOrDefault();

            Source.Contact_Name = source.Contact_Name;
            Source.Company_Name = source.Company_Name;
            Source.Source_Type = source.Source_Type;
           // Source.Mobile1 = source.Mobile1;
           
            DB.SaveChanges();
            ViewData["msg"] = "Successfully Updated";
            return View();
        }
       
        }
}