using System.Web;
using System.Web.Mvc;

namespace Proyecto_Web_Ingenieria_de_Software
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
