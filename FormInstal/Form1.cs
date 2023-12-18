
using FormInstal.API;
using FormInstal.Trida;

namespace FormInstal
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private async void Instal_ClickAsync(object sender, EventArgs e)
        {
            var Akt = MenuInstal.Aktualizuj();
            //provedení instalace na zadanou cestu
            var zip = await Install.GetSearchAsync("AcadElektro.zip");
            //var zip = await Install.GetSearchAsync("instal.zip");
            if (zip.Count < 1)
            {
                MessageBox.Show($"Chyba hledání souboru v RestApi\nSoubor pravdepodobne není nahrán");
                Akt.Close();
                Close();
                return;
            }

            //posledni vracený soubor z rest api který je uložen v databazi
            string RandomFilename = zip.Last().StoredFileName ?? "";

            //cesta kam bude provedena instalace
            string Cesta = label2.Text;

            //melo by vždy existovat
            if (!Directory.Exists(Cesta))
            {
                Directory.CreateDirectory(Cesta);
            }

            if (!await Install.Download(RandomFilename, Cesta))
            {
                MessageBox.Show($"Chyba pri extrakci souboru: {RandomFilename}");
                Akt.Close();
                Close();
                return;
            }
            //Na zadané ceste by měly existovat zadané soubory.

            //Nactení manifestu z restApi
            ProgramInfo Nova = await HttpApi.DownloadFile<ProgramInfo>($"api/file/manifest");

            //uložení manifestu do cesty instalce
            Nova.SaveJson(Cesta);
            Akt.Close();
            return;
        }
    }
}
