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
    public class ClientController : Controller
    {

        [HttpGet]
        public ActionResult Create()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["DistrictList"] = loadDistrict();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Client_List client)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["DistrictList"] = loadDistrict();
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

            string validation = validationCreate(client);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                client.Created_By = int.Parse(Session["Login_Id"].ToString());
                client.Image = file != null && file.ContentLength > 0 ? true : false;
                DB.Client_List.Add(client);
                DB.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/Client"), "C_" + client.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }

        public String validationCreate(Client_List client)
        {
            if (client.Counsilor_Name == null)
            {
                return "Please enter your valid Counsilor_Name";
            }
            //else if (client.Mobile1 == null)
            //{
            //    return "Please enter your valid Mobile1";
            //}
            else if (client.Mobile1 == null || client.Mobile1.Length != 11)
            {
                return "Please enter your valid Mobile1";
            }
            else if (client.ward == null)
            {
                return "Please enter your valid word";
            }
            else if (client.District_Id == null)
            {
                return "Please enter your valid District";
            }
            else if (client.Client_Type == null)
            {
                return "Please enter your valid Client Type";
            }
            return "true";
        }


        [HttpGet]
        public ActionResult Search()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();

            List<Client_List> clientList = (from client in DB.Client_List
                                            select client).ToList();

            return View(clientList);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["DistrictList"] = loadDistrict();
            DBContext DB = new DBContext();
            Client_List client = (from user in DB.Client_List
                                  where user.Id == id
                                  select user).FirstOrDefault();

            return View(client);
        }
        [HttpPost]
        public ActionResult Edit(Client_List client)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["DistrictList"] = loadDistrict();
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
            string validation = validationCreate(client);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                Client_List Client = (from user in DB.Client_List
                                      where user.Id == client.Id
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
                //  Client.Is_Elected = client.Is_Elected;
                Client.Client_Type = client.Client_Type;
                Client.Present_Position = client.Present_Position;
                Client.Image = file != null && file.ContentLength > 0 ? true : client.Image;
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/Client"), "C_" + client.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Updated";
            }
            return View(client);
        }

        [HttpGet]
        public ActionResult Online_Create()
        {
            
            return View();
        }
        [HttpPost]
        public ActionResult Online_Create(Online_Order_Details online)
        {
            

            if (online.Advertisement_Area == null)
            {
                ViewData["msg"] = "Please enter your valid Advertisement Area";
            }
            else if (online.Estimated_Reach_Ordered == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated Reach Ordered";
            }
            else if (online.Estimated_Budget == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated Budget";
            }
            else if (online.Price_Per_Piece == null)
            {
                ViewData["msg"] = "Please enter your valid Price Per Piece";
            }
            else if (online.Age_Group == null)
            {
                ViewData["msg"] = "Please enter your valid Age Group";
            }
            //else if (online.Duration == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Duration";
            //}
            //else if (online.Obd_Voice_Provided == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Obd Voice Provided";
            //}
            else if (online.Obd_Vioce_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Obd Vioce Content";
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
            else
            {
                DBContext DB = new DBContext();
                DB.Online_Order_Details.Add(online);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }

            return View();
        }
        [HttpGet]
        public ActionResult OnlineEdit(int userid)
        {
            DBContext DB = new DBContext();
            Online_Order_Details online = (from user in DB.Online_Order_Details
                                           where user.Id == userid
                                           select user).FirstOrDefault();

            return View(online);
        }
        [HttpPost]
        public ActionResult OnlineEdit(Online_Order_Details online)
        {
            if (online.Advertisement_Area == null)
            {
                ViewData["msg"] = "Please enter your valid Advertisement Area";
            }
            else if (online.Estimated_Reach_Ordered == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated Reach Ordered";
            }
            else if (online.Estimated_Budget == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated Budget";
            }
            else if (online.Price_Per_Piece == null)
            {
                ViewData["msg"] = "Please enter your valid Price Per Piece";
            }
            else if (online.Age_Group == null)
            {
                ViewData["msg"] = "Please enter your valid Age Group";
            }
            //else if (online.Duration == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Duration";
            //}
            //else if (online.Obd_Voice_Provided == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Obd Voice Provided";
            //}
            else if (online.Obd_Vioce_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Obd Vioce Content";
            }
            else if (online.Sms_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Sms Content";
            }
            else if (online.Status == null)
            {
                ViewData["msg"] = "Please enter your valid Status";
            }
            else if (online.Discount == null)
            {
                ViewData["msg"] = "Please enter your valid Discount";
            }
            else
            {
                DBContext DB = new DBContext();
                Online_Order_Details Online = (from user in DB.Online_Order_Details
                                               where user.Id == user.Id
                                               select user).FirstOrDefault();




                Online.Advertisement_Area = online.Advertisement_Area;
                Online.Estimated_Reach_Ordered = online.Estimated_Reach_Ordered;
                Online.Estimated_Budget = online.Estimated_Budget;
                Online.Price_Per_Piece = online.Price_Per_Piece;
                Online.Age_Group = online.Age_Group;
                //Online.Duration= online.Duration;
                //Online.Obd_Voice_Provided = online.Obd_Voice_Provided;
                Online.Obd_Vioce_Content = online.Obd_Vioce_Content;
                Online.Sms_Content = online.Sms_Content;
                Online.Status = online.Status;
                Online.Discount = online.Discount;
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View(online);
        }
        [HttpGet]
        public ActionResult Offline_Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Offline_Create(Offline_Order_Details offline)
        {

            if (offline.Price_Per_Piece == null)
            {
                ViewData["msg"] = "Please enter your valid Price Per Piece";
            }
            else if (offline.Estimated_Voters == null)
            {
                ViewData["msg"] = "Please enter your valid Estimated Voters";
            }
            else if (offline.Format_No == null)
            {
                ViewData["msg"] = "Please enter your valid Format No";
            }
            else if (offline.Free_Blank_Slip == null)
            {
                ViewData["msg"] = "Please enter your valid Free Blank Slip";
            }
            else if (offline.Paid_Blank_Slip == null)
            {
                ViewData["msg"] = "Please enter your valid Paid Blank Slip";
            }
            else if (offline.Sample_Slip_Image == null)
            {
                ViewData["msg"] = "Please enter your valid Sample Slip Image";
            }

            else if (offline.Slip_Content == null)
            {
                ViewData["msg"] = "Please enter your valid Slip Content";
            }
            else if (offline.Voter_List_Print == null)
            {
                ViewData["msg"] = "Please enter your valid Voter List Print";
            }
            else if (offline.Voter_List_Print_Comment == null)
            {
                ViewData["msg"] = "Please enter your valid Voter List Print Comment";
            }
            else if (offline.Voter_Slip_Image_Type == null)
            {
                ViewData["msg"] = "Please enter your valid Voter Slip Image Type";
            }
            //else if (offline.Is_Cd_Provided == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Is Cd Provided";
            //}
            //else if (offline.Kendro_Name_Image == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Kendro Name Image";
            //}
            //else if (offline.Kendro_Name_List == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Voter List File";
            //}

            else if (offline.Discount == null)
            {
                ViewData["msg"] = "Please enter your valid Discount";
            }
            else if (offline.Status == null)
            {
                ViewData["msg"] = "Please enter your valid Status";
            }
            else if (offline.Remarks == null)
            {
                ViewData["msg"] = "Please enter your valid Remarks";
            }
            else if (offline.Marka == null)
            {
                ViewData["msg"] = "Please enter your valid Marka";
            }
            else
            {
                DBContext DB = new DBContext();
                DB.Offline_Order_Details.Add(offline);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }

            return View();
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            Client_List client = (from u in DB.Client_List
                                  where u.Id == id
                                  select u).FirstOrDefault();

            return View(client);
        }

        [HttpGet]
        public ActionResult ClientLoginDetails(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            var DBLogin = DB.User_Login.Include("Client_list").Where(x => x.Client_Id == id).FirstOrDefault();

            return View("ClientLoginDetails", DBLogin);
        }

        [HttpGet]
        public ActionResult ClientMakeSource(int id)///client id
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            Numeral_DBContext Num_DB = new Numeral_DBContext();
            Source sourceData = Num_DB.Sources.Where(x => x.Ref_Id == id && x.Is_Client == true).FirstOrDefault();

            if (sourceData != null)
            {
                return Redirect("~/Source/SourceNumber?sId=" + sourceData.Id);
            }

            Source source = new Source();

            Client_List client = (from u in DB.Client_List
                                  where u.Id == id
                                  select u).FirstOrDefault();
            
            source.Ref_Id = client.Id;
            source.Is_Client = true;
            source.Contact_Name = client.Counsilor_Name;
            source.Mobile1 = client.Mobile1;
            source.Mobile2 = client.Mobile2;
            source.Ward = client.ward;
            source.District_Id = client.District_Id;
            source.Source_Type_Id = client.Client_Type;
            source.Address = client.Address;
            source.Created_By = int.Parse(Session["Login_Id"].ToString());
            
            Num_DB.Sources.Add(source);
            Num_DB.SaveChanges();

            return Redirect("~/Source/SourceNumber?sId=" + source.Id);
        }

        [HttpGet]
        public ActionResult ClientMakeUser(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();
            Client_List client = (from u in DB.Client_List
                                  where u.Id == id
                                  select u).FirstOrDefault();

            ///check if the client has already login access
            if(client.User_Login.Count >= 1)
            {
                return Redirect("~/Client/ClientLoginDetails/" + client.Id);
            }

            User_Login login = new User_Login();
            login.Is_Client = true;
            login.Is_Active = true;
            login.Client_Id = client.Id;
            login.UserName = client.Mobile1;
            login.Role_Id = 5;///client role
            login.Password = PasswordEncryption.GetSHA1HashData(client.Mobile1 + client.Id.ToString());
            login.Created_By = int.Parse(Session["Login_Id"].ToString());

            Client_Inventory clientInventory = new Client_Inventory();
            clientInventory.Client_Id = client.Id;
            clientInventory.Free_Call = 2;
            clientInventory.Free_Sms = 2;

            DB.Client_Inventory.Add(clientInventory);
            DB.User_Login.Add(login);
            DB.SaveChanges();

            return Redirect("~/Client/ClientLoginDetails/" + client.Id);
        }

        [HttpGet]
        public ActionResult Dashboard()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            return View();
        }

        public List<SelectListItem> loadDistrict()
        {
            DBContext DB = new DBContext();
            List<District> district = (from dis in DB.Districts
                                       select dis).ToList();
            List<SelectListItem> districtDropdown = new List<SelectListItem>();
            foreach (var item in district)
            {
                districtDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return districtDropdown;
        }
    }
}