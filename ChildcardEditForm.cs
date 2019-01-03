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
    public partial class ChildcardEditForm : Form
    {
        private DBLinker dbLinker;
        private Child child;
        private Boolean New { get; set; }
        public ChildcardEditForm(String tabNumber)
        {
            InitializeComponent();
            dbLinker = new DBLinker();
            List <Child> allChilds = dbLinker.GetChildList();
            if (!tabNumber.Equals("New"))
            {
                New = false;
  //              try
  //              {
                    foreach (Child kid in allChilds)
                        if (kid.TabNumber.Equals(tabNumber))
                        {
                            this.child = kid;
                            break;
                        }
                    DisplayChild();
  /*              }
                catch(Exception e)
                {
                    MessageBox.Show("Child with this tab number was not found");
                    this.Dispose();
                }*/
            }
            else
            {
                New = true;
                child = new Child();
                child.TabNumber = (Int32.Parse(allChilds[allChilds.Count - 1].TabNumber) + 1).ToString();
            }
        }

        private void DisplayChild()
        {
            textBox1.Text = child.Surname;
            textBox2.Text = child.FirstName;
            textBox3.Text = child.Patronymic;
            textBox4.Text = child.BirthDate;
            textBox5.Text = child.Address;
            textBox6.Text = child.Group;
            textBox7.Text = child.Privilege;
            textBox9.Text = child.MotherSurname;
            textBox10.Text = child.MotherFirstName;
            textBox11.Text = child.MotherPatronymic;
            textBox12.Text = child.FatherSurname;
            textBox13.Text = child.FatherFirstName;
            textBox14.Text = child.FatherPatronymic;

        }

        private void textBox7_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox12_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            child.Surname = textBox1.Text;
            child.FirstName = textBox2.Text;
            child.Patronymic = textBox3.Text;
            child.BirthDate = textBox4.Text;
            child.Address = textBox5.Text;
            if(New)
            dbLinker.AddChild(child, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text,
                textBox13.Text, textBox14.Text, textBox7.Text, textBox6.Text);
            else
                dbLinker.UpdateChild(child, textBox9.Text, textBox10.Text, textBox11.Text, textBox12.Text,
                textBox13.Text, textBox14.Text, textBox7.Text, textBox6.Text);
        }
    }
}
