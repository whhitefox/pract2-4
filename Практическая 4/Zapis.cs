using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Практическая_4
{
    internal class Zapis
    {
        public string date;
        public string name { get; set; }
        public string typeName { get; set; }

        private double sum;

        public double Sum
        {
            get { return sum; }
            set 
            {
                if (value < 0)
                {
                    income = false;
                    sum = -value;
                }
                else
                {
                    income = true;
                    sum = value;
                }
            } 
        }

        public bool income { get; set; }

        public Zapis(string date, string name, string typeName, double sum)
        {
            this.date = date;
            this.name = name;
            this.typeName = typeName;
            this.Sum = sum;
        }
    }
}
