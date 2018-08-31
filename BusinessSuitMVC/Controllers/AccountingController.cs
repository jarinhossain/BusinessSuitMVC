using BusinessSuitMVC.ModelClasses;
using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
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

            return View();
        }
        [HttpPost]
        public JsonResult ExpenseCreate(Expense expense)
        {
            if (PermissionValidate.validatePermission() == false)
                return Json("Unauthorized", JsonRequestBehavior.AllowGet);


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
                return Json(validation, JsonRequestBehavior.AllowGet);

            }
            else
            {
              
                DB.Expenses.Add(expense);
                DB.SaveChanges();
               // ViewData["msg"] = "Successfully Saved";
                return Json("true",JsonRequestBehavior.AllowGet);
            }
          
        }

        [HttpGet]
        public ActionResult ExpenseEdit(int id)
        {
            if (PermissionValidate.validatePermission() == false)
                return View("Unauthorized");

            Expense expense = (from user in DB.Expenses
                                  where user.Id == id
                                  select user).FirstOrDefault();

            return View(expense);
        }

        [HttpPost]
        public JsonResult ExpenseEdit(Expense expens)
        {
            if (PermissionValidate.validatePermission() == false)
                return Json("Unauthorized",JsonRequestBehavior.AllowGet);

            string validation = ValidateExpense(expens);

            if(validation != "true")
            {
                // ViewData["msg"] = validation;
                return Json(validation, JsonRequestBehavior.AllowGet);
            }
            else
            {
                DBContext DB = new DBContext();
                Expense expense = (from user in DB.Expenses
                                   where user.Id == expens.Id
                                   select user).FirstOrDefault();


                expense.Type = expens.Type;
                expense.Spent_Date = expens.Spent_Date;
                expense.Amount = expens.Amount;
                expense.Description = expens.Description;

                DB.SaveChanges();
                return Json("True", JsonRequestBehavior.AllowGet);

            }
           
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

        [HttpGet]
        public ActionResult ExpenseTypeCreate()
        {
           
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseTypeCreate(Expense_Type type)
        {
            Numeral_DBContext DB = new Numeral_DBContext();
            //DB.Expense_Type.A

            return View();
        }
    }
}