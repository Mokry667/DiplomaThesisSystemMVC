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
    public class SubmitTopicController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        // GET: SubmitTopic
        public ActionResult Index()
        {
            var diplomaThesisTopic = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);
            return View(diplomaThesisTopic.ToList());
        }

        // GET: SubmitTopic/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
            if (diplomaThesisTopic == null)
            {
                return HttpNotFound();
            }
            return View(diplomaThesisTopic);
        }

        // GET: SubmitTopic/Create
        public ActionResult Create()
        {
            ViewBag.ReviewerID = new SelectList(db.Reviewer, "ID", "FirstName");
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName");
            return View();
        }

        // POST: SubmitTopic/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ReviewerID,TeacherID,Name,Description,NumberOfStudents,FieldOfStudy,Degree,Availability,Status")] DiplomaThesisTopic diplomaThesisTopic)
        {
            if (ModelState.IsValid)
            {
                db.DiplomaThesisTopic.Add(diplomaThesisTopic);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ReviewerID = new SelectList(db.Reviewer, "ID", "FirstName", diplomaThesisTopic.ReviewerID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", diplomaThesisTopic.TeacherID);
            return View(diplomaThesisTopic);
        }

        // GET: SubmitTopic/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
            if (diplomaThesisTopic == null)
            {
                return HttpNotFound();
            }
            ViewBag.ReviewerID = new SelectList(db.Reviewer, "ID", "FirstName", diplomaThesisTopic.ReviewerID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", diplomaThesisTopic.TeacherID);
            return View(diplomaThesisTopic);
        }

        // POST: SubmitTopic/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ReviewerID,TeacherID,Name,Description,NumberOfStudents,FieldOfStudy,Degree,Availability,Status")] DiplomaThesisTopic diplomaThesisTopic)
        {
            if (ModelState.IsValid)
            {
                db.Entry(diplomaThesisTopic).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ReviewerID = new SelectList(db.Reviewer, "ID", "FirstName", diplomaThesisTopic.ReviewerID);
            ViewBag.TeacherID = new SelectList(db.Teacher, "ID", "FirstName", diplomaThesisTopic.TeacherID);
            return View(diplomaThesisTopic);
        }

        // GET: SubmitTopic/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
            if (diplomaThesisTopic == null)
            {
                return HttpNotFound();
            }
            return View(diplomaThesisTopic);
        }

        // POST: SubmitTopic/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
            db.DiplomaThesisTopic.Remove(diplomaThesisTopic);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
