using MetaQuotes.Storage.Data;
using System.Diagnostics;
using System.IO;
using System.Runtime.InteropServices;

namespace MetaQuotes.Storage
{
    public class GeobaseStorage
    {
        private static IPData[] arrayIp;
        private static LocationData[] arrayLocation;
        private static LocationIndexData[] arrayLocationIndex;

        public static IPData[] IpData
        {
            get
            {
                return arrayIp;
            }
        }

        public static LocationData[] LocationData
        {
            get
            {
                return arrayLocation;
            }
        }

        public static LocationIndexData[] LocationIndexData
        {
            get
            {
                return arrayLocationIndex;
            }
        }


        public static void Init(string pathFileGeobase)
        {
            var timewatch = new Stopwatch();
            using (var stream = new FileStream(pathFileGeobase, FileMode.Open))
            {
                // TODO: для увеличения скорости загрузки, можно загрузить файл в буфер целиком и затем из буфера копировать нужные
                timewatch.Start();
                using (var reader = new BinaryReader(stream))
                {
                    HeaderData header;

                    int sizeHeader = Marshal.SizeOf(typeof(HeaderData));
                    var readBuffer = new byte[sizeHeader];
                    readBuffer = reader.ReadBytes(sizeHeader);
                    GCHandle handleHeader = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
                    header = (HeaderData)Marshal.PtrToStructure(handleHeader.AddrOfPinnedObject(), typeof(HeaderData));
                    handleHeader.Free();

                    arrayIp = GetRangeData<IPData>(reader, header);
                    arrayLocation = GetRangeData<LocationData>(reader, header);
                    arrayLocationIndex = GetRangeData<LocationIndexData>(reader, header);
                }
            }
            timewatch.Stop();
            var elapsed = timewatch.ElapsedMilliseconds;
            Trace.WriteLine($"Time  --- {elapsed}");
        }

        private static T[] GetRangeData<T>(BinaryReader reader, HeaderData header)
        {
            var result = new T[header.records];
            int sizeArray = Marshal.SizeOf(typeof(T)) * header.records;
            var readBuffer = new byte[sizeArray];
            readBuffer = reader.ReadBytes(sizeArray);
            GCHandle handle = GCHandle.Alloc(readBuffer, GCHandleType.Pinned);
            var structSize = Marshal.SizeOf(typeof(T));
            var pointer = handle.AddrOfPinnedObject();
            // TODO: для увеличения скорости загрузки, также можно избавиться от цикла
            for (int i = 0; i < header.records; i++)
            {
                result[i] = (T)Marshal.PtrToStructure(pointer, typeof(T));
                pointer += structSize;
            }
            handle.Free();

            return result;
        }
    }
}