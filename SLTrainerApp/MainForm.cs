using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SLTrainerApp.Structures;
using System.Windows.Forms;
using System.Reflection;
using System.IO;
using System.Diagnostics;

namespace SLTrainerApp
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            CreateGrid(ticketInput, true);
            CreateGrid(ticketDisplay, false);
            ticketDisplay = ticketDisplay.Where(x => x.Name != "").ToList();
            ticketInput = ticketInput.Where(x => x.Name != "").ToList();
            ticketInput[1].Focus();
            HashesDataBase = new HashesDb();
            currentBoxFocus = 0;
            numberOfMatch.Focus();
            Process.Start($"{Funks.getExePath()}/SLTU.exe");
            isInPractice = false;
            ticketInput.boxesIsRunning(isInPractice, SumOfTicket, CompareTickets);
        }

        public bool isInPractice { get; set; }

        public static List<TextBox> ticketInput = new List<TextBox>();
        public static List<TextBox> ticketDisplay = new List<TextBox>();
        public static int currentBoxFocus { get; set; }
        public static HashesDb HashesDataBase { get; set; }

        private TextBox CreateGridBox(bool? boxType, int i, int j, int boxSpan, int count, bool isInput, bool isType)
        {
            switch(boxType)
            {
                case null:
                    {
                        return new TextBox
                        {
                            BackColor = Color.LightGray,
                            Enabled = false,
                            Location = new Point(boxSpan * j, 23 * i + 23),
                            Size = new Size(23, 23),
                            TextAlign = HorizontalAlignment.Center,
                            Font = new Font("Segoe UI Symbol", 11, FontStyle.Bold),
                            ForeColor = Color.Black,
                            BorderStyle = BorderStyle.FixedSingle,
                            Text = $"{i + 1}"
                        };
                    }
                case true:
                    {
                        return new TextBox
                        {
                            Name = count.ToString(),
                            Location = new Point(boxSpan * j - 57, 23 * i + 23),
                            BorderStyle = BorderStyle.FixedSingle,
                            Size = new Size(boxSpan, 23),
                            Font = new Font("Segoe UI Symbol", 11, FontStyle.Bold),
                            MaxLength = 4,
                            Enabled = isInput
                        };
                    }
                case false:
                    {
                        return new TextBox
                        {
                            Name = count.ToString(),
                            Location = new Point(boxSpan / 2 * j - 57, 23 * i + 23),
                            BorderStyle = BorderStyle.FixedSingle,
                            Size = new Size(boxSpan / 2, 23),
                            Font = new Font("Segoe UI Symbol", 11, FontStyle.Bold),
                            MaxLength = isType? 1 : 2,
                            Enabled = isInput
                        };
                    }
            }
        }

        private void CreateGrid(List<TextBox> list, bool isInput)
        {
            var count = 0;
            for(var i = 0; i < 50; i++)
            {
                for(var j = 0; j <= (isInput? 7 : 4); j++)
                {
                    switch(j)
                    {
                        case 0:
                            {
                                list.Add(CreateGridBox(null, i, j, 80, count, isInput, j % 2 == 0));
                                continue;
                            }
                        case 1:
                            {
                                list.Add(CreateGridBox(true, i, j, 80, count, isInput, j % 2 == 0));
                                break;
                            }
                        default:
                            {
                                if (isInput)
                                    list.Add(CreateGridBox(false, i, j + 2, 80, count, isInput, j % 2 == 0));
                                else
                                    list.Add(CreateGridBox(true, i, j, 80, count, isInput, j % 2 == 0));
                                break;
                            }
                    }
                    count++;

                }
            }
            foreach (var x in list)
            {
                if(isInput)
                {
                    x.TextChanged += TextChangeGrid;
                    x.KeyPress += EnterPress;
                    x.KeyDown += ArrowKeys;
                    x.GotFocus += IsFocused;
                    TicketInput.Controls.Add(x);
                }
                else
                {
                    TicketDisplay.Controls.Add(x);
                }
                
            }
        }

        private void TextChangeGrid(object sender, EventArgs e)
        {
            var box = (TextBox)sender;
            if (box.Text.Count() >= box.MaxLength)
            {
                currentBoxFocus++;
                SetBoxFocusSelectAll();
            }
        }

        private void IsFocused(object sender, EventArgs e)
        {
            currentBoxFocus = int.Parse(((TextBox)sender).Name);
        }

        private void ArrowKeys(object sender, KeyEventArgs e)
        {
            e.Handled = true;
            switch (e.KeyCode)
            {
                case Keys.Down:
                    {
                        var newOffset = currentBoxFocus - currentBoxFocus % 7 + 7;
                        currentBoxFocus = newOffset > 349 ? 0 : newOffset;
                        SetBoxFocusSelectAll();
                        break;
                    }
                case Keys.Up:
                    {
                        var newOffset = currentBoxFocus - currentBoxFocus % 7 - 7;
                        currentBoxFocus = newOffset < 0 ? 0 : newOffset;
                        SetBoxFocusSelectAll();
                        break;
                    }
                case Keys.Left:
                    {
                        currentBoxFocus = currentBoxFocus - 1 < 0 ? 0 : currentBoxFocus - 1;
                        SetBoxFocusSelectAll();
                        break;
                    }
                case Keys.Right:
                    {
                        currentBoxFocus = currentBoxFocus + 1 > 349 ? 0 : currentBoxFocus + 1;
                        SetBoxFocusSelectAll();
                        break;
                    }
            }
        }

        private void EnterPress(object sender, KeyPressEventArgs e)
        {
            switch(e.KeyChar)
            {
                case (char)Keys.Return:
                    {
                        e.Handled = true;

                        var next = currentBoxFocus - currentBoxFocus % 7 + 7;
                        if (next > 349 || ticketInput.Where(x => x.Text == "" && int.Parse(x.Name) % 7 == 0 && x.Focused).Any())
                        {
                            SumOfTicket.SelectAll();
                            SumOfTicket.Focus();
                            return;
                        }
                        else
                        {
                            var newOffset = currentBoxFocus - currentBoxFocus % 7 + 7;
                            if (newOffset > 349)
                            {
                                SumOfTicket.SelectAll();
                                SumOfTicket.Focus();
                                return;
                            }
                            else
                            {
                                currentBoxFocus = currentBoxFocus - currentBoxFocus % 7 + 7;
                            }
                        }
                        SetBoxFocus();
                        break;
                    }
            }
        }

        public void SetBoxFocus() => ticketInput.First(x => x.Name == $"{currentBoxFocus }").Focus();

        public void SetBoxFocusSelectAll()
        {
            var x = ticketInput.First(x => x.Name == $"{currentBoxFocus }");
            x.SelectAll();
            x.Focus();
        }

        private void Exit_Click(object sender, EventArgs e)
        {
            var dialogResult = MessageBox.Show("Are you sure you want to Exit?", "Exit", MessageBoxButtons.YesNo);
            if (dialogResult == DialogResult.Yes) Environment.Exit(0);
            else if (dialogResult == DialogResult.No) Generate.Focus();
        }

        private void SumOfTicket_KeyPress(object sender, KeyPressEventArgs e)
        {
            var x = (TextBox)sender;
            if (x.Text == "") return;
            if(e.KeyChar == (char)Keys.Return)
            {
                e.Handled = true;
                CompareTickets.Focus();
            }
        }

        private void CompareTickets_Click(object sender, EventArgs e)
        {
            if(SumOfTicket.Text.Length < 2)
            {
                SumOfTicket.SelectAll();
                SumOfTicket.Focus();
                return;
            }
            TicketDisplayLabel.BackColor = Color.Orange;
            ticketDisplay.clearTextBoxes(false);
            ticketInput.clearTextBoxes(false);
            var dispTicket = new Dictionary<string, string>();
            foreach(var x in ticketDisplay.Where(t => t.Text != "" && int.Parse(t.Name) % 4 == 0))
            {
                dispTicket.Add(x.Text, HashesFunctions.GetMatchInfo(ticketDisplay, HashesDataBase, int.Parse(x.Name)).Aggregate((a, b) => $"{a}{b}").TrimEnd('|'));
            }
            var inTicket = new Dictionary<string, string>();
            foreach(var x in ticketInput.Where(t => t.Text != "" && int.Parse(t.Name) % 7 == 0))
            {
                inTicket.Add(x.Text, HashesFunctions.GetMatchInfo(ticketInput, HashesDataBase, int.Parse(x.Name), false).Aggregate("", (a, b) => $"{a}{b}").TrimEnd('|'));
            }


            var missingMatches = dispTicket.Where(t => !inTicket.ContainsKey(t.Key)).ToDictionary(t => t.Key, val => val.Value);
            var wrongMatches = inTicket.Where(t => !dispTicket.ContainsKey(t.Key)).ToDictionary(t=> t.Key);

            var correctMatches = dispTicket.Where(t => inTicket.ContainsKey(t.Key)).ToDictionary(t => t.Key, val => val.Value);

            var hashesWrong = 0;

            foreach (var (key, value) in correctMatches)
            {
                var inpt = (inTicket[key] == "") ? new Dictionary<string, string>() : inTicket[key].Split('|').Select(x => x.Split(';')).ToDictionary(x => x[0], x => x[1]);
                var dist = dispTicket[key].Split('|').Select(x => x.Split(';')).ToDictionary(x => x[0], x => x[1]);
                if (inpt.Count() == 0)
                {
                    dist.Where(t => inpt.ContainsKey(t.Value)).ToList().ForEach(a => ticketDisplay.FirstOrDefault(b => b.Name == $"{a.Value}").BackColor = Color.DeepSkyBlue);
                    correctMatches.Remove(key);
                    wrongMatches.Add(key, new KeyValuePair<string, string>("", ""));
                    continue;
                }

                var tempDisp = dist.Where(x => inpt.ContainsKey(x.Key));
                var tempInp = inpt.Where(x => dist.ContainsKey(x.Key));

                var madeUpShit = inpt.Where(t => !dist.ContainsKey(t.Key));
                var missingShit = dist.Where(t => !inpt.ContainsKey(t.Value));


                foreach(var x in tempInp)
                {
                    var rowNumb = int.Parse(x.Value);
                    ticketInput.FirstOrDefault(b => b.Name == $"{rowNumb}").BackColor = Color.LawnGreen; 
                    ticketInput.FirstOrDefault(b => b.Name == $"{rowNumb + 1}").BackColor = Color.LawnGreen; 
                }
                foreach(var x in madeUpShit)
                {
                    var rowNumb = int.Parse(x.Value);
                    ticketInput.FirstOrDefault(b => b.Name == $"{rowNumb}").BackColor = Color.IndianRed;
                    ticketInput.FirstOrDefault(b => b.Name == $"{rowNumb + 1}").BackColor = Color.IndianRed;
                    hashesWrong++;
                }
                foreach(var x in missingShit)
                {
                    ticketDisplay.FirstOrDefault(b => b.Name == $"{x.Value}").BackColor = Color.DeepSkyBlue;
                }
                foreach (var x in tempDisp)
                {
                    ticketDisplay.FirstOrDefault(b => b.Name == x.Value).BackColor = Color.LawnGreen;
                }


            }

            foreach (var x in missingMatches)
            {
                ticketDisplay.First(b => b.Text == x.Key).BackColor = Color.DeepSkyBlue;
                hashesWrong++;
            }
            foreach(var x in wrongMatches)
            {
                ticketInput.First(t => t.Text == x.Key).BackColor = Color.IndianRed;
                hashesWrong++;
            }
            foreach(var x in correctMatches)
            {
                ticketInput.First(t => t.Text == x.Key).BackColor = Color.LawnGreen;
                ticketDisplay.First(t => t.Text == x.Key).BackColor = Color.LawnGreen;
            }
            if (SumOfTicketDisplayTextBox.Text.Split(':')[1] != SumOfTicket.Text)
            {
                hashesWrong++;
                SumOfTicketDisplayTextBox.BackColor = Color.IndianRed;
            }
            else
            {
                SumOfTicketDisplayTextBox.BackColor = Color.LawnGreen;
            }

            if(hashesWrong == 0)
            {
                TicketDisplayLabel.BackColor = Color.LawnGreen;
                isInPractice = false;
                ticketInput.boxesIsRunning(isInPractice, SumOfTicket, CompareTickets);
                Generate.Focus();
            }
            else
            {
                TicketDisplayLabel.BackColor = Color.IndianRed;
                var t = ticketInput.FirstOrDefault(x => x.BackColor == Color.IndianRed || x.BackColor == Color.DeepSkyBlue);
                if (t == null)
                {
                    SetBoxFocus();
                    return;
                }
                t.SelectAll();
                t.Focus();
            }
        }

        private void Generate_Click(object sender, EventArgs e)
        {
            DialogResult dialogRez = MessageBox.Show("Are you ready?", "Ready Check", MessageBoxButtons.YesNo);
            if (dialogRez == DialogResult.No)
            {
                return;
            }
            TicketDisplayLabel.BackColor = Color.Orange;
            SumOfTicketDisplayTextBox.BackColor = Color.White;
            ticketDisplay.clearTextBoxes(true);
            ticketInput.clearTextBoxes(true);
            HashesFunctions.GenerateTicket(ticketDisplay, HashesDataBase, (int)numberOfMatch.Value, (int)DifficultyLevel.Value, multiHashBox.Checked);
            SumOfTicketDisplayTextBox.Text = $"Sum Of Ticket:{Funks.GetRandom(1, 500) * 10}";
            isInPractice = true;
            ticketInput.boxesIsRunning(isInPractice, SumOfTicket, CompareTickets);
            currentBoxFocus = 0;
            SumOfTicket.Text = "";
            SetBoxFocus();
        }

        private void FillHelpBox()
        {
            DisplayBox.Text = HashesDataBase.Hashes.Aggregate("KAKO SE KUCA | IGRA | TIP", (a, b) => $"{a}" + Environment.NewLine + $"{new StringBuilder().Append($"{b.prefix}").Append(' ', 30 - b.prefix.Length)}{new StringBuilder().Append($"{b.game}").Append(' ', 30 - b.game.Length)}{new StringBuilder().Append($"{b.type}").Append(' ', 30 - b.type.Length)}");
        }

        private void SumOfTicket_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Up)
            {
                SetBoxFocus();
            }
        }
    }
}
