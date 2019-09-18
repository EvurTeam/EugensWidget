using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EugensWidget
{
    public partial class TicTacToe : Form
    {
        char[] cells;

        public TicTacToe()
        {
            InitializeComponent();
            cells = new[] { '#', '#', '#', '#', '#', '#' };
            foreach (Control item in Controls)
            {
                if (item is Label)
                {
                    item.Click += Item_Click;
                }
            }
        }

        private void Item_Click(object sender, EventArgs e)
        {
            var label = sender as Label;
            var name = label.Name;
            var num = int.Parse(name.Replace("label","")) - 1;
            Text = num.ToString();
        }
    }
}
