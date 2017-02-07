using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomaThesisSystemMVC.Controllers.Website
{

    public class EmptyPageController : Controller
    {
        // GET: EmptyPage
        public ActionResult Index()
        {
            return View();
        }
    }
}