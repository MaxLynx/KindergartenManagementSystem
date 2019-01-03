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
    public partial class HomeForm : Form
    {
        private HomeController controller;

        public HomeForm()
        {
            InitializeComponent();
            controller = new HomeController();
            List <Child> childs = controller.Childs;
            int rowCount = childs.Count;
            dataGridView1.ColumnCount = 6;
            dataGridView1.Columns[0].Name = "Tab number";
            dataGridView1.Columns[1].Name = "Surname";
            dataGridView1.Columns[2].Name = "First name";
            dataGridView1.Columns[3].Name = "Patronymic";
            dataGridView1.Columns[4].Name = "Birth date";
            dataGridView1.Columns[5].Name = "Group";
            for (int i = 0; i < rowCount; i++)
            {
                String[] row = new String[6];
                row[0] = childs[i].TabNumber;
                row[1] = childs[i].Surname;
                row[2] = childs[i].FirstName;
                row[3] = childs[i].Patronymic;
                row[4] = childs[i].BirthDate;
                row[5] = childs[i].Group;
                dataGridView1.Rows.Add(row);
            }
            listBox1.Items.Add("All");
            foreach(String group in controller.Groups)
            {
                listBox1.Items.Add(group);
            }
            listBox1.SetSelected(0, true);
            listBox2.Items.Add("All");
            foreach (String privilege in controller.Privileges)
            {
                listBox2.Items.Add(privilege);
            }
            listBox2.SetSelected(0, true);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Child child = controller.GetChildByName(textBox1.Text);
            if(child != null)
                new ChildInfoForm(child).ShowDialog();
            else
                MessageBox.Show("No child with this tab number");
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new WelcomeForm().ShowDialog();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            
        }

        private void button3_Click(object sender, EventArgs e)
        {
            String group = listBox1.SelectedItem.ToString();
            String privilege = listBox2.SelectedItem.ToString();
            dataGridView1.Rows.Clear();
            dataGridView1.Refresh();
            foreach(Child child in controller.Childs)
            {
                if ((group.Equals("All") && privilege.Equals("All"))
                    || (group.Equals("All") && child.Privilege.Equals(privilege))
                    || (group.Equals(child.Group) && privilege.Equals("All"))
                    || (group.Equals(child.Group) && child.Privilege.Equals(privilege)))
                {
                    String[] row = new String[6];
                    row[0] = child.TabNumber;
                    row[1] = child.Surname;
                    row[2] = child.FirstName;
                    row[3] = child.Patronymic;
                    row[4] = child.BirthDate;
                    row[5] = child.Group;
                    dataGridView1.Rows.Add(row);
                }
            }
        }
    }
}
