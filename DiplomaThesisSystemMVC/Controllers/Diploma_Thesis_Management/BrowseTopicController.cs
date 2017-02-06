using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DiplomaThesisSystemMVC.Models;

namespace DiplomaThesisSystemMVC.Controllers.Diploma_Thesis_Management
{
    public class BrowseTopicController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        // GET: BrowseTopic
        public ActionResult Index()
        {

            var numberOfStudents = db.DiplomaThesisTopic.Select(d => d.NumberOfStudents).Distinct();
            var fieldOfStudy = db.DiplomaThesisTopic.Select(d => d.FieldOfStudy).Distinct();
            var degree = db.DiplomaThesisTopic.Select(d => d.Degree).Distinct();
            var availability = db.DiplomaThesisTopic.Select(d => d.Availability).Distinct();

            ViewBag.NumberOfStudents = new SelectList(numberOfStudents);
            ViewBag.FieldOfStudy = new SelectList(fieldOfStudy);
            ViewBag.Degree = new SelectList(degree);
            ViewBag.Availability = new SelectList(availability);

            var diplomaThesisTopic = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);
            return View(diplomaThesisTopic.ToList());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Filter([Bind(Include = "NumberOfStudents,FieldOfStudy,Degree,Availability")] DiplomaThesisTopic diplomaThesisTopic)
        {

            //var topicList = db.DiplomaThesisTopic.Include(d => d.NumberOfStudents == diplomaThesisTopic.NumberOfStudents);
            var topicList = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);

            var numberOfStudents = db.DiplomaThesisTopic.Select(d => d.NumberOfStudents).Distinct();
            var fieldOfStudy = db.DiplomaThesisTopic.Select(d => d.FieldOfStudy).Distinct();
            var degree = db.DiplomaThesisTopic.Select(d => d.Degree).Distinct();
            var availability = db.DiplomaThesisTopic.Select(d => d.Availability).Distinct();

            ViewBag.NumberOfStudents = new SelectList(numberOfStudents);
            ViewBag.FieldOfStudy = new SelectList(fieldOfStudy);
            ViewBag.Degree = new SelectList(degree);
            ViewBag.Availability = new SelectList(availability);

            // think about better method
            if(diplomaThesisTopic.NumberOfStudents != null)
            {
                topicList = topicList.Where(d => d.NumberOfStudents == diplomaThesisTopic.NumberOfStudents);
            }
            if(diplomaThesisTopic.FieldOfStudy != null)
            {
                topicList = topicList.Where(d => d.FieldOfStudy == diplomaThesisTopic.FieldOfStudy);
            }
            if(diplomaThesisTopic.Degree != null)
            {
                topicList = topicList.Where(d => d.Degree == diplomaThesisTopic.Degree);
            }
            if(diplomaThesisTopic.Availability != null)
            {
                topicList = topicList.Where(d => d.Availability == diplomaThesisTopic.Availability);
            }

            return View(topicList.ToList());
        }


    }
}
