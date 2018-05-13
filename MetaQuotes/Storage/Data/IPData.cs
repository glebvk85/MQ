using System.Runtime.InteropServices;

namespace MetaQuotes.Storage.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct IPData
    {
        public uint ip_from;           // начало диапазона IP адресов

        public uint ip_to;             // конец диапазона IP адресов

        public uint location_index;     // индекс записи о местоположении
    }
}