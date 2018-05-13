using MetaQuotes.Models;
using MetaQuotes.Providers;
using MetaQuotes.Services;
using System.Web.Http;

namespace MetaQuotes.Controllers
{
    [RoutePrefix("ip")]
    public class IpController : ApiController
    {
        [HttpGet]
        [Route("location")]
        public IPResponse GetLocation(string ip = "")
        {
            var cacheProvider = new MemCacheProvider<IPResponse>();
            string cacheKey = $"ip;{ip}";
            var response = cacheProvider.TryGetResponse(cacheKey);

            if (response == null)
            {
                var service = new SearchService();
                response = service.FindByIp(ip);
                cacheProvider.SaveResponse(cacheKey, response);
            }

            return response;
        }
    }
}
