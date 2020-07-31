using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    class Client : Person
    {
        public Client(string name):base(name) {}
        public string Start { get; set; }
        public string End { get; set; }
        public string Name { get; set; }
        public string Date { get; set; }
    }
}
