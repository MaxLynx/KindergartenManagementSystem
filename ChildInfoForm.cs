using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kindergarten
{
    public partial class ChildInfoForm : Form
    {
        private ChildInfoController controller;

        public ChildInfoForm(Child child)
        {
            InitializeComponent();
            controller = new ChildInfoController(child);
            label1.Text = controller.Child.Surname;
            label2.Text = controller.Child.FirstName;
            label3.Text = controller.Child.Patronymic;
            label4.Text = controller.Child.BirthDate;
            label5.Text = controller.Child.Group;
            label6.Text = controller.Child.Address;
            label13.Text = controller.Child.MotherSurname;
            label12.Text = controller.Child.MotherFirstName;
            label11.Text = controller.Child.MotherPatronymic;
            label10.Text = controller.Child.FatherSurname;
            label9.Text = controller.Child.FatherFirstName;
            label8.Text = controller.Child.FatherPatronymic;
            label14.Text += controller.Child.Privilege;
            label15.Text += controller.Child.CurrentMonth;
            label16.Text += controller.Child.CurrentYear;
            label17.Text += controller.Child.Visits.ToString();
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter streamWriter = new StreamWriter("D:\\Receipt.txt");
                streamWriter.WriteLine("--Receipt--\r\nTab number: " + controller.Child.TabNumber
                    + "\r\nName: " + controller.Child.Surname + " " + controller.Child.FirstName
                    + "\r\nGroup: " + controller.Child.Group + "\r\nMonth, year: " + controller.Child.CurrentMonth
                    + ", " + controller.Child.CurrentYear + "\r\nTo pay: " + controller.Child.GetToPay());
                streamWriter.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Some problems occured while receipt creation");
            }

        }
    }
}
