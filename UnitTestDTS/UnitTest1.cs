using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Web;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using DiplomaThesisSystemMVC.Controllers.Diploma_Thesis_Management;
using System.Web.Mvc;
using DiplomaThesisSystemMVC.Models;
using System.Diagnostics;

namespace UnitTestDTS
{
    // Make sure that database is populated with script before testing
    // Otherwise change ID of users
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void BrowseTopicTest1()
        {
            BrowseTopicController controller = new BrowseTopicController();
            var result = controller.Index() as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void BrowseTopicTest2()
        {
            BrowseTopicController controller = new BrowseTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            diplomaThesisTopic.NumberOfStudents = null;
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Degree = null;
            diplomaThesisTopic.Availability = null;
            var result = controller.Filter(diplomaThesisTopic) as ViewResult;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SubmitTopicTest1()
        {
            //set up
            SubmitTopicController controller = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;


            //test
            controller.TCreate(diplomaThesisTopic, userID);

            //check
            Assert.IsNotNull(db.DiplomaThesisTopic.Select(d => d.Name == diplomaThesisTopic.Name));

            //cleanup
            DiplomaThesisTopic topicToRemove = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            db.DiplomaThesisTopic.Remove(topicToRemove);
            db.SaveChanges();


        }

        [TestMethod]
        public void SubmitTopicTest2()
        {
            //set up
            SubmitTopicController controller = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            int dbTopicCount = db.DiplomaThesisTopic.Count();

            //test
            controller.TCreate(diplomaThesisTopic, userID);
            controller.TCreate(diplomaThesisTopic, userID);

            //check
            Assert.AreEqual(dbTopicCount+1, db.DiplomaThesisTopic.Count());

            //cleanup
            DiplomaThesisTopic topicToRemove = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            db.DiplomaThesisTopic.Remove(topicToRemove);
            db.SaveChanges();
        }

        [TestMethod]
        public void SubmitTopicTest3()
        {
            //set up
            SubmitTopicController controller = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            int dbTopicCount = db.DiplomaThesisTopic.Count();

            //test
            controller.TCreate(diplomaThesisTopic, userID);
            diplomaThesisTopic.NumberOfStudents = "4";
            controller.TCreate(diplomaThesisTopic, userID);
            diplomaThesisTopic.Description = "Other Description";
            controller.TCreate(diplomaThesisTopic, userID);

            //check
            Assert.AreEqual(dbTopicCount + 1, db.DiplomaThesisTopic.Count());

            //cleanup
            DiplomaThesisTopic topicToRemove = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            db.DiplomaThesisTopic.Remove(topicToRemove);
            db.SaveChanges();
        }

        [TestMethod]
        public void VoteTopicTest1()
        {
            //set up
            VoteTopicController voteController = new VoteTopicController();
            SubmitTopicController submitController = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            submitController.TCreate(diplomaThesisTopic, userID);
            DiplomaThesisTopic topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            int topicID = topic.ID;

            //test  
            voteController.Index();
            voteController.Vote(topicID);
            voteController.TConfirm(userID);

            //check
            db = new DiplomaThesisSystemDB();
            topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            Assert.AreEqual("Accepted", topic.Status);

            //cleaning
            Vote voteToRemove = db.Vote.First(d => d.DiplomaThesisTopicID == topic.ID);
            db.DiplomaThesisTopic.Remove(topic);
            db.Vote.Remove(voteToRemove);
            db.SaveChanges();

        }

        [TestMethod]
        public void VoteTopicTest2()
        {
            //set up
            VoteTopicController voteController = new VoteTopicController();
            SubmitTopicController submitController = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            submitController.TCreate(diplomaThesisTopic, userID);
            DiplomaThesisTopic topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            int topicID = topic.ID;

            //test  
            voteController.Index();
            voteController.Vote(topicID);

            //check
            db = new DiplomaThesisSystemDB();
            topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            Assert.AreEqual("NotVoted", topic.Status);

            //cleaning
            db.DiplomaThesisTopic.Remove(topic);
            db.SaveChanges();

        }

        [TestMethod]
        public void VoteTopicTest3()
        {
            //set up
            VoteTopicController voteController = new VoteTopicController();
            SubmitTopicController submitController = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            submitController.TCreate(diplomaThesisTopic, userID);
            DiplomaThesisTopic topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            int topicID = topic.ID;

            //test  
            voteController.Index();
            voteController.Vote(topicID);
            voteController.Vote(topicID);
            voteController.Vote(topicID);

            //check
            db = new DiplomaThesisSystemDB();
            topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            Assert.AreEqual("NotVoted", topic.Status);

            //cleaning
            db.DiplomaThesisTopic.Remove(topic);
            db.SaveChanges();

        }

        [TestMethod]
        public void VoteTopicTest4()
        {
            //set up
            VoteTopicController voteController = new VoteTopicController();
            SubmitTopicController submitController = new SubmitTopicController();
            DiplomaThesisTopic diplomaThesisTopic = new DiplomaThesisTopic();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            diplomaThesisTopic.NumberOfStudents = "3";
            diplomaThesisTopic.FieldOfStudy = "Computer Science";
            diplomaThesisTopic.Name = "TestTopic1";
            diplomaThesisTopic.Description = "TestDescription1";
            diplomaThesisTopic.Degree = "Master";
            Teacher teacher = db.Teacher.First();
            string userID = teacher.ID;
            submitController.TCreate(diplomaThesisTopic, userID);
            DiplomaThesisTopic topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            int topicID = topic.ID;

            //test  
            voteController.Index();
            voteController.Vote(topicID);
            voteController.Index();
            voteController.TConfirm(userID);

            //check
            db = new DiplomaThesisSystemDB();
            topic = db.DiplomaThesisTopic.First(d => d.Name == diplomaThesisTopic.Name);
            Assert.AreEqual("NotVoted", topic.Status);

            //cleaning
            db.DiplomaThesisTopic.Remove(topic);
            db.SaveChanges();

        }

        [TestMethod]
        public void CheckGradeTest1()
        {
            //set up
            CheckGradeController checkGradeController = new CheckGradeController();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            Student stud = db.Student.First(d => d.ReviewID != null);
            string userID = stud.ID;

            //test
            ActionResult result = checkGradeController.TIndex(userID);

            //check
            Assert.AreEqual(null, checkGradeController.TempData["Message"]);

        }

        [TestMethod]
        public void CheckGradeTest2()
        {
            //set up
            CheckGradeController checkGradeController = new CheckGradeController();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            Student stud = db.Student.First(d => d.DiplomaThesisID == null);
            string userID = stud.ID;

            //test
            ActionResult result = checkGradeController.TIndex(userID);

            //check
            Assert.AreEqual("Work not submitted", checkGradeController.TempData["Message"]);

        }

        [TestMethod]
        public void CheckGradeTest3()
        {
            //set up
            CheckGradeController checkGradeController = new CheckGradeController();
            DiplomaThesisSystemDB db = new DiplomaThesisSystemDB();
            var studList = db.Student.Where(d => d.DiplomaThesisID != null);
            Student stud = studList.First(d => d.Review.Content == null);
            string userID = stud.ID;
            Debug.Print(userID);
            //test
            ActionResult result = checkGradeController.TIndex(userID);

            //check
            Assert.AreEqual("Reviewing in progress", checkGradeController.TempData["Message"]);
        }



    }
}
