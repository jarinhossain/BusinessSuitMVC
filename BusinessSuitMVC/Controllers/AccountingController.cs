using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class AccountingController : Controller
    {
        // GET: Accounting
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult ExpenseCreate()
        {
            return View();
        }
        [HttpPost]
        public ActionResult ExpenseCreate(Expense expense)
        {
            string validation = ValidateExpense(expense);

            if (validation != "true")
            {
                ViewData["msg"] = validation;
            }
            else
            {
                DBContext DB = new DBContext();

                DB.Expenses.Add(expense);
                DB.SaveChanges();
                ViewData["msg"] = "Successfully Saved";
            }
            return View();
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DBContext DB = new DBContext();
            Expense expense = (from user in DB.Expenses
                                  where user.Id == id
                                  select user).FirstOrDefault();

            return View(expense);
        }

        [HttpPost]
        public ActionResult Edit(Expense expens)
        {
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


                expense.Type = expens.Type;
                expense.Spent_Date = expens.Spent_Date;
                expense.Amount = expens.Amount;
                expense.Description = expens.Description;

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            }
            return View();
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