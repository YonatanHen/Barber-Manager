using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace barberManager
{
    interface IDataAccess
    {
        bool isPersonExist(string username, string password);
        bool isAppointmentPossible(string date,string start,string end);
        void AddPerson(string textQuery, string tableName);
        DataTable getData(string tableName);
        DataTable getData(string tableName,string date);
        List<Person> RemovePeople();
    }
}
