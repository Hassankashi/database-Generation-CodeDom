using System.Web;
using System.Web.Mvc;

namespace MVCNHibernate
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            try
            {
                filters.Add(new HandleErrorAttribute());
            }
            //catch (System.Exception)
            catch
            {
                
                //throw;
            }
           
        }
    }
}