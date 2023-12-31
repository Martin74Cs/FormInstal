﻿using FormInstal.API;
using FormInstal.Trida;
using IWshRuntimeLibrary;
using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Drawing;
using System.ServiceModel.Syndication;

namespace FormInstal
{
    public partial class Form1 : Form
    {
        public InstalInfo info { get; set; }
        public Form1()
        {
            InitializeComponent();
        }

        private async void Instal_ClickAsync(object sender, EventArgs e)
        {

            //cesta kam bude provedena instalace
            string Cesta = label2.Text;

            //provedení instalace na zadanou cestu
            var zip = await Install.GetSearchAsync(info);
            //var zip = await Install.GetSearchAsync("instal.zip");
            if (zip.Count < 1)
            {
                MessageBox.Show($"Chyba hledání souboru v RestApi\nSoubor pravděpodobně není nahrán");
                Close();
                Environment.Exit(0);
            }
                var Akt = MenuInstal.Aktualizuj();

                //melo by vždy existovat
                if (!Directory.Exists(Cesta))
                    Directory.CreateDirectory(Cesta);
                else
                {
                    //PŘI PRVNÍ INSTALACI NESMÍ ADRESAŘ EXISTOVAT.
                    MessageBox.Show($"Adresár pro instalaci již existuje\nProgram je již instalován\nBude UKONČENO");
                    Close();
                    Environment.Exit(0);
                }

                //string Cesta = Path.Combine(Cesty.AppData, "Autodesk");
                //if (!Directory.Exists(Path.GetDirectoryName(info.InstalPath)))
                if (!Directory.Exists(Cesta))
                {
                    MessageBox.Show($"Adresař {Cesta} nebyl nalezen\nProgram bude UKONČEN");
                    Close();
                    Environment.Exit(0);
                }

                //posledni vracený soubor z rest api který je uložen v databazi
                string RandomFilename = zip.Last().StoredFileName ?? "";

                if (!await Install.Download(RandomFilename, Cesta))
                {
                    MessageBox.Show($"Chyba pri extrakci souboru: {RandomFilename}");
                    Akt.Close();
                    Close();
                    return;
                }
                //Na zadané ceste by měly existovat zadané soubory.

                //Nactení manifestu z restApi
                //ProgramInfo Nova = await HttpApi.DownloadFile<ProgramInfo>($"api/file/manifest");

                //uložení manifestu do cesty instalce
                //Nova.SaveJson(Cesta);

                Akt.Close();
                Instal.Visible = false;
                BStart.Visible = true;
      
            //Close();
            return;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            string Json = Path.Combine(Cesty.AktualniAdresar, "nastaveni.json");
            info = Cesty.LoadJson<InstalInfo>(Json);
            if (info == null)
            {
                info = new InstalInfo()
                { DownloadUrl = HttpApi.IP(), ReleaseDate = DateTime.Now, InstalPath = Cesty.AppData, StartFile= "WFForm.exe", InstalFile = "Elektro.Bundle.zip", Version = "1.1.1" };
                info.SaveJson(Json);
            }

            if (System.IO.File.Exists(Path.Combine(info.InstalPath, info.StartFile)))
            { 
                Instal.Visible = false;
                BStart.Visible = true;
            }
            else 
            {
                Instal.Visible = true;
                BStart.Visible = false;
            }

            //Cesta = Path.Combine(Cesta, "ApplicationPlugins");
            //if (!Directory.Exists(Cesta))
            //    { MessageBox.Show("Autocad plugins nebyl nalezen"); Close(); return; }

            //Pokud neexistuje bude vztvořeno
            //Cesta = Cesta.AddCesta("Elektro.bundle");
            //Cesta = Cesta.AddCesta("Test.bundle");
            //požitá cesta pro instalaci

            label2.Text = info.InstalPath;
        }

        private void BStart_Click(object sender, EventArgs e)
        {
            string Cesta = Path.Combine(info.InstalPath, info.StartFile);
            string shortcutPath = Environment.GetFolderPath(Environment.SpecialFolder.DesktopDirectory) + @"\TeZak.lnk";
            string iconPath = Path.Combine(Cesty.AktualniAdresar, "StorageFile.ico");

            WshShell shell = new WshShell();
            IWshShortcut link = (IWshShortcut)shell.CreateShortcut(shortcutPath);
            link.TargetPath = Cesta;
            link.IconLocation = iconPath;
            link.Save();
            MessageBox.Show("Byl vytvořen zásupce");

            if (System.IO.File.Exists(Cesta))
            { 
                Process.Start(Cesta, "");
                Environment.Exit(0);
            }
            
            else
            { 
                MessageBox.Show($"Soubor {info.StartFile} nebyl nalezen"); 
                Close(); 
                return; 
            }
            label2.Text = Cesta;
        }
    }
}
