namespace MetaQuotes.Utils
{
    public static class Converter
    {
        public static string ConvertToIp(uint ipAddress)
        {
            var octet = new uint[4];
            for (int i = 0; i < 4; i++)
            {
                octet[i] = (ipAddress >> (i * 8)) & 0xFF;
            }
            return $"{octet[3]}.{octet[2]}.{octet[1]}.{octet[0]}";
        }

        public static string ConvertToString(sbyte[] arr)
        {
            string returnStr;
            unsafe
            {
                fixed (sbyte* fixedPtr = arr)
                {
                    returnStr = new string(fixedPtr);
                }
            }

            return returnStr;
        }
    }
}