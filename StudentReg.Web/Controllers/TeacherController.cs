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
    public class TeacherController : Controller
    {
        
        TeacherBLL teacherBLL;

        // GET: Teacher
        public ActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SaveTeacher(Teacher teacher)
        {
            teacherBLL = new TeacherBLL();
            try
            {
                teacherBLL.SaveTeacher(teacher);
                return RedirectToAction("Register", "Teacher", new { tchcscc = true });

            }
            catch (Exception)
            {
                return RedirectToAction("Register", "Teacher", new { tchcerr = true });
            }
        }

        public ActionResult UpdateTeacher(Teacher teacher)
        {
            teacherBLL = new TeacherBLL();
            try
            {
                teacherBLL.UpdateTeacher(teacher);
                ViewBag.TeacherList = teacherBLL.GetTeachers();
                return RedirectToAction("TeachersList", "Teacher", new { tchuscc = true });
            }
            catch (Exception)
            {
                return RedirectToAction("TeachersList", "Teacher", new { tchuerr = true });
            }

        }

        [HttpGet]
        public ActionResult ListAll()
        {
            teacherBLL = new TeacherBLL();
            ViewBag.TeacherList = teacherBLL.GetTeachers();
            return View("TeachersList");
        }

        public ActionResult Edit(int id)
        {
            teacherBLL = new TeacherBLL();
            ViewBag.Teacher = teacherBLL.GetTeacherById(id);
            return View("Edit");
        }

        public ActionResult SearchTeacher(string searchKey, enumSearchTeacherBy searchBy)
        {
            teacherBLL = new TeacherBLL();
            ViewBag.TeacherList = teacherBLL.SearchTeachers(searchKey, searchBy);
            return View("TeachersList");
        }

    }
}