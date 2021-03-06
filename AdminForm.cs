﻿using System;
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
    public partial class AdminForm : Form
    {
        public AdminForm()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new AdminUpdForm().ShowDialog();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new AdminAddForm().ShowDialog();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new AdminRmvForm().ShowDialog();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new AddVisitForm().ShowDialog();
        }
    }
}
