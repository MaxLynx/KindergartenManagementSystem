using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kindergarten
{
    public class Child
    {
        public String TabNumber { get; set; }
        public String Surname { get; set; }
        public String FirstName { get; set; }
        public String Patronymic { get; set; }
        public String BirthDate { get; set; }
        public String Address { get; set; }
        public String MotherSurname { get; set; }
        public String MotherFirstName { get; set; }
        public String MotherPatronymic { get; set; }
        public String FatherSurname { get; set; }
        public String FatherFirstName { get; set; }
        public String FatherPatronymic { get; set; }
        public String Privilege { get; set; }
        public String Group { get; set; }
        public String CurrentMonth { get; set; }
        public String CurrentYear { get; set; }
        public Int32 Visits { get; set; }
        public float toPay;

        public Child()
        {
            TabNumber = "?";
            Surname = "?";
            FirstName = "?";
            Patronymic = "?";
            BirthDate = "?";
            Address = "?";
            MotherSurname = "?";
            MotherFirstName = "?";
            MotherPatronymic = "?";
            FatherSurname = "?";
            FatherFirstName = "?";
            FatherPatronymic = "?";
            Privilege = "?";
            Group = "?";
            CurrentMonth = DateTime.Now.Month.ToString();
            CurrentYear = DateTime.Now.Year.ToString();
            Visits = 0;
            toPay = 0;
        }

        public float GetToPay()
        {
            return toPay;
        }
        public void SetToPay(float toPay)
        {
            this.toPay = toPay;
        }

    }
}
