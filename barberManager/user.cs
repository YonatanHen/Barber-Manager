using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    /// <summary>
    /// Class represent a user that can reach to the system. 
    /// </summary>
    public class User : Person
    {
        private string password;
        public User(string password, string uname):base(uname)
        {
            this.password = password;
        }
        public string Password {
                get { return password; } 
                set { password = value; } 
             }
        public string Uname {
                get { return Name; }
                set { Name = value; }
            }
        }
}
