﻿using FormInstal.API;
using FormInstal.Trida;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace FormInstal
{
    public class Install
    {
        public static async Task<List<Upload>> GetSearchAsync(string FileName)
        {
            var http = new HttpApi();
            var response = await http.GetFromJsonAsync<List<Upload>>($"/api/File/Search/{FileName}");
            if (response == null)
                return [];
            return response;
        }

        /// <summary>
        /// Download zadaného souboru u unzip na zadanou cestu
        /// </summary>
        public static async Task<bool> Download(string StoredFileName, string Uložit)
        {
            var http = new HttpApi();
            var response = await http.GetAsync($"/api/File/{StoredFileName}");
            if (response.IsSuccessStatusCode)
            {
                //cesta dočasného uložení
                //string zipFilePath = Path.Combine(Path.GetTempPath(), "temp.zip");

                //Stažení proudu dat jako soubor zip
                byte[] fileBytes = await response.Content.ReadAsByteArrayAsync();

                //vytvožení memory stream
                var memoryStream = new MemoryStream(fileBytes);

                //z stream unzip na zadanou cestu
                System.IO.Compression.ZipFile.ExtractToDirectory(memoryStream, Uložit, true);

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}