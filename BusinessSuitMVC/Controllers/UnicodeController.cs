using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BusinessSuitMVC.Controllers
{
    public class UnicodeModel{
        public string Id { get; set; }
        public string Value { get; set; }
    }
    public class UnicodeController : Controller
    {
        // GET: Unicode
        [HttpPost]
        public ActionResult Index(List<UnicodeModel> unicode)
        {
            ViewData["unicodeValue"] = unicode;
            return View();
        }
    }
}