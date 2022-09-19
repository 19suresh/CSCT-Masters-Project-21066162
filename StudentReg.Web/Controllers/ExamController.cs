using StudentReg.BLL;
using StudentReg.Cmn.Enums;
using StudentReg.Ent;
using System.Web.Mvc;

namespace StudentReg.Web.Controllers
{
    public class ExamController : Controller
    {
        ExamBLL examBLL;
        SubjectBLL subBLL;
        TeacherBLL teacherBLL;

        // GET: Exam
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Exam exam)
        {
            examBLL = new ExamBLL();

            try
            {
                examBLL.SaveExam(exam);

                TempData["SucessMessage"] = "";
                return RedirectToAction("Create", "Exam", new { examcscs = true });

            }
            catch (System.Exception)
            {

                TempData["ErrMessage"] = "";
                return RedirectToAction("Create", "Exam", new { examcerr = true });

            }

            //return Redirect("Create");
        }

        [HttpPost]
        public ActionResult UpdateExam(Exam subject)
        {
            examBLL = new ExamBLL();

            try
            {
                examBLL.UpdateExam(subject);
                ViewBag.ExamList = examBLL.GetExams();
                return RedirectToAction("ListAll", "Exam", new { updated = true });

            }
            catch (System.Exception)
            {
                return RedirectToAction("ListAll", "Exam", new { error = true });

            }

        }

        public ActionResult ListAll()
        {

            examBLL = new ExamBLL();
            ViewBag.ExamList = examBLL.GetExams();
            return View("ExamList");
        }

        public ActionResult Edit(int id)
        {
            examBLL = new ExamBLL();
            ViewBag.Exam = examBLL.GetExamById(id);
            return View("Edit");
        }

        public ActionResult SearchExam(string searchKey, enumSearchExamBy searchBy)
        {
            examBLL = new ExamBLL();
            ViewBag.ExamList = examBLL.SearchExam(searchKey, searchBy);
            return View("ExamList");
        }

        public bool DeleteExam(int id)
        {
            examBLL = new ExamBLL();
            
            return examBLL.DeleteExamById(id);
        }

        [HttpGet]
        public ActionResult Result()
        {
            teacherBLL = new TeacherBLL();
            ViewBag.Teachers = teacherBLL.GetTeachers();
            return View();
        }

        [HttpPost]
        public JsonResult LoadExams(int year, int grade, int term)
        {
            examBLL = new ExamBLL();
            return Json(examBLL.SearchExam(year, grade, term));
        }

        [HttpPost]
        public JsonResult LoadSubjects(int year, int grade, int term)
        {
            subBLL = new SubjectBLL();
            return Json(subBLL.GetSubjects(year, grade, term));
        }

        [HttpPost]
        public JsonResult SaveResult(ExamResult result)
        {
            examBLL = new ExamBLL();
            var ret = examBLL.InsertResult(result);
            return Json(ret);
        }

        [HttpPost]
        public JsonResult LoadBulkResults(int subjectId, int examId, int grade, int year, int term)
        {
            examBLL = new ExamBLL();
            var res = examBLL.loadBulkResult(subjectId, examId, grade, year, term);
            return Json(res);
        }

        [HttpPost]
        public JsonResult DeleteResult(int id)
        {
            examBLL = new ExamBLL();
            var res = examBLL.DeleteExamResultById(id);
            return Json(res);

        }
    }

}