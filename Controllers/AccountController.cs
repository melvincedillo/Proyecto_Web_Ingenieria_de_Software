using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Controllers
{
    public class AccountController : Controller
    {
        // GET: Account
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(string User, string Pass)
        {
            try
            {
                using (Models.BeautySalonEntities db = new Models.BeautySalonEntities())
                {
                    var oUser = (from d in db.Users
                                 where d.UserName == User.Trim() && d.UserPassword == Pass.Trim()
                                 select d).FirstOrDefault();
                    if(oUser == null)
                    {
                        ViewBag.Error = "Usuario o contraseña invalidos";
                        return View();
                    }

                    Session["User"] = oUser;
                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
        }

        public ActionResult Logout()
        {
            Session["User"] = null;
            return RedirectToAction("Login", "Account");
        }
    }
}