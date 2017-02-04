using DiplomaThesisSystemMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using System.Linq;
using Owin;

[assembly: OwinStartupAttribute(typeof(DiplomaThesisSystemMVC.Startup))]
namespace DiplomaThesisSystemMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // uncomment to populate database
            // populateRoles();
            // populateUsers();
            // setSupervisor("Degeorge", 246194);
            // setSupervisor("Degeorge", 592194);
            // setSupervisor("Mcintire", 246821);
            // setSupervisor("Degeorge", 582910);
        }

        private void populateRoles()
        {
            // add new roles if necessary

            createRole("Teacher");
            createRole("Student");
            createRole("Supervisor");
            createRole("Reviewer");
            createRole("User");
            createRole("FacultyCouncilMember");
        }

        private void populateUsers()
        {
            // some test data to work with 

            //Teachers
            addNewUser("teacher1@pwr.edu.pl", "password", "teacher1@pwr.edu.pl", "Teacher");
            addTeacher("teacher1@pwr.edu.pl", "John", "Proctor", "Doctorate", "W8 - Computer Science and Management");

            addNewUser("teacher2@pwr.edu.pl", "password", "teacher2@pwr.edu.pl", "Teacher");
            addTeacher("teacher2@pwr.edu.pl", "Mark", "Pavao", "Doctorate", "W8 - Computer Science and Management");

            addNewUser("teacher3@pwr.edu.pl", "password", "teacher3@pwr.edu.pl", "Teacher");
            addTeacher("teacher3@pwr.edu.pl", "Steven", "Jenneve", "Professor", "W4 - Electronics");

            //Students
            addNewUser("student1@pwr.edu.pl", "password", "student1@pwr.edu.pl", "Student");
            addStudent("student1@pwr.edu.pl", 246194, "Teodoro", "Steinhauer", "Bachelor", 7, "Computer Science");

            addNewUser("student2@pwr.edu.pl", "password", "student2@pwr.edu.pl", "Student");
            addStudent("student2@pwr.edu.pl", 592194, "Wrennie", "Shieber", "Master", 3, "Computer Science");

            addNewUser("student3@pwr.edu.pl", "password", "student3@pwr.edu.pl", "Student");
            addStudent("student3@pwr.edu.pl", 582910, "Ives", "Callahan", "Bachelor", 6, "Computer Science");

            addNewUser("student4@pwr.edu.pl", "password", "student4@pwr.edu.pl", "Student");
            addStudent("student4@pwr.edu.pl", 482014, "Wendye", "Ozols", "Bachelor", 6, "Electronics");

            addNewUser("student5@pwr.edu.pl", "password", "student5@pwr.edu.pl", "Student");
            addStudent("student5@pwr.edu.pl", 246821, "Kyle", "Chiang", "Bachelor", 6, "Electronics");

            //Reviewers
            addNewUser("reviewer1@pwr.edu.pl", "password", "reviewer1@pwr.edu.pl", "Reviewer");
            addReviewer("reviewer1@pwr.edu.pl", "Valentine", "Harriman", "Professor");

            addNewUser("reviewer2@pwr.edu.pl", "password", "reviewer2@pwr.edu.pl", "Reviewer");
            addReviewer("reviewer2@pwr.edu.pl", "Wilmer", "Jander", "Professor");

            //Supervisors
            addNewUser("supervisor1@pwr.edu.pl", "password", "supervisor1@pwr.edu.pl", "Supervisor");
            addSupervisor("supervisor1@pwr.edu.pl", "Ethelda", "Degeorge", "Doctorate", "W8 - Computer Science and Management");

            addNewUser("supervisor2@pwr.edu.pl", "password", "supervisor2@pwr.edu.pl", "Supervisor");
            addSupervisor("supervisor2@pwr.edu.pl", "Filberto", "Mcintire", "Professor", "W4 - Electronics");

            //Faculty Council Members
            addNewUser("facultycouncilmember1@pwr.edu.pl", "password", "facultycouncilmember1@pwr.edu.pl", "FacultyCouncilMember");
            addTeacher("facultycouncilmember1@pwr.edu.pl", "Belita", "Humenek", "Doctorate", "W8 - Computer Science and Management");

            addNewUser("facultycouncilmember2@pwr.edu.pl", "password", "facultycouncilmember2@pwr.edu.pl", "FacultyCouncilMember");
            addTeacher("facultycouncilmember2@pwr.edu.pl", "Gregorius", "Ferrandino", "Doctorate", "W8 - Computer Science and Management");

            addNewUser("facultycouncilmember3@pwr.edu.pl", "password", "facultycouncilmember3@pwr.edu.pl", "FacultyCouncilMember");
            addTeacher("facultycouncilmember3@pwr.edu.pl", "Bennie", "Day", "Professor", "W4 - Electronics");




        }

        private void createRole(string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            if (!roleManager.RoleExists(roleName))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }

        private void addNewUser (string username, string password, string mail, string roleName)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            if (userManager.Find(username, password) == null){

                var user = new ApplicationUser();
                user.UserName = username;
                user.Email = mail;

                var chkUser = userManager.Create(user, password);

                if (chkUser.Succeeded)
                {
                    userManager.AddToRole(user.Id, roleName);
                }
            } 
        }

        private void addTeacher(string username, string firstName, string lastName, string title, string department)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = userManager.FindByName(username);
            string userID = currentUser.Id;

            var entities = new DiplomaThesisSystemDB();
            if (entities.Teacher.Find(userID) == null)
            {
                Teacher newTeacher = new Teacher();
                newTeacher.ID = userID;
                newTeacher.FirstName = firstName;
                newTeacher.LastName = lastName;
                newTeacher.Title = title;
                newTeacher.Department = department;
                newTeacher.isSupervisor = 0;
                entities.Teacher.Add(newTeacher);
                entities.SaveChanges();
            }

        }

        private void addSupervisor(string username, string firstName, string lastName, string title, string department)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = userManager.FindByName(username);
            string userID = currentUser.Id;

            var entities = new DiplomaThesisSystemDB();
            if (entities.Teacher.Find(userID) == null)
            {
                Teacher newTeacher = new Teacher();
                newTeacher.ID = userID;
                newTeacher.FirstName = firstName;
                newTeacher.LastName = lastName;
                newTeacher.Title = title;
                newTeacher.Department = department;
                newTeacher.isSupervisor = 1;
                entities.Teacher.Add(newTeacher);
                entities.SaveChanges();
            }
        }

        private void addStudent(string username, int studentID, string firstName, string lastName, string degree, int semester, string fieldOfStudy)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = userManager.FindByName(username);
            string userID = currentUser.Id;

            var entities = new DiplomaThesisSystemDB();
            if (entities.Student.Find(userID) == null)
            {
                Student newStudent = new Student();
                newStudent.ID = userID;
                newStudent.StudentID = studentID;
                newStudent.FirstName = firstName;
                newStudent.LastName = lastName;
                newStudent.Degree = degree;
                newStudent.Semester = semester;
                newStudent.FieldOfStudy = fieldOfStudy;
                entities.Student.Add(newStudent);
                entities.SaveChanges();
            }
        }

        private void addReviewer(string username, string firstName, string lastName, string title)
        {
            ApplicationDbContext context = new ApplicationDbContext();
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
            ApplicationUser currentUser = userManager.FindByName(username);
            string userID = currentUser.Id;

            var entities = new DiplomaThesisSystemDB();
            if (entities.Reviewer.Find(userID) == null)
            {
                Reviewer newReviewer = new Reviewer();
                newReviewer.ID = userID;
                newReviewer.FirstName = firstName;
                newReviewer.LastName = lastName;
                newReviewer.Title = title;
                entities.Reviewer.Add(newReviewer);
                entities.SaveChanges();
            }
        }
        
        private void setSupervisor(string superLastName, int studentID)
        {
            var entities = new DiplomaThesisSystemDB();
            Teacher supervisor = entities.Teacher.Single(Teacher => Teacher.LastName == superLastName);
            Student student = entities.Student.Single(Student => Student.StudentID == studentID);
            student.SupervisorID = supervisor.ID;
            entities.SaveChanges();
        }
    }
}
