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
    public class AssignmentController : Controller
    {
        SubjectBLL subjectBLL;
        AssignmentBLL assignmentBLL;
        TeacherBLL teacherBLL;


        // GET: Assignment
        public ActionResult Create()
        {
            subjectBLL = new SubjectBLL();
            ViewBag.SubjectList = subjectBLL.GetSubjects();
            return View();
        }

        [HttpPost]
        public ActionResult Create(Assignment assignment)
        {
            assignmentBLL = new AssignmentBLL();

            try
            {
                assignmentBLL.SaveAssignment(assignment);
                return RedirectToAction("Create", "Assignment", new { asncscs = true });

            }
            catch (Exception)
            {
                return RedirectToAction("Create", "Assignment", new { asncerr = true });
            }

        }

        [HttpPost]
        public ActionResult UpdateAssignment(Assignment assignment)
        {
            assignmentBLL = new AssignmentBLL();
            try
            {
                assignmentBLL.UpdateAssignment(assignment);
                ViewBag.AssignmentList = assignmentBLL.GetAssignment();
                return RedirectToAction("ListAll", "Assignment", new { asnuscs = true });

            }
            catch (Exception)
            {
                return RedirectToAction("ListAll", "Assignment", new { asnuerr = true });
            }
        }

        public ActionResult ListAll()
        {
            assignmentBLL = new AssignmentBLL();
            ViewBag.AssignmentList = assignmentBLL.GetAssignment();
            return View("AssignmentList");
        }

        public ActionResult Edit(int id)
        {
            assignmentBLL = new AssignmentBLL();
            subjectBLL = new SubjectBLL();

            ViewBag.SubjectList = subjectBLL.GetSubjects();
            ViewBag.Assignment = assignmentBLL.GetAssignmentById(id);
            return View("Edit");
        }

        public ActionResult SearchAssignment(string searchKey, enumSearchAssignmentBy searchBy)
        {
            assignmentBLL = new AssignmentBLL();
            ViewBag.ExamList = assignmentBLL.SearchAssignment(searchKey, searchBy);
            return View("AssignmentList");
        }



        [HttpGet]
        public ActionResult Result()
        {
            teacherBLL = new TeacherBLL();
            ViewBag.Teachers = teacherBLL.GetTeachers();
            return View();
        }

        [HttpPost]
        public JsonResult LoadAssignments(int year, int grade, int term)
        {
            assignmentBLL = new AssignmentBLL();
            return Json(assignmentBLL.SearchAssignment(year, grade, term));
        }

        [HttpPost]
        public JsonResult SaveResult(ExamResult result)
        {
            assignmentBLL = new AssignmentBLL();
            
            try
            {
                return Json(assignmentBLL.InsertResult(result));
            }
            catch (Exception ex)
            {

                return Json(ex.Message);
            }
            
        }

        [HttpPost]
        public JsonResult LoadSubjects(int year, int grade, int term)
        {
            subjectBLL = new SubjectBLL();
            return Json(subjectBLL.GetSubjects(year, grade, term));
        }

        [HttpPost]
        public JsonResult LoadBulkResults(int subjectId, int examId, int grade, int year, int term)
        {
            assignmentBLL = new AssignmentBLL();
            var res = assignmentBLL.loadBulkResult(subjectId, examId, grade, year, term);
            return Json(res);
        }

        [HttpPost]
        public bool DeleteAssignment(int id)
        {
            assignmentBLL = new AssignmentBLL();
            var res = assignmentBLL.DeleteAssignmentById(id);
            return res;
        }
    }
}