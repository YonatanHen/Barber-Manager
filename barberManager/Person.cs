using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    /// <summary>
    /// Abstract class which represent a person. person can be Client or User
    /// </summary>
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
