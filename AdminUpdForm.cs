using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kindergarten
{
    public partial class AdminUpdForm : Form
    {
        public AdminUpdForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {

        }

        private void button2_Click_1(object sender, EventArgs e)
        {
            try
            {
                new ChildcardEditForm(textBox1.Text).ShowDialog();
            }
            catch(Exception exc)
            {
                new AdminForm().ShowDialog();
            }
        }
    }
}
