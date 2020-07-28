using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    class Client : Person
    {
        DateTime appointment;
        string name;
        public Client(DateTime appointment, string name)
        {
            this.appointment = appointment;
            this.name = name;
        }

        //If client exists, just set new values for him.
        DateTime Appointment{get;set;}
        string Name { get; set; }
    }
}
