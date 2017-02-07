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
    public class VoteTopicController : Controller
    {
        private DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();

        static private List<int> statusList;
       

        // GET: VoteTopic
        public ActionResult Index()
        {
            statusList = new List<int>();
            var diplomaThesisTopic = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);
            if (diplomaThesisTopic == null)
            {
                TempData["Message"] = "Diploma Thesis topic pool is empty";
            }

            return View(diplomaThesisTopic.ToList());
        }

        // GET: VoteTopic/Edit/5
        public ActionResult Vote(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);

            statusList.Add(diplomaThesisTopic.ID);

            if (diplomaThesisTopic == null)
            {
                return HttpNotFound();
            }

            /*
            diplomaThesisTopic.Status = "Accepted";
            db.Entry(diplomaThesisTopic).State = EntityState.Modified;
            db.SaveChanges();
            */

            return RedirectToAction("VotedIndex");
        }

        public ActionResult VotedIndex()
        {
            //throw new Exception();
            var diplomaThesisTopic = db.DiplomaThesisTopic.Include(d => d.Reviewer).Include(d => d.Teacher);
            List<DiplomaThesisTopic> topicList = diplomaThesisTopic.ToList();
            foreach (var id in statusList)
            {
                topicList.First(d => d.ID == id).Status = "Accepted";
            }
            return View(topicList);
        }

        public ActionResult Confirm()
        {
            foreach (var id in statusList)
            {
                DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
                Vote vote = new Vote();

                vote.DiplomaThesisTopicID = diplomaThesisTopic.ID;
                vote.TeacherID = User.Identity.GetUserId();
                db.Vote.Add(vote);

                diplomaThesisTopic.Status = "Accepted";
                db.Entry(diplomaThesisTopic).State = EntityState.Modified;
                db.SaveChanges();
            }
            TempData["Message"] = "Changes saved successfully";
            return RedirectToAction("Index");
        }

        public ActionResult TConfirm(string userID)
        {
            foreach (var id in statusList)
            {
                DiplomaThesisTopic diplomaThesisTopic = db.DiplomaThesisTopic.Find(id);
                Vote vote = new Vote();

                vote.DiplomaThesisTopicID = diplomaThesisTopic.ID;
                vote.TeacherID = userID;
                db.Vote.Add(vote);

                diplomaThesisTopic.Status = "Accepted";
                db.Entry(diplomaThesisTopic).State = EntityState.Modified;
                db.SaveChanges();
            }
            TempData["Message"] = "Changes saved successfully";
            return RedirectToAction("Index");
        }

    }
}
