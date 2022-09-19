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
    public class SubjectController : Controller
    {
        SubjectBLL subjectBLL;

        // GET: Subject
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Subject subject)
        {
            subjectBLL = new SubjectBLL();
            try
            {
                subjectBLL.SaveSubject(subject);
                return RedirectToAction("Create", "Subject", new { subcscc = true });
            }
            catch (Exception)
            {

                return RedirectToAction("Create", "Subject", new { subcerr = true });

            }

        }

        public ActionResult UpdateSubject(Subject subject)
        {
            subjectBLL = new SubjectBLL();


            try
            {

                subjectBLL.UpdateSubject(subject);
                ViewBag.SubjectList = subjectBLL.GetSubjects();
                return RedirectToAction("SubjectList", "Subject", new { subuscc = true });
            }
            catch (Exception)
            {

                return RedirectToAction("SubjectList", "Subject", new { subuerr = true });

            }
        }

        public ActionResult ListAll()
        {
            subjectBLL = new SubjectBLL();
            ViewBag.SubjectList = subjectBLL.GetSubjects();
            return View("SubjectList");
        }

        public ActionResult Edit(int id)
        {
            subjectBLL = new SubjectBLL();
            ViewBag.Subject = subjectBLL.GetSubjectById(id);
            return View("Edit");
        }

        public ActionResult SearchStudent(string searchKey, enumSearchSubjectBy searchBy)
        {
            subjectBLL = new SubjectBLL();
            ViewBag.SubjectList = subjectBLL.SearchSubject(searchKey, searchBy);
            return View("SubjectList");
        }

        public ActionResult DeleteSubject(int id)
        {
            subjectBLL = new SubjectBLL();
            subjectBLL.DeleteSubjectById(id);
            return View("SubjectList");
        }

    }
}