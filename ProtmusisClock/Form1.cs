using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.Collections.Generic;
using System.Media;
using System.Resources;

namespace ProtmusisClock
{
    public partial class Form1 : Form
    {
        private int timeLeft = Properties.Settings.Default.Time, milsLeft = 0, formWidth;
        private int openHeight;
        private bool addKey = false, subKey = false;
        public static bool inactiveShortCuts = false;
        System.Timers.Timer timer;
        

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HT_CAPTION = 0x2;

        [DllImportAttribute("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,
                         int Msg, int wParam, int lParam);

        [DllImportAttribute("user32.dll")]
        public static extern bool ReleaseCapture();

        public Form1()
        {
            InitializeComponent();
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.SizableToolWindow;
            this.Text = string.Empty;
            this.ControlBox = false;
            formWidth = this.Width;
            Application.ThreadException += (sender, args) => MessageBox.Show(args.Exception.Message);

        }

        private void unColorControls()
        {
            if(Properties.Settings.Default.ColorsOnPointsAdd)
                foreach (ComandsPanel cp in panel.Controls)
                    cp.unColorPoints();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            updateClock(timeLeft, 0);
            timer = new System.Timers.Timer(92);
            timer.Elapsed += timer_Tick;
            openHeight = this.Height;
        }

        private void sortAllCommands()
        {
            List<ComandsPanel> cp = new List<ComandsPanel>();

            foreach (ComandsPanel a in panel.Controls)
            {
                cp.Add(a);
            }

            cp.Sort(delegate(ComandsPanel a, ComandsPanel b) { return b.points.CompareTo(a.points); });
           
            panel.Controls.Clear();

            foreach (ComandsPanel c in cp)
            {
                panel.Controls.Add(c);
            }
        }

        private void updateClock(int sec, int mil)
        {
            if (sec == 5 && mil == 0)
                clock.ForeColor = Color.Red;
                    
            clock.Text = (sec / 60).ToString() + ":" + (sec % 60).ToString().PadLeft(2, '0') + "." +
                         mil.ToString();
        }


        private void Form1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                clock.Focus();
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
            else if (e.Button == MouseButtons.Right)
            {
                rightClickMenu.Show(Cursor.Position.X, Cursor.Position.Y);
            }
        }
        private void timer_Tick(object sender, EventArgs e)
        {
            if (--milsLeft < 0)
            {
                milsLeft = 9;
                --timeLeft;
            }

            this.Invoke((MethodInvoker)delegate
            {

                if (timeLeft < 0)
                {
                    timer.Stop();
                    timeLeft = Properties.Settings.Default.Time;
                    milsLeft = 0;
                    updateClock(0, 0);

                    unColorControls();
                }
                else
                {
                    updateClock(timeLeft, milsLeft);
                }
            });

           if (milsLeft == 0 && Properties.Settings.Default.Sounds)
           {
               if (timeLeft <= 5 && timeLeft > 0)
               {
                   SoundPlayer sp = new SoundPlayer(Properties.Resources.short_beep);
                   sp.Play();
               }
               else if (timeLeft == 0)
               {
                   SoundPlayer sp = new SoundPlayer(Properties.Resources.finish);
                   sp.Play();
               }
           }

        }

        private void Form1_KeyDown(object sender, KeyEventArgs e)
        {
            if (inactiveShortCuts && e.KeyCode == Keys.Enter)
            {
                clock.Focus();
                e.Handled = true;
            }



            if (inactiveShortCuts)
                return;

            switch (e.KeyCode)
            {
                case Keys.S:
                    sortAllCommands();
                    e.Handled = true;
                    break;
                case Keys.Add:
                    addKey = true;
                    e.Handled = true;
                    break;
                case Keys.Oemplus:
                    addKey = true;
                    e.Handled = true;
                    break;
                case Keys.Subtract:
                    subKey = true;
                    e.Handled = true;
                    break;
                case Keys.OemMinus:
                    subKey = true;
                    e.Handled = true;
                    break;

                case Keys.Space:
                    if (timeLeft == Properties.Settings.Default.Time)
                        clock.ForeColor = Color.Black;
                    timer.Enabled = !timer.Enabled;
                    e.Handled = true;
                    break;
                case Keys.R:
                    timer.Stop();
                    clock.ForeColor = Color.Black;
                    timeLeft = Properties.Settings.Default.Time;
                    milsLeft = 0;
                    updateClock(timeLeft, milsLeft);
                    e.Handled = true;
                    break;
                case Keys.OemOpenBrackets:
                    if (panel.Controls.Count < 21)
                    {
                        panel.Controls.Add(new ComandsPanel(panel.Controls.Count + 1));
                        this.Height += 32;
                    }
                    e.Handled = true;
                    break;
                case Keys.OemCloseBrackets:
                    if (panel.Controls.Count > 0)
                    {
                        panel.Controls.RemoveAt(panel.Controls.Count - 1);
                        this.Height -= 32;
                    }
                    e.Handled = true;
                    break;
                case Keys.H:
                    if (this.Height == openHeight)
                        this.Height += panel.Controls.Count * 32;
                    else
                    {
                        this.Height = openHeight;
                        unColorControls();
                        if(Properties.Settings.Default.Sort)
                            sortAllCommands();
                    }
                    
                        e.Handled = true;
                    break;
            }

            //Points adding, subtracting
            if (e.KeyCode >= Keys.NumPad1 && e.KeyCode <= Keys.NumPad9)
            {
                if (e.KeyCode - Keys.NumPad1 < panel.Controls.Count)
                {
                    int index = e.KeyCode - Keys.NumPad1;

                    if (addKey && !subKey)
                        (panel.Controls[index] as ComandsPanel).addPoint();
                    
                    else if (!addKey && subKey)
                        (panel.Controls[index] as ComandsPanel).subPoint();
                }
                e.Handled = true;
            }
            else if (e.KeyCode >= Keys.D1 && e.KeyCode <= Keys.D9)
            {
                if (e.KeyCode - Keys.D1 < panel.Controls.Count)
                {
                    int index = e.KeyCode - Keys.D1;

                    if (addKey && !subKey)
                        (panel.Controls[index] as ComandsPanel).addPoint();
                    else if (!addKey && subKey)
                        (panel.Controls[index] as ComandsPanel).subPoint();
                }
                e.Handled = true;
            }

            //Handling with Funtion keys:
            if (e.KeyCode >= Keys.F1 && e.KeyCode <= Keys.F12)
            {
                if (e.KeyCode - Keys.F1 + 9  < panel.Controls.Count)
                {
                    int index = e.KeyCode - Keys.F1 + 9;

                    if (addKey && !subKey)
                        (panel.Controls[index] as ComandsPanel).addPoint();

                    else if (!addKey && subKey)
                        (panel.Controls[index] as ComandsPanel).subPoint();
                }
                e.Handled = true;
            }


        }

        private void mouseKeyEventProvider1_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                    case Keys.Add:
                        addKey = false;
                    break;
                    case Keys.Subtract:
                        subKey = false;
                    break;
            }
        }

        private void checkIfEnterClick(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                clock.Focus();
            }

            e.Handled = true;
        }

        private void Form1_ResizeEnd(object sender, EventArgs e)
        {
            this.Width = formWidth;
        }

        private void showSettings()
        {
            Settings settings = new Settings();
            settings.FormClosing += (o, args) =>
            {
                Properties.Settings.Default.Save();
            };
            settings.FormClosed += (o, args) =>
            {
                inactiveShortCuts = false;
            };
            inactiveShortCuts = true;
            Application.Run(settings);

            
        }


        private void rightClickMenu_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            switch (e.ClickedItem.Text)
            {
                case "Properties":
                    try
                    {
                        System.Threading.Thread settingsForm = new System.Threading.Thread(showSettings);
                        settingsForm.Start();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                    break;
                case "Help":
                    Help help = new Help();
                    help.Show();
                    break;
                case "Exit":
                    Application.Exit();
                   break;
            }
            }
        }
    }