using StudentReg.BLL;
using StudentReg.Ent;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace StudentReg.Web.Controllers
{
    public class HomeController : Controller
    {

        AssignmentBLL assignmentBLL;
        ExamBLL examBLL;


        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login()
        {
            return View();
        }


        public ActionResult AllResults()
        {
            return View();
        }


        [HttpPost]
        public JsonResult LoadAllAssingmentResults(int year, string regno)
        {
            assignmentBLL = new AssignmentBLL();
            var res = assignmentBLL.loadAllResults(year, regno);
            return Json(res);
        }

        [HttpPost]
        public JsonResult LoadAllExamResults(int year, string regno)
        {
            examBLL = new ExamBLL();
            var res = examBLL.loadAllExamResult(year, regno);
            return Json(res);
        }

        [HttpPost]
        public JsonResult Authenticate(string userName, string password)
        {
            SRegResponse res = null;
            try
            {
                res = new UserBLL().Login(userName, password);

                if (res.ResStatus == true)
                {
                    Session["user"] = res.ResData1;
                    Session["menus"] = res.ResData2;
                }
                else
                {
                    Session["user"] = null;
                    Session["menus"] = null;
                    //Response.Status = "error";
                }
                //Response.StatusCode = 200;
                //Response.Status = "success 123";

                return Json(res);
            }
            catch (Exception ex)
            {

                Session["user"] = null;
                Session["menus"] = null;

                throw ex;
            }
            //return Json(res);
        }

        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Home");
        }
    }
}