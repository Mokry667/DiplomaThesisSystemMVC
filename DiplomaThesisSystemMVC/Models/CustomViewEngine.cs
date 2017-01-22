using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DiplomaThesisSystemMVC.Models
{
    public class CustomViewLocationRazorViewEngine : RazorViewEngine
    {
        public CustomViewLocationRazorViewEngine()
        {
            ViewLocationFormats = new[]
            {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Common/{1}/{0}.cshtml", "~/Views/Common/{1}/{0}.vbhtml",
            "~/Views/Tabs/{1}/{0}.cshtml", "~/Views/Tabs/{1}/{0}.vbhtml",
            "~/Views/Main_Views/{1}/{0}.cshtml", "~/Views/Main_Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            };
            MasterLocationFormats = new[]
            {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Common/{1}/{0}.cshtml", "~/Views/Common/{1}/{0}.vbhtml",
            "~/Views/Tabs/{1}/{0}.cshtml", "~/Views/Tabs/{1}/{0}.vbhtml",
            "~/Views/Main_Views/{1}/{0}.cshtml", "~/Views/Main_Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            };
            PartialViewLocationFormats = new[]
            {
            "~/Views/{1}/{0}.cshtml", "~/Views/{1}/{0}.vbhtml",
            "~/Views/Common/{1}/{0}.cshtml", "~/Views/Common/{1}/{0}.vbhtml",
            "~/Views/Tabs/{1}/{0}.cshtml", "~/Views/Tabs/{1}/{0}.vbhtml",
            "~/Views/Main_Views/{1}/{0}.cshtml", "~/Views/Main_Views/{1}/{0}.vbhtml",
            "~/Views/Shared/{0}.cshtml", "~/Views/Shared/{0}.vbhtml"
            };
        }
    }
}