using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    interface IDataAccess
    {
        bool isPersonExist(string username, string password);
        List<Person> AddPeople();
        List<Person> RemovePeople();
    }
}
