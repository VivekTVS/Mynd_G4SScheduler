using G4SScheduler.Repository;
using G4SScheduler.Utils;
using G4SScheduler.ViewModel;
using System;
using G4SScheduler.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace G4SScheduler.Controllers
{
    public class HomeController : Controller
    {
        [AllowAnonymous]
        [Route("~/", Name = "Defaultloginview")]
        [Route("login", Name = "loginview")]
        public ActionResult Login()
        {
            Scheduler scheduler = new Scheduler();
             //scheduler.UPdateSFTPTID(3720744, LDofPM);
            if (Session["uBo"] != null)
            {
               
                return RedirectToAction("Index", "Home");
                 
                // return RedirectToAction("DashBoard", "DashBoard", new { area = "Report" });

            }
            else
            {
                return View();
            }

        }
        public ActionResult Index()
        {
            //  Scheduler obj = new Scheduler();
            //   obj.TransferG4SFile();
            //  obj.CreateZipfile();
            // obj.CopyToSFTP("", "");
           
            return View();
            
        }


        [HttpPost]
        [ValidateAntiForgeryToken()]
        [Route("login", Name = "login")]
        public ActionResult Login(LoginVM User)
        {
            try
            {

                if (ModelState.IsValid)
                {

                    TransferSFTPRepo ObjURep = new TransferSFTPRepo();
                    User ObjU = new User();

                    String Message = "";
                    ObjU = ObjURep.Authenticate(User, ref Message);

                    if (Message == "success")
                    {
                         
                        //   ObjURep.LogLogin(ObjU.UID);
                        Session["uBo"] = ObjU;
                        // int uid = ((User)Session["uBo"]).UID;

                        return RedirectToAction("Index", "Home");

                    }
                    //Invalid Password.
                    else if (Message == "Invalid Password.")
                    {
                        Message = "Invalid user name or password.";
                        
                        ModelState.AddModelError("", Message);
                        return View(User);
                    }
                    
                    
                    else
                    {
                        ModelState.AddModelError("", "Invalid user name or password.");
                        return View(User);
                    }
                }

                else
                    return View(User);
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        //    [ValidateAntiForgeryToken()]

        [Route("FileTF", Name = "FileTF")]
        public ActionResult FileTF()
        {
            Response res = new Response();
            try
            {
                int Year = 3;
                if (Year > 2)
                {
                    Scheduler obj = new Scheduler();
                   obj.TransferG4SFile();
                  //  obj.TransferG4SFiletest();
                    res.IsSuccess = true;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    res.IsSuccess = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            // return View();
        }


        [HttpPost]
        //    [ValidateAntiForgeryToken()]

        [Route("CreateZIP", Name = "CreateZIP")]
        public ActionResult CreateZIP()
        {
            Response res = new Response();
            try
            {
                int Year = 3;
                if (Year > 2)
                {
                    Scheduler obj = new Scheduler();
              res.Message= obj.CreateZipfile();
                    res.IsSuccess = true;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    res.IsSuccess = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            // return View();
        }



        [HttpPost]
        //    [ValidateAntiForgeryToken()]

        [Route("CopyToSFTP", Name = "CopyToSFTP")]
        public ActionResult CopyToSFTP()
        {
            Response res = new Response();
            try
            {
                int Year = 3;
                if (Year > 2)
                {
                    Scheduler obj = new Scheduler();
                    res.Message = obj.CopyToSFTP("");
                    res.IsSuccess = true;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    res.IsSuccess = false;
                    return Json(res, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                res.IsSuccess = false;
                res.Message = ex.Message;
                return Json(res, JsonRequestBehavior.AllowGet);
            }
            // return View();
        }


        [Route("loginout", Name = "loginout")]
        public ActionResult Loginout()
        {
            Session.Clear();
            Session.Abandon();
            Session.RemoveAll();

            if (Request.Cookies["ASP.NET_SessionId"] != null)
            {
                Response.Cookies["ASP.NET_SessionId"].Value = string.Empty;
                Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddMonths(-20);
            }

            return RedirectToAction("Login", "Home");
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
    }
}