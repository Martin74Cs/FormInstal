using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FormInstal
{
    public static class Cesty
    {
        public static string AktualniAdresar => Environment.CurrentDirectory;
        public static string AppData => Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);

        public static string AddCesta(this string Cesta, string Slozka)
        {
            string CestaVysledek = Path.Combine(Cesta, Slozka);
            if (!Directory.Exists(CestaVysledek))
                Directory.CreateDirectory(CestaVysledek);
            return CestaVysledek;
        }

        public static void SaveJson<T>(this T moje, string cesta)
        {
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje);
            File.WriteAllText(cesta, jsonString);
        }
    }
}
