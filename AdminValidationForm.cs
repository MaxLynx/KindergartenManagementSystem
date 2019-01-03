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
    public partial class AdminValidationForm : Form
    {
        public AdminValidationForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(AdminValidator.Validate(textBox1.Text, textBox2.Text))
            {
                this.Dispose();
                new AdminForm().ShowDialog();
            }
            else
            {
                MessageBox.Show("Not valid. Please check what you've entered");
            }
        }
    }
}
