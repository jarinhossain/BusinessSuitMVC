using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class ClientController : Controller
    {
        // GET: Client

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
        public ActionResult Create(Client_List client)
        {
            if (client.Counsilor_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Counsilor_Name";
            }
            else if (client.Mobile1 == null)
            {
                ViewData["msg"] = "Please enter your valid Mobile1 Number";
            }
            else if (client.ward == null)
            {
                ViewData["msg"] = "Please enter your valid Word";
            }
            else if (client.District == null)
            {
                ViewData["msg"] = "Please enter your valid District";
            }
            else if (client.Client_Type == null)
            {
                ViewData["msg"] = "Please enter your valid Client_Type";
            }
            else
            {
                DBContext DB = new DBContext();

                DB.Client_List.Add(client);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }

        [HttpGet]
        public ActionResult Search()
        {
            DBContext DB = new DBContext();

            List<Client_List> clientList = (from client in DB.Client_List
                                            select client).ToList();

            return View(clientList);
        }

        [HttpGet]
        public ActionResult Edit(int userid)
        {
            Session["role"] = "testrole";
            if (RoleValidate.IsValidatedRole())
            {

            }
            DBContext DB = new DBContext();
            Client_List client = (from user in DB.Client_List
                                  where user.Id == userid
                                  select user).FirstOrDefault();

            return View(client);
        }
        [HttpPost]
        public ActionResult Edit(Client_List client)
        {
            if(client.Counsilor_Name == null)
            {
                ViewData["msg"] = "Please enter your valid Counsilor_Name";
            }
           else if (client.Mobile1 == null)
            {
                ViewData["msg"] = "Please enter your valid Mobile1";
            }
            else if (client.Mobile2 == null)
            {
                ViewData["msg"] = "Please enter your valid Mobile2";
            }
            else if (client.Email == null)
            {
                ViewData["msg"] = "Please enter your valid Email";
            }
            else if (client.ward == null)
            {
                ViewData["msg"] = "Please enter your valid word";
            }
            else if (client.District == null)
            {
                ViewData["msg"] = "Please enter your valid District";
            }
            else if (client.Address == null)
            {
                ViewData["msg"] = "Please enter your valid Address";
            }
            else if (client.Remarks == null)
            {
                ViewData["msg"] = "Please enter your valid Remarks";
            }
            else
            {
                DBContext DB = new DBContext();
                Client_List Client = (from user in DB.Client_List
                                      where user.Id == user.Id
                                       select user).FirstOrDefault();

                Client.Counsilor_Name = client.Counsilor_Name;
                Client.Bangla_Name = client.Bangla_Name;
                Client.Mobile1 = client.Mobile1;
                Client.Mobile2 = client.Mobile2;
                Client.Email = client.Email;
                Client.ward = client.ward;
                Client.District = client.District;
                Client.Address = client.Address;
                Client.Remarks = client.Remarks;
                Client.Is_Elected = client.Is_Elected;
                Client.Client_Type = client.Client_Type;

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View(client);
        }
    }
}