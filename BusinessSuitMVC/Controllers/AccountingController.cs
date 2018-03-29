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
            if (expense.Type == null)
            {
                ViewData["msg"] = "Please enter your valid Type";

            }
            DBContext DB = new DBContext();

            DB.Expenses.Add(expense);
            DB.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
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
            //if (expens.Type == null)
            //{
            //    ViewData["msg"] = "Please enter your valid Type";
            //}
          
            //else
            //{
                DBContext DB = new DBContext();
                Expense expense = (from user in DB.Expenses
                                      where user.Id == expens.Id
                                      select user).FirstOrDefault();


                expense.Type = expens.Type;
                expense.Spent_Date = expens.Spent_Date;
                expense.Description = expens.Description;

                DB.SaveChanges();
                ViewData["msg"] = "Successfully Updated";
            
            return View();
        }
    }
}