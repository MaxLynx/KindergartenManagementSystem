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
    public partial class AdminRmvForm : Form
    {
        private DBLinker dbLinker;
        public AdminRmvForm()
        {
            InitializeComponent();
            dbLinker = new DBLinker();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            dbLinker.RemoveChild(textBox1.Text);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbLinker.RemovePrivilege(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbLinker.RemoveGroup(textBox3.Text);
        }
    }
}
