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
    public class ReviewDiplomaThesisController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
        static private DiplomaThesis reviewedDiplomaThesis;
        static private string studentID;

        // GET: ReviewDiplomaThesis
        public ActionResult Index()
        {
            reviewedDiplomaThesis = new DiplomaThesis();
            string reviewerID = User.Identity.GetUserId();
            var student = db.Student.Include(s => s.DiplomaThesis).Include(s => s.DiplomaThesisTopic).Include(s => s.Review).Include(s => s.Teacher).Where(s => s.Review.ReviewerID == reviewerID).Where(s => s.Review.Content == null);
            if (!student.Any() && null == TempData["Message"])
            {
                TempData["Message"] = "List of diploma thesis to review is empty";
                return View(student.ToList());
            }
            return View(student.ToList());
        }

        public ActionResult CheckAbstract(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesis diplomaThesis = db.DiplomaThesis.Find(id);

            if (diplomaThesis== null)
            {
                return HttpNotFound();
            }
            return View(diplomaThesis);
        }

        public ActionResult WriteReview(int? id, string studID)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesis diplomaThesis = db.DiplomaThesis.Find(id);

            if (diplomaThesis == null)
            {
                return HttpNotFound();
            }

            studentID = studID;

            reviewedDiplomaThesis.ID = diplomaThesis.ID;
            reviewedDiplomaThesis.Name = diplomaThesis.Name;

            // make view more fancy
            return View();
        }

        public ActionResult Create([Bind(Include = "Content, Grade")] Review review)
        {

            // check for multiple students with same diploma thesis
            Student student = db.Student.Find(studentID);

            Review modifiedReview = db.Review.Find(student.ReviewID);
            modifiedReview.Content = review.Content;
            modifiedReview.Grade = review.Grade;
            //add some checks


            if (ModelState.IsValid)
            {
                db.Entry(modifiedReview).State = EntityState.Modified;
                db.SaveChanges();
                TempData["Message"] = "Review Submited";
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
