using MetaQuotes.Models;
using MetaQuotes.Storage.Data;

namespace MetaQuotes.Utils
{
    public static class ResponseCreator
    {
        public static IPResponse CreateIPResponse(IPData ipData, LocationData locationData)
        {
            return new IPResponse()
            {
                Location = CreateLocation(ipData, locationData, true)
            };
        }

        public static Location CreateLocationItem(IPData ipData, LocationData locationData)
        {
            return CreateLocation(ipData, locationData, false);
        }

        private static Location CreateLocation(IPData ipData, LocationData locationData, bool withCity)
        {
            return new Location()
            {
                City = withCity ? Converter.ConvertToString(locationData.city) : null,
                RangeIp = string.Format("{0} - {1}", Converter.ConvertToIp(ipData.ip_from), Converter.ConvertToIp(ipData.ip_to)),
                Country = Converter.ConvertToString(locationData.country),
                Organization = Converter.ConvertToString(locationData.organization),
                Postal = Converter.ConvertToString(locationData.postal),
                Region = Converter.ConvertToString(locationData.region),
                Longitude = locationData.longitude,
                Latitude = locationData.latitude,
            };
        }
    }
}