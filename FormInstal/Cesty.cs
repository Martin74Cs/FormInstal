

using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
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
            //string jsonString = JsonConvert.SerializeObject(moje, Newtonsoft.Json.Formatting.Indented);
            var format = new JsonSerializerOptions
            {
                PropertyNamingPolicy = null,
                WriteIndented = true,
                AllowTrailingCommas = true,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
            };
            string jsonString = System.Text.Json.JsonSerializer.Serialize(moje, format);
            File.WriteAllText(cesta, jsonString);
        }

        public static T LoadJson<T>(string cesta) where T : class
        {
            if (File.Exists(cesta))
            {
                string jsonString = File.ReadAllText(cesta);
                var format = new JsonSerializerOptions
                {
                    PropertyNamingPolicy = null,
                    WriteIndented = true,
                    AllowTrailingCommas = true,
                    DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull,
                };
                var res = System.Text.Json.JsonSerializer.Deserialize<T>(jsonString, format);
                return res;
            }
            return null;

        }
    }
}
