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
    public class AccountingController : Controller
    {
        private DBContext DB = new DBContext();

        [HttpGet]
        public ActionResult ExpenseCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["accounthd"] = loadAccountHead();
            ViewData["typ"] = loadExpenseType();
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseCreate(Expense expense)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["accounthd"] = loadAccountHead();
            ViewData["typ"] = loadExpenseType();


            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension == ".jpg" || extension == ".pdf" || extension == ".png")
                {
                    ///do nothing
                }
                else
                {
                    ViewData["msg"] = "Failed to Save User Information! Allowed image format is .jpg";
                    return View();
                }
            }
            //if (expense.Id == 0)
            //{
            //    ModelState.AddModelError("", "Select Type");
            //}
            //int selectvalue = expense.Id;
            //ViewBag.SelectedValue = expense.Id;
            //List<Expense> expens = new List<Models.Expense>();
            //expens = (from ex in DB.Expenses
            //          select ex).ToList();

            //expens.Insert(0, new Expense { Id = 0, Type = Convert.ToInt32("Select")});
            //ViewBag.ListOfExpense = expens;
            // return View();
            string validation = ValidateExpense(expense);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                expense.Image = file != null && file.ContentLength > 0 ? true : false;
                expense.Created_By = int.Parse(Session["Login_Id"].ToString());
                DB.Expenses.Add(expense);
                DB.SaveChanges();

                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    expense.Extension = extension;
                    string path = Path.Combine(Server.MapPath("~/Images/Expense"), "E_" + expense.Id + extension);
                    file.SaveAs(path);/// file save
                    DB.SaveChanges();

                }
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Expensetypecreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            return View();
        }
        [HttpPost]
        public ActionResult Expensetypecreate(Expense_Type expense)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext db = new DBContext();
            db.Expense_Type.Add(expense);
            db.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
            return View();

        }
        [HttpGet]
        public ActionResult ExpenseTypeEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Expense_Type expense = (from user in DB.Expense_Type
                               where user.Id == id
                               select user).FirstOrDefault();

            return View(expense);
        }

        [HttpPost]
        public ActionResult ExpenseTypeEdit(Expense_Type expens)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();
            Expense_Type expense = (from user in DB.Expense_Type
                                   where user.Id == expens.Id
                                   select user).FirstOrDefault();

                expense.Name = expens.Name;
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Update";
            
            return View(expens);
        }
        public List<SelectListItem> loadAccountHead()
        {
            DBContext DB = new DBContext();
            List<Account_Head_TB> account = (from dis in DB.Account_Head_TB
                                             where dis.Main_Account_Id == 5 ///only load expense account head
                                       select dis).ToList();
            List<SelectListItem> accountDropdown = new List<SelectListItem>();
            foreach (var item in account)
            {
                accountDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return accountDropdown;
        }
        public List<SelectListItem> loadExpenseType()
        {
            DBContext DB = new DBContext();
            List<Expense_Type> expense = (from dis in DB.Expense_Type
                                             select dis).ToList();
            List<SelectListItem> accountDropdown = new List<SelectListItem>();
            foreach (var item in expense)
            {
                accountDropdown.Add(new SelectListItem() { Value = item.Id.ToString(), Text = item.Name });
            }
            return accountDropdown;
        }
        [HttpGet]
        public ActionResult ExpenseEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["accounthd"] = loadAccountHead();
            ViewData["typ"] = loadExpenseType();

            Expense expense = (from user in DB.Expenses
                                  where user.Id == id
                                  select user).FirstOrDefault();

            return View(expense);
        }

        [HttpPost]
        public ActionResult ExpenseEdit(Expense expens)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            ViewData["accounthd"] = loadAccountHead();
            ViewData["typ"] = loadExpenseType();
            HttpPostedFileBase file = null;
            try { file = Request.Files[0]; } catch { }

            if (file != null && file.ContentLength > 0)
            {
                string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                if (extension == ".jpg" || extension == ".pdf" || extension == ".png")
                {
                    ///do nothing
                }
                else
                {
                    ViewData["msg"] = "Failed to Save User Information! Allowed image format is .jpg";
                    return View();
                }
            }
            string validation = ValidateExpense(expens);

            if(validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();
                Expense expense = (from user in DB.Expenses
                                   where user.Id == expens.Id
                                   select user).FirstOrDefault();

                expense.Account_Head_Id = expens.Account_Head_Id;
                expense.Type = expens.Type;
                expense.Spent_Date = expens.Spent_Date;
                expense.Amount = expens.Amount;
                expense.Description = expens.Description;
                expense.Image = file != null && file.ContentLength > 0 ? true : expens.Image;
                expense.Updated_By = int.Parse(Session["Login_Id"].ToString());
                expense.Updated_On = DateTime.Now;
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    expense.Extension = extension;
                    string path = Path.Combine(Server.MapPath("~/Images/Expense"), "E_" + expens.Id + extension);
                    file.SaveAs(path);/// file save
                    DB.SaveChanges();

                }
                ViewData["msg"] = "Successfully Update";
            }
            return View(expens);
        }

        [HttpGet]
        public ActionResult ExpenseSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();

            List<Expense> expense = (from expens in DB.Expenses
                                     select expens).ToList();

            return View(expense);
        }
        [HttpGet]
        public ActionResult ExpenseTypeSearch()
        {

            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            DBContext DB = new DBContext();

            List<Expense_Type> expense = (from expens in DB.Expense_Type
                                     select expens).ToList();

            return View(expense);
        }
        [HttpGet]
        public ActionResult AccountHeadCreate()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["MainAccountList"] = LoadMainAccount();
            return View();
        }
        [HttpPost]
        public ActionResult AccountHeadCreate(Account_Head_TB acount)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["MainAccountList"] = LoadMainAccount();
            DBContext db = new DBContext();
            db.Account_Head_TB.Add(acount);
            db.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
            return View();
        }
        [HttpGet]
        public ActionResult AccountHeadEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["MainAccountList"] = LoadMainAccount();
            DBContext db = new DBContext();
            Account_Head_TB account = (from ac in db.Account_Head_TB
                                       where ac.Id == id
                                       select ac).FirstOrDefault();


            return View(account);
        }
        [HttpPost]
        public ActionResult AccountHeadEdit(Account_Head_TB account)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");
            ViewData["MainAccountList"] = LoadMainAccount();
            DBContext db = new DBContext();
            Account_Head_TB accoun = (from ac in db.Account_Head_TB
                                      where ac.Id == account.Id
                                      select ac).FirstOrDefault();

            accoun.Name = account.Name;
            db.SaveChanges();

            ViewData["msg"] = "Successfully Updated";
            return View(account);
        }
        [HttpGet]
        public ActionResult AccountheadSearch()
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            DBContext DB = new DBContext();

            List<Account_Head_TB> expense = (from client in DB.Account_Head_TB
                                             select client).ToList();

            return View(expense);
        }
        public List<SelectListItem> LoadMainAccount()
        {
            DBContext DB = new DBContext();
            List<Main_Accounts> mainaccount = (from main in DB.Main_Accounts
                                               select main).ToList();
            List<SelectListItem> gen = new List<SelectListItem>();
            foreach (var item in mainaccount)
            {
                gen.Add(new SelectListItem() { Value = item.ID.ToString(), Text = item.Name });
            }
            return gen;
        }
        public string ValidateExpense(Expense expense)
        {
            if (expense.Type == null)
            {
                return  "Please enter your valid Type";

            }
            else if (expense.Spent_Date == null)
            {
                return "Please enter your valid Spent_Date";

            }
            else if (expense.Amount == null)
            {
                return "Please enter your valid Amount";
            }

            return "true";
        }


    }
}