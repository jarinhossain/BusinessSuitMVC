using BusinessSuitMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
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
        public ActionResult Create(Product pro)
        {
            DBContext DB = new DBContext();
            DB.Products.Add(pro);
            DB.SaveChanges();
            ViewData["msg"] = "Successfully Saved";
            return View("Create");
        }
        [HttpGet]
        public ActionResult Edit(int id)
        {
            DBContext DB = new DBContext();
            Product product = (from pro in DB.Products
                               where pro.Id == id
                               select pro).FirstOrDefault();
            return View(product);
        }
        [HttpPost]
        public ActionResult Edit(Product produc)
        {
            DBContext DB = new DBContext();
            Product product = (from pro in DB.Products
                               where pro.Id == produc.Id
                               select pro).FirstOrDefault();
            product.Name = produc.Name;
            product.Is_Online = produc.Is_Online;
            product.Description = produc.Description;
            product.Regular_Price = produc.Regular_Price;
            product.Lower_Price = produc.Lower_Price;
            DB.SaveChanges();
            ViewData["msg"] = "Successfully Updated";
            return View(produc);
        }
    }
}