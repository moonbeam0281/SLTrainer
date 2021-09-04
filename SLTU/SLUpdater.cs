using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLTU
{
    public partial class SLUpdater : Form
    {

        private static readonly string githubToken = "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";
        public SLUpdater()
        {
            InitializeComponent();
            Logger.Text += getExePath() + Environment.NewLine;
            Logger.Text += $"{getExePath()}NewVersion.txt" + Environment.NewLine;
            Logger.Text += $"{getExePath()}DataBase\\Version.txt" + Environment.NewLine;
        }

        private void DownloadVersion()
        {
            var url = "https://raw.githubusercontent.com/moonbeam0281/SLTrainer/main/SLTrainerApp/DataBase/Version.txt";
            var pathToSave = $"{getExePath()}DataBase\\NewVersion.txt";
            var pathOfCurrent = $"{getExePath()}DataBase\\Version.txt";

            using (var client = new HttpClient())
            {
                var credentials = string.Format(CultureInfo.InvariantCulture, "{0}:", githubToken);
                credentials = Convert.ToBase64String(Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                var contents = client.GetByteArrayAsync(new Uri(url)).Result;
                File.WriteAllBytes(pathToSave, contents);
            }
            Logger.Text += $"New: {File.ReadAllText($"{pathToSave}")}";
            Logger.Text += $"Current: {File.ReadAllText($"{pathOfCurrent}")}";
        }

        public static string getExePath() => $"{Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.Length - 8)}";

        private void StartDownload_Click(object sender, EventArgs e)
        {
            DownloadVersion();
        }
    }
}
