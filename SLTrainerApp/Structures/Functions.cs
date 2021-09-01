using System;
using System.Collections.Generic;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace SLTrainerApp.Structures
{
    static class Funks
    {
        private static readonly object _lock = new object();

        public static readonly Random ran = new Random();

        public static int GetRandom(int x, int y = -1)
        {
            lock (_lock)
            {
                if (y != -1)
                {
                    return ran.Next(x, y);
                }
                else
                {
                    return ran.Next(x);
                }
            }
        }

        public static string getExePath() => $"{Assembly.GetEntryAssembly().Location.Substring(0, Assembly.GetEntryAssembly().Location.Length - 16)}";

        public static void clearTextBoxes(this List<TextBox> list, bool clearText) => list.ForEach(x => { if (clearText) x.Text = ""; x.BackColor = Color.White; });

        public static void boxesIsRunning(this List<TextBox> list, bool isRunnign, TextBox checkBox, Button checkButton)
        {
            list.ForEach(x => x.Enabled = isRunnign);
            checkBox.Enabled = isRunnign;
            checkButton.Enabled = isRunnign;
        }

    }
}
