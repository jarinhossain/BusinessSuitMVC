using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    [Authorize]
    public class FileController : Controller
    {
        // GET: File
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult PlayFileCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            return View();
        }
        [HttpPost]
        public ActionResult PlayFileCreate(Play_File_TB playFile)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension == ".wav" || extension == ".gsm")
                {
                    ///do nothing
                }
                else
                {
                    ViewData["msg"] = "Select .wav or .gsm or  less than 20Μ";
                    return View();
                }
            }
            DBContext db = new DBContext();
            //acount.File_Name = file != null && file.ContentLength > 0 ? true : false; Play_Id_Remarks
            db.Play_File_TB.Add(playFile);
            db.SaveChanges();

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                playFile.File_Name = "Play_" + playFile.Id + "_" + playFile.Remarks + extension;
                string path = Path.Combine(Server.MapPath("~/Music/Play"), playFile.File_Name);
                file.SaveAs(path);/// file save
                db.SaveChanges();

            }
            ViewData["msg"] = "Successfully Saved";
            return View();
        }
        [HttpGet]
        public ActionResult PlayFileEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            Play_File_TB account = (from ac in db.Play_File_TB
                                       where ac.Id == id
                                       select ac).FirstOrDefault();


            return View(account);
        }
        [HttpPost]
        public ActionResult PlayFileEdit(Play_File_TB account)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            Play_File_TB accoun = (from ac in db.Play_File_TB
                                      where ac.Id == account.Id
                                      select ac).FirstOrDefault();

            accoun.File_Name = account.File_Name;
            accoun.Remarks = account.Remarks;
            db.SaveChanges();

            ViewData["msg"] = "Successfully Updated";
            return View(account);
        }
        [HttpGet]
        public ActionResult PlayFileSearch()
        {

            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();

            List<Play_File_TB> expense = (from client in DB.Play_File_TB
                                     select client).ToList();

            return View(expense);
        }
        [HttpGet]
        public ActionResult SlipFileCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["Constituencylist"] = LoadConstituency();
            return View();
        }
        [HttpPost]
        public ActionResult SlipFileCreate(Slip_File slipfile)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["Constituencylist"] = LoadConstituency();
            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0 && file.ContentLength <= (2 * 1024))
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension == ".pdf" || extension == ".zip")
                {
                    ///do nothing
                }
                else
                {
                    ViewData["msg"] = "Select .wav or .gsm or  less than 2Μ";
                    return View();
                }
            }
           
            DBContext db = new DBContext();
            db.Slip_File.Add(slipfile);
            db.SaveChanges();
            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();

                Constituency accoun = (from con in db.Constituencies
                                       where con.Id == slipfile.Constituency_Id
                                       select con).FirstOrDefault();

                slipfile.File_Name = "Slip_" + slipfile.Id + "_" + accoun.Name + "_W"+slipfile.Ward + "_" + slipfile.Remarks + extension;
                string path = Path.Combine(Server.MapPath("~/Music/Slip"), slipfile.File_Name);
                file.SaveAs(path);/// file save
                db.SaveChanges();

            }
            ViewData["msg"] = "Successfully Saved";
            return View();
        }
        [HttpGet]
        public ActionResult SlipFileEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["Constituencylist"] = LoadConstituency();
            DBContext db = new DBContext();
            Slip_File account = (from ac in db.Slip_File
                                    where ac.Id == id
                                    select ac).FirstOrDefault();


            return View(account);
        }
        [HttpPost]
        public ActionResult SlipFileEdit(Slip_File account)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["Constituencylist"] = LoadConstituency();
            DBContext db = new DBContext();
            Slip_File accoun = (from ac in db.Slip_File
                                   where ac.Id == account.Id
                                   select ac).FirstOrDefault();

            accoun.Constituency_Id = account.Constituency_Id;
            accoun.Ward = account.Ward;
            accoun.Files = account.Files;
            accoun.Remarks = account.Remarks;
            db.SaveChanges();

            ViewData["msg"] = "Successfully Updated";
            return View(account);
        }
        public List<SelectListItem> LoadConstituency()
        {
            DBContext DB = new DBContext();
            List<Constituency> mainaccount = (from main in DB.Constituencies
                                               select main).ToList();
            List<SelectListItem> gen = new List<SelectListItem>();
            foreach (var item in mainaccount)
            {
                gen.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return gen;
        }
        [HttpGet]
        public ActionResult SlipFileSearch()
        {

            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();

            List<Slip_File> expense = (from client in DB.Slip_File
                                          select client).ToList();

            return View(expense);
        }
    }
}