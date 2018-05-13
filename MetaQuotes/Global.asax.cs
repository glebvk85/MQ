using MetaQuotes.Storage;
using System.Web;
using System.Web.Http;

namespace MetaQuotes
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            GeobaseStorage.Init(HttpContext.Current.Server.MapPath("~/App_Data/geobase.dat"));
        }
    }
}
