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
    public partial class AdminAddForm : Form
    {
        private DBLinker dbLinker;
        public AdminAddForm()
        {
            InitializeComponent();
            dbLinker = new DBLinker();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            dbLinker.AddPrivilege(textBox2.Text);
        }

        private void button3_Click(object sender, EventArgs e)
        {
            dbLinker.AddGroup(textBox3.Text);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new ChildcardEditForm("New").ShowDialog();
        }
    }
}
