using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kindergarten
{
    class DBLinker
    {
        private MySqlConnection connection;
        private String connectionInfo = "Database=Kindergarten;Data Source=localhost;User Id=root;Password=max280697";

        public DBLinker()
        {
            CreateConnection();
            TestConnection();
        }

        private void CreateConnection()
        {
            connection = new MySqlConnection(connectionInfo);
        }
        private void TestConnection()
        {
            connection.Open();
            connection.Close();
        }
        public List<String> GetAllGroups()
        {
            String query = "select nameOfGroup from Groupe";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;
            List<String> groups = new List<String>();
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    groups.Add(reader[0].ToString());
                }
                return groups;
            }
            catch (Exception e)
            {
                groups.Add("");
                return groups;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<String> GetAllPrivileges()
        {
            String query = "select nameOfPrivileges from typeOfPrivileges";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;
            List<String> privileges = new List<String>();
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    privileges.Add(reader[0].ToString());
                }
                return privileges;
            }
            catch (Exception e)
            {
                privileges.Add("");
                return privileges;
            }
            finally
            {
                connection.Close();
            }
        }
        public List<Child> GetChildList()
        {
            String query = "select Child.tabNumber, Child.surName, Child.mainName, Child.patronymic, Child.dateOfBirth, " +
                "Child.address, Parents.surNameMother, Parents.mainNameMother, " +
                "Parents.patronymicMother, Parents.surNameFather, Parents.mainNameFather, " +
                "Parents.patronymicFather, typeOfPrivileges.nameOfPrivileges, " +
                "Groupe.nameOfGroup " +
                "from Child join Parents " +
                "on Child.idParents = Parents.idParents " +
                "join typeOfPrivileges " +
                "on Child.idPrivileges = typeOfPrivileges.idPrivileges " +
                "join Groupe " +
                "on Child.idGroup = Groupe.idGroup;";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;
            List<Child> childs = new List<Child>();
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Child child = new Child();
                    child.TabNumber = reader[0].ToString();
                    child.Surname = reader[1].ToString();
                    child.FirstName = reader[2].ToString();
                    child.Patronymic = reader[3].ToString();
                    child.BirthDate = reader[4].ToString().Remove(10);
                    child.Address = reader[5].ToString();
                    child.MotherSurname = reader[6].ToString();
                    child.MotherFirstName = reader[7].ToString();
                    child.MotherPatronymic = reader[8].ToString();
                    child.FatherSurname = reader[9].ToString();
                    child.FatherFirstName = reader[10].ToString();
                    child.FatherPatronymic = reader[11].ToString();
                    child.Privilege = reader[12].ToString();
                    child.Group = reader[13].ToString();
                    String[] visitsInfo = GetVisits(child.TabNumber);
                    if (visitsInfo != null)
                    {                        
                    bool result;
                    int count;
                    result = Int32.TryParse(visitsInfo[2], out count);
                    if (result)
                        child.Visits = count;
                    }
                    childs.Add(child);
                }
                return childs;
            }
            catch (Exception e)
            {
                Child child = new Child();
                childs.Add(child);
                return childs;
            }
            finally
            {
                connection.Close();
            }
        }
        private String[] GetVisits(String tabNumber)
        {
            MySqlConnection connection = new MySqlConnection(connectionInfo);
            String currentMonth = DateTime.Now.Month.ToString();
            String currentYear = DateTime.Now.Year.ToString();
            String query = "select Visiting.currentMonth, Visiting.currentYear, Visiting.numberOfVisitingDays " +
                           "from Child join Visiting " +
                           "on Child.tabNumber = Visiting.tabNumber " +
                           "where Visiting.tabNumber = '" + tabNumber +
                           "' and Visiting.currentMonth = '" + currentMonth +
                           "' and Visiting.currentYear = '" + currentYear + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;
            String[] visitsInfo = new String[3];
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    visitsInfo[0] = reader[0].ToString();
                    visitsInfo[1] = reader[1].ToString();
                    visitsInfo[2] = reader[2].ToString();
                }
                return visitsInfo;
            }
            catch (Exception e)
            {                
                return null;
            }
            finally
            {
                connection.Close();
            }
        }
        public String GetCountOfWorkingDays(Child child)
        {
            String query = "select numberOfWorkingDays " +
                "from Months " +
                "where idMonth = '" + child.CurrentMonth + "'";
            MySqlCommand command = new MySqlCommand(query, connection);
            MySqlDataReader reader = null;
            List<Child> childs = new List<Child>();
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();               
                return reader[0].ToString();
                    
            }
            catch (Exception e)
            {              
                return "0";
            }
            finally
            {
                connection.Close();
            }
        }
        public void ConfirmVisit(String tabNumber)
        {
            String currentMonth = DateTime.Now.Month.ToString();
            String currentYear = DateTime.Now.Year.ToString();
            String query = "update Visiting " +
                "set numberOfVisitingDays = numberOfVisitingDays + 1 " +
                "where tabNumber = '" + tabNumber + "' and currentMonth = '" + currentMonth +
                "' and currentYear = '" + currentYear + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                MessageBox.Show(command.ExecuteNonQuery().ToString());
                MessageBox.Show("Visit confirmed");
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong... Visit was not confirmed");
            }
            finally
            {
                connection.Close();
            }
        }
        public void RemoveChild(String tabNumber)
        {
            String query = "delete from Child " +
                "where tabNumber = '" + tabNumber + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                String query20 = "delete from Visiting where tabNumber = '" + tabNumber + "'";
                command = new MySqlCommand(query20, connection);
                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception e)
                {
                }
                finally
                {
                    connection.Close();
                }
                MessageBox.Show("Data was removed");
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong... Data was not removed");
            }
            finally
            {
                connection.Close();
            }
          
        }
        public void RemoveGroup(String group)
        {
            String query = "delete from Groupe " +
                "where nameOfGroup = '" + group + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Data was removed");
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong... Data was not removed");
            }
            finally
            {
                connection.Close();
            }
        }
        public void RemovePrivilege(String privilege)
        {
            String query = "delete from typeofprivileges " +
                "where nameOfPrivileges = '" + privilege + "';";
            MySqlCommand command = new MySqlCommand(query, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Data was removed");
            }
            catch (Exception e)
            {
                MessageBox.Show("Something went wrong... Data was not removed");
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddPrivilege(String privilege)
        {
            String query1 = "select * from typeofprivileges;";
            MySqlCommand command = new MySqlCommand(query1, connection);
            MySqlDataReader reader = null;
            String lastID = "0";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while(reader.Read())
                    lastID = reader[0].ToString();

            }
            catch (Exception e)
            {
                lastID = "NOT VALID";
            }
            finally
            {
                connection.Close();
            }

            try
            {
                int newID = Int32.Parse(lastID) + 1;
                String query2 = "insert into typeofprivileges values(" + newID.ToString() + ",'" + privilege+"');";
                command = new MySqlCommand(query2, connection);
                connection.Open();
                MessageBox.Show("Privilege was added");
            }
            catch (Exception e)
            {
                MessageBox.Show("Sorry, something went wrong...");
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddGroup(String group)
        {
            String query1 = "select * from Groupe;";
            MySqlCommand command = new MySqlCommand(query1, connection);
            MySqlDataReader reader = null;
            String lastID = "0";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                    lastID = reader[0].ToString();

            }
            catch (Exception e)
            {
                lastID = "NOT VALID";
            }
            finally
            {
                connection.Close();
            }
            try
            {
                int newID = Int32.Parse(lastID) + 1;
                String query2 = "insert into Groupe values(" + newID + ",'" + group + "');";
                command = new MySqlCommand(query2, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Group was added");
            }
            catch (Exception e)
            {
                MessageBox.Show("Sorry, something went wrong");
            }
            finally
            {
                connection.Close();
            }
        }
        public void AddChild(Child child, String motherSurname, String motherFirstName, String motherPatronymic,
            String fatherSurname, String fatherFirstName, String fatherPatronymic, String privilege, String group)
        {
            String query1 = "select idPrivileges from typeofprivileges where nameOfPrivileges = '" + privilege +"'";
            MySqlCommand command = new MySqlCommand(query1, connection);
            MySqlDataReader reader = null;
            String privilegeID = "";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
            while (reader.Read())
            {
                privilegeID = reader[0].ToString();
            }
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid privilege name");
            }
            finally
            {
                connection.Close();
            }
            String query2 = "select idGroup from Groupe where nameOfGroup = '" + group + "'";
            command = new MySqlCommand(query2, connection);
            reader = null;
            String groupID = "";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                groupID = reader[0].ToString();

            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid group name");
            }
            finally
            {
                connection.Close();
            }
            String query20 = "insert into Visiting values ('" + child.TabNumber + "', '" +
                DateTime.Now.Month.ToString() + "', '" + DateTime.Now.Year.ToString() + "', 0)";
            command = new MySqlCommand(query20, connection);
            try
            {
                connection.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
            }
            finally
            {
                connection.Close();
            }
            String query4 = "select idParents from Parents where surNameMother = '" + motherSurname + "'" +
                "and mainNameMother = '" + motherFirstName + "' and patronymicMother = '" + motherPatronymic + "'" +
                " and surNameFather = '" + fatherSurname + "'" +
                "and mainNameFather = '" + fatherFirstName + "' and patronymicFather = '" + fatherPatronymic + "'";
            command = new MySqlCommand(query4, connection);
            reader = null;
            String parentsID = "-";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while(reader.Read())
                parentsID = reader[0].ToString();

            }
            catch (Exception e)
            {
            }
            finally
            {
                connection.Close();
            }
            if (parentsID.Equals("-"))
            {
                String query6 = "select * from Parents;";
                command = new MySqlCommand(query6, connection);
                reader = null;
                String parentstID = "0";
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                        parentsID = (Int32.Parse(reader[0].ToString()) + 1).ToString();

                }
                catch (Exception e)
                {
                    parentsID = "NOT VALID";
                }
                finally
                {
                    connection.Close();
                }
                String query5 = "insert into Parents values (" + parentsID + ", '" + motherSurname + "'" +
               ", '" + motherFirstName + "' , '" + motherPatronymic + "'" +
               " , '" + fatherSurname + "'" +
               ", '" + fatherFirstName + "' , '" + fatherPatronymic + "', '-'," +
               "'-','-','-','-')";
                command = new MySqlCommand(query5, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
            try
            {
                String[] date = child.BirthDate.Split('.');
                String year = date[2];
                String month = date[1];
                String day = date[0];
                DateTime birthDate = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                string mySQLFormatDate = birthDate.ToString("yyyy-MM-dd HH:mm:ss");
                String query3 = "insert into Child values('" + child.TabNumber + "','" + child.Surname +
                    "', '" + child.FirstName +
                    "', '" + child.Patronymic + "','" + mySQLFormatDate + "','" + child.Address +
                    "','" + parentsID + "'," + privilegeID + "," + groupID + ");";
                command = new MySqlCommand(query3, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("New childcard saved in the database");
            }
            catch (Exception e)
            {
                MessageBox.Show("Wrong input");
            }
            finally
            {
                connection.Close();
            }
        }
        public void UpdateChild(Child child, String motherSurname, String motherFirstName, String motherPatronymic,
            String fatherSurname, String fatherFirstName, String fatherPatronymic, String privilege, String group)
        {
            String query1 = "select idPrivileges from typeofprivileges where nameOfPrivileges = '" + privilege + "'";
            MySqlCommand command = new MySqlCommand(query1, connection);
            MySqlDataReader reader = null;
            String privilegeID = "";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while (reader.Read())
                {
                    privilegeID = reader[0].ToString();
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid privilege name");
            }
            finally
            {
                connection.Close();
            }
            String query2 = "select idGroup from Groupe where nameOfGroup = '" + group + "'";
            command = new MySqlCommand(query2, connection);
            reader = null;
            String groupID = "";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                reader.Read();
                groupID = reader[0].ToString();

            }
            catch (Exception e)
            {
                MessageBox.Show("Invalid group name");
            }
            finally
            {
                connection.Close();
            }
            String query4 = "select idParents from Parents where surNameMother = '" + motherSurname + "'" +
                "and mainNameMother = '" + motherFirstName + "' and patronymicMother = '" + motherPatronymic + "'" +
                " and surNameFather = '" + fatherSurname + "'" +
                "and mainNameFather = '" + fatherFirstName + "' and patronymicFather = '" + fatherPatronymic + "'";
            command = new MySqlCommand(query4, connection);
            reader = null;
            String parentsID = "-";
            try
            {
                connection.Open();
                reader = command.ExecuteReader();
                while(reader.Read())
                    parentsID = reader[0].ToString();

            }
            catch (Exception e)
            {
            }
            finally
            {
                connection.Close();
            }
            if(parentsID.Equals("-"))
            {
                String query6 = "select * from Parents;";
                command = new MySqlCommand(query6, connection);
                reader = null;
                String parentstID = "0";
                try
                {
                    connection.Open();
                    reader = command.ExecuteReader();
                    while (reader.Read())
                        parentsID = (Int32.Parse(reader[0].ToString()) + 1).ToString();

                }
                catch (Exception e)
                {
                    parentsID = "NOT VALID";
                }
                finally
                {
                    connection.Close();
                }
                String query5 = "insert into Parents values (" + parentsID + ", '" + motherSurname + "'" +
               ", '" + motherFirstName + "' , '" + motherPatronymic + "'" +
               " ,  '" + fatherSurname + "'" +
               ", '" + fatherFirstName + "' , '" + fatherPatronymic + "', '-'," +
               "'-','-','-','-')";
                connection.Open();
                command = new MySqlCommand(query5, connection);
                command.ExecuteNonQuery();
                connection.Close();
            }
            try
            {
                String[] date = child.BirthDate.Split('.');
                String year = date[2];
                String month = date[1];
                String day = date[0];
                DateTime birthDate = new DateTime(Int32.Parse(year), Int32.Parse(month), Int32.Parse(day));
                string mySQLFormatDate = birthDate.ToString("yyyy-MM-dd HH:mm:ss");
                String query3 = "update Child set surName = '" + child.Surname +
                        "', mainName = '" + child.FirstName +
                        "', patronymic = '" + child.Patronymic + "', dateOfBirth = '" + mySQLFormatDate + 
                        "', address = '" + child.Address +
                        "',idParents = '" + parentsID + "', idPrivileges = " + privilegeID + ", idGroup = " + groupID + 
                        " where tabNumber = '" + child.TabNumber + "';";
                command = new MySqlCommand(query3, connection);
                connection.Open();
                command.ExecuteNonQuery();
                MessageBox.Show("Childcard was successfully updated");
            }
            catch (Exception e)
            {
                MessageBox.Show("Wrong input");
            }
            finally
            {
                connection.Close();
            }
        }
    }
}
