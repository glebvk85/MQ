using MetaQuotes.Models;
using MetaQuotes.Storage;
using MetaQuotes.Utils;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace MetaQuotes.Services
{
    public class SearchService
    {
        // делитель, для преобразования индексов в третьем массиве с данными
        private const int sizeLocationElement = 96;

        public IPResponse FindByIp(string searchAddress)
        {
            IPAddress ipAddress;
            if (!IPAddress.TryParse(searchAddress, out ipAddress))
                return null;
            var ipBytes = ipAddress.GetAddressBytes();
            uint ip = (uint)ipBytes[3];
            ip += (uint)ipBytes[2] << 8;
            ip += (uint)ipBytes[1] << 16;
            ip += (uint)ipBytes[0] << 24;

            int startIndex = 0;
            int finishIndex = GeobaseStorage.IpData.Length;
            while (true)
            {
                int averageIndex = startIndex + (finishIndex - startIndex) / 2;
                if (averageIndex == startIndex || averageIndex == finishIndex)
                    return new IPResponse();
                if (ip >= GeobaseStorage.IpData[averageIndex].ip_from && ip <= GeobaseStorage.IpData[averageIndex].ip_to)
                {
                    var location = GeobaseStorage.LocationData[GeobaseStorage.IpData[averageIndex].location_index];
                    return ResponseCreator.CreateIPResponse(GeobaseStorage.IpData[averageIndex], location);
                }
                if (ip < GeobaseStorage.IpData[averageIndex].ip_to)
                    finishIndex = averageIndex;
                else
                    startIndex = averageIndex;
            }
        }

        public LocationsResponse FindByCity(string cityName)
        {
            return new LocationsResponse() { Locations =  GetLocations(cityName).ToArray() };
        }

        private Location GetLocation(int index)
        {
            var location = GeobaseStorage.LocationData[GeobaseStorage.LocationIndexData[index].location_index / sizeLocationElement];
            var ipInfo = GeobaseStorage.IpData[GeobaseStorage.LocationIndexData[index].location_index / sizeLocationElement];
            return ResponseCreator.CreateLocationItem(ipInfo, location);
        }
        
        private string GetCityName(int index)
        {
            return Converter.ConvertToString(GeobaseStorage.LocationData[GeobaseStorage.LocationIndexData[index].location_index / sizeLocationElement].city);
        }

        private IEnumerable<Location> GetLocations(string cityName)
        {
            int startIndex = 0;
            int finishIndex = GeobaseStorage.IpData.Length;
            while (true)
            {
                int averageIndex = startIndex + (finishIndex - startIndex) / 2;
                if (averageIndex == startIndex || averageIndex == finishIndex)
                    yield break;
                var currentCityName = GetCityName(averageIndex);

                if (string.Compare(currentCityName, cityName, false) == 0)
                {
                    yield return GetLocation(averageIndex);

                    // проверяем элементы справа и слева от найденного, они тоже могут удовлетворять критерию поиска
                    int currentIndex = averageIndex;
                    while (currentIndex > 0)
                    {
                        currentIndex--;
                        currentCityName = GetCityName(currentIndex);
                        if (string.Compare(currentCityName, cityName, false) == 0)
                            yield return GetLocation(currentIndex);
                        else
                            break;
                    }
                    
                    while (averageIndex < GeobaseStorage.IpData.Length)
                    {
                        averageIndex++;
                        currentCityName = GetCityName(averageIndex);
                        if (string.Compare(currentCityName, cityName, false) == 0)
                            yield return GetLocation(averageIndex);
                        else
                            break;
                    }
                    break;
                }
                if (string.Compare(currentCityName, cityName, false) > 0)
                    finishIndex = averageIndex;
                else
                    startIndex = averageIndex;
            }
        }

    }
}