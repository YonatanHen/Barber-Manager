using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    interface IDataAccess
    {
        List<Person> LoadPeople();
        List<Person> AddPeople();
        List<Person> RemovePeople();
    }
}
