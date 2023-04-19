using Proyecto_Web_Ingenieria_de_Software.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software.Filters
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class PermisosModulos : AuthorizeAttribute
    {
        private Users oUser;
        private BeautySalonEntities db = new BeautySalonEntities();
        private int moduloId;

        public PermisosModulos(int moduloId = 0)
        {
            this.moduloId = moduloId;
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            try
            {
                oUser = (Users)HttpContext.Current.Session["User"];
                var ItsMyModulo = from m in db.Permissions
                                  where m.UserID == oUser.ID && m.ModuleID == moduloId && m.estado == true
                                  select m;

                if (ItsMyModulo.ToList().Count() < 1)
                {
                    filterContext.Result = new RedirectResult("~/Home/Index");
                }
            }
            catch (Exception)
            {
                filterContext.Result = new RedirectResult("~/Home/Index");
            }
        }
    }
}