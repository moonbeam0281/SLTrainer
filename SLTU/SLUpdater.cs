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
        }


        public static string getExePath() => $"{Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.Length - 8)}";
    }
}
