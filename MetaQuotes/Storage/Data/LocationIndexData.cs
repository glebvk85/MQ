using System.Runtime.InteropServices;

namespace MetaQuotes.Storage.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LocationIndexData
    {
        public uint location_index;     // индекс записи о местоположении
    }
}