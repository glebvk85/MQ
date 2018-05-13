using System.Runtime.InteropServices;

namespace MetaQuotes.Storage.Data
{
    [StructLayout(LayoutKind.Sequential, Pack = 1)]
    public struct HeaderData
    {
        public int version;           // версия база данных

        [MarshalAs(UnmanagedType.ByValArray, SizeConst = 32)]
        public sbyte[] name;          // название/префикс для базы данных

        public ulong timestamp;         // время создания базы данных

        public int records;           // общее количество записей

        public uint offset_ranges;     // смещение относительно начала файла до начала списка записей с геоинформацией

        public uint offset_cities;     // смещение относительно начала файла до начала индекса с сортировкой по названию городов

        public uint offset_locations;  // смещение относительно начала файла до начала списка записей о местоположении
    }
}