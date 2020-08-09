using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    public abstract class Person
    {
        private string name;
        public Person(string name) { this.name = name; }
        public string Name
        {
            get { return name; }
            set { name = value; }
        }
    }
}
