using MetaQuotes.Models;
using MetaQuotes.Providers;
using MetaQuotes.Services;
using System.Web.Http;

namespace MetaQuotes.Controllers
{
    [RoutePrefix("city")]
    public class CityController : ApiController
    {
        [HttpGet]
        [Route("locations")]
        public LocationsResponse GetLocations(string city = "")
        {
            var cacheProvider = new MemCacheProvider<LocationsResponse>();
            string cacheKey = $"city;{city}";
            var response = cacheProvider.TryGetResponse(cacheKey);

            if (response == null)
            {
                var service = new SearchService();
                response = service.FindByCity(city);
                cacheProvider.SaveResponse(cacheKey, response);
            }

            return response;
        }
    }
}
