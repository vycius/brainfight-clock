using System;
using System.Windows.Forms;

namespace ProtmusisClock
{
    public partial class Settings : Form
    {
        public Settings()
        {
            InitializeComponent();
            numericUpDown1.Value = Properties.Settings.Default.Time;
            checkBox1.Checked = Properties.Settings.Default.ColorsOnPointsAdd;
            checkBox2.Checked = Properties.Settings.Default.Sounds;
            checkBox3.Checked = Properties.Settings.Default.Sort;

            numericUpDown1.ValueChanged += (sender, args) =>
            {
                Properties.Settings.Default.Time = Convert.ToInt32(numericUpDown1.Value);
            };

            checkBox1.CheckedChanged += (sender, args) =>
            {
                Properties.Settings.Default.ColorsOnPointsAdd = checkBox1.Checked;
            };

            checkBox2.CheckedChanged += (sender, args) =>
            {
                Properties.Settings.Default.Sounds = checkBox2.Checked;
            };

            checkBox3.CheckedChanged += (sender, args) =>
            {
                Properties.Settings.Default.Sort = checkBox3.Checked;
            };

        }
    }
}
