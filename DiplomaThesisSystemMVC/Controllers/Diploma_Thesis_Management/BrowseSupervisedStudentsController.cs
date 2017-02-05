using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaThesisSystemMVC.Models;
using Microsoft.AspNet.Identity;

namespace DiplomaThesisSystemMVC.Controllers.Diploma_Thesis_Management
{
    public class BrowseSupervisedStudentsController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        // GET: BrowseSupervisedStudents
        public ActionResult Index()
        {
            string supervisorID = User.Identity.GetUserId();
            var student = db.Student.Include(s => s.DiplomaThesis).Include(s => s.DiplomaThesisTopic).Include(s => s.Review).Include(s => s.Teacher).Where(s => s.SupervisorID == supervisorID);
            // check if blank is working
            if (student == null){
                TempData["Message"] = "List is empty";
                return View();
            }
            return View(student.ToList());
        }

    }
}
