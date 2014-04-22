using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProtmusisClock
{
    public partial class ComandsPanel : UserControl
    {

        public int points = 0;

        public ComandsPanel(int index)
        {
            InitializeComponent();
           cName.AppendText(index.ToString());


            cName.GotFocus += (sender, args) => ProtmusisClock.Form1.inactiveShortCuts = true;
            cName.LostFocus += (sender, args) => ProtmusisClock.Form1.inactiveShortCuts = false;

            cPoints.ContextMenuStrip = new ContextMenuStrip();
            cPoints.ContextMenuStrip.Items.Add("Pridėti tašką", Properties.Resources.plus, new EventHandler((o, e) => addPoint()));
            cPoints.ContextMenuStrip.Items.Add("Nuimti tašką", Properties.Resources.minus, new EventHandler((o, e) => subPoint()));
            cPoints.ContextMenuStrip.Items.Add("Color/Uncolor", Properties.Resources.pencil, new EventHandler((o, e) => removeOrAddColor()));
        }

        public void removeOrAddColor()
        {
            if (cPoints.BackColor == this.BackColor)
                colorPoints();
            else
                unColorPoints();
        }

        public void selectAll()
        {

        }

        public void addPoint()
        {
            if (points != 99)
            {
                cPoints.Text = (++points).ToString();
                colorPoints();
            }
        }

        public void subPoint()
        {
            if (points != 0)
            {
                cPoints.Text = (--points).ToString();
                unColorPoints();
            }
        }

        private void colorPoints()
        {
            if (Properties.Settings.Default.ColorsOnPointsAdd)
                cPoints.BackColor = Color.LimeGreen;
        }

        public void unColorPoints()
        {
            cPoints.BackColor = this.BackColor;
        }
    }
}
