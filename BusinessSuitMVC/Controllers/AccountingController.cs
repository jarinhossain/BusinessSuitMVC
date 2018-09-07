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
                DB.Expenses.Add(expense);
                DB.SaveChanges();
                if (file != null && file.ContentLength > 0)
                {
                    string extension = Path.GetExtension(Request.Files[0].FileName).ToLower();
                    string path = Path.Combine(Server.MapPath("~/Images/Expense"), "E_" + expense.Id + extension);
                    file.SaveAs(path);/// file save
                }
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
        public List<SelectListItem> loadAccountHead()
        {
            DBContext DB = new DBContext();
            List<Account_Head_TB> account = (from dis in DB.Account_Head_TB
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

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Update";
            }
            return View();
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