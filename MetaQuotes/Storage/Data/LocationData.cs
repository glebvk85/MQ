using System.Runtime.InteropServices;

namespace MetaQuotes.Storage.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct LocationData
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
        public sbyte[] country;        // название страны (случайная строка с префиксом "cou_")

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public sbyte[] region;        // название области (случайная строка с префиксом "reg_")

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
        public sbyte[] postal;        // почтовый индекс (случайная строка с префиксом "pos_")

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 24)]
        public sbyte[] city;          // название города (случайная строка с префиксом "cit_")

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public sbyte[] organization;  // название организации (случайная строка с префиксом "org_")

        public float latitude;          // широта

        public float longitude;         // долгота
    }
}