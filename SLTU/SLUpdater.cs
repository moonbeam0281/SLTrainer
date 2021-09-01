using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SLTU
{
    public partial class SLUpdater : Form
    {
        public SLUpdater()
        {
            InitializeComponent();
            Logger.Text += getExePath();

            var githubToken = "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";
            var url = "https://github.com/moonbeam0281/SLTrainer/archive/";
            var path = @"[local path]";

            using (var client = new System.Net.Http.HttpClient())
            {
                var credentials = string.Format(System.Globalization.CultureInfo.InvariantCulture, "{0}:", githubToken);
                credentials = Convert.ToBase64String(System.Text.Encoding.ASCII.GetBytes(credentials));
                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", credentials);
                var contents = client.GetByteArrayAsync(url).Result;
                System.IO.File.WriteAllBytes(path, contents);
            }
        }


        public static string getExePath() => $"{Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.Length - 8)}";
    }
}
