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
    public class CheckGradeController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        // GET: CheckGrade
        public ActionResult Index()
        {
            string studentID = User.Identity.GetUserId();
            var student = db.Student.Include(s => s.DiplomaThesis).Include(s => s.DiplomaThesisTopic).Include(s => s.Review).Include(s => s.Teacher).Where(s => s.ID == studentID);
            var diplomaThesis = db.Student.Include(s => s.DiplomaThesis).Where(s => s.ID == studentID).Where(s => s.DiplomaThesisID != null);

            if (!diplomaThesis.Any())
            {
                TempData["Message"] = "Work not submitted";
                return View(student.ToList());
            }

            if (!student.Any() && diplomaThesis.Any())
            {
                TempData["Message"] = "Reviewing in progress";
                return View();
            }
            return View(student.ToList());
        }

        public ActionResult CheckReview(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Review review = db.Review.Find(id);

            if (review == null)
            {
                return HttpNotFound();
            }
            // make view more fancy
            return View(review);
        }

    }
}
