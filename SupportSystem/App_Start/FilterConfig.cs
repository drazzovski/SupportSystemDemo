using System.Web;
using System.Web.Mvc;
//using static SupportSystem.Models.BLL.MainBLL;

namespace SupportSystem
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
