using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{
    class HomeController
    {
        private DBLinker dbLinker;
        public List<Child> Childs { get; set; }
        private float pricePerDay = 22.0F;
        public List<String> Groups;
        public List<String> Privileges;

        public HomeController()
        {
            dbLinker = new DBLinker();
            Childs = new List<Child>();
            LoadChilds();
            CalculateSum();
            Groups = dbLinker.GetAllGroups();
            Privileges = dbLinker.GetAllPrivileges();
        }

        private void LoadChilds()
        {
            Childs = dbLinker.GetChildList();
        }

        private void CalculateSum()
        {
            foreach(Child child in Childs)
            {
                if(child.Privilege.Equals("No Privileges"))
                    child.SetToPay(Int32.Parse((child.Visits * pricePerDay).ToString()));
                else
                    child.SetToPay(0.0F);
            }
        }

        public Child GetChildByName (String tabNumber)
        {
            foreach(Child child in Childs)
            {
                if (child.TabNumber.Equals(tabNumber))
                    return child;
            }
            return null;
        }
    }
}
