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
    public class SubmitTopicController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        // GET: SubmitTopic
        public ActionResult Index()
        {
            var diplomaThesisTopic = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);
            return View(diplomaThesisTopic.ToList());
        }

        // GET: SubmitTopic/Create
        public ActionResult Create()
        {
            return View();
        }


        // POST: SubmitTopic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Name,Description,NumberOfStudents,FieldOfStudy,Degree")] DiplomaThesisTopic diplomaThesisTopic)
        {
            // set default values
            diplomaThesisTopic.TeacherID = User.Identity.GetUserId();
            diplomaThesisTopic.Availability = "Free";
            diplomaThesisTopic.ReviewerID = null;
            diplomaThesisTopic.Status = "NotVoted";

            //add some checks

            if (ModelState.IsValid)
            {
                db.DiplomaThesisTopic.Add(diplomaThesisTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(diplomaThesisTopic);
        }

    }
}
