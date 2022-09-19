using StudentReg.BLL;
using StudentReg.Cmn.Enums;
using StudentReg.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentReg.Web.Controllers
{
    public class StudentController : Controller
    {

        StudentBLL studentBLL;

        public ActionResult Register()
        {
            return View();
        }

        public ActionResult SaveStudent(Student student)
        {
            studentBLL = new StudentBLL();
            try
            {
                studentBLL.SaveStudent(student);
                return RedirectToAction("Register", "Student", new { stdcscc = true });

            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Student", new { stdcerr = true });
            }
        }

        public ActionResult UpdateStudent(Student student)
        {
            studentBLL = new StudentBLL();

            try
            {
                studentBLL.UpdateStudent(student);
                ViewBag.StudentList = studentBLL.GetStudents();
                return RedirectToAction("StudentList", "Student", new { stduscc = true });
            }
            catch (Exception)
            {
                return RedirectToAction("StudentList", "Student", new { stduerr = true });
            }

        }

        public ActionResult ListAll()
        {
            studentBLL = new StudentBLL();
            ViewBag.StudentList = studentBLL.GetStudents();
            return View("StudentList");
        }

        public ActionResult Edit(int id)
        {
            studentBLL = new StudentBLL();
            ViewBag.Student = studentBLL.GetStudentById(id);
            return View("Edit");
        }

        public ActionResult SearchStudent(string searchKey, enumSearchTeacherBy searchBy)
        {
            studentBLL = new StudentBLL();
            ViewBag.StudentList = studentBLL.SearchStudents(searchKey,searchBy);
            return View("StudentList");
        }

        [HttpPost]
        public JsonResult LoadStudents(int grade)
        {
            studentBLL = new StudentBLL();
            var jsn = studentBLL.GetStudentByGrade(grade);

            return Json(jsn, JsonRequestBehavior.AllowGet);
        }
    }
}