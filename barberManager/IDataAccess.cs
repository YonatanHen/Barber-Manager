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
        bool isUserExist(string username, string password);
        bool isAppointmentExist(string name, string date, string start, string end);
        bool isAppointmentPossible(string date,string start,string end);
        void AddPerson(string textQuery, string tableName);
        DataTable getData(string tableName);
        DataTable getData(string tableName,string date);
        bool RemovePeople(string name, string date, string start, string end);
        bool updateAppointment(string name, string date, string start, string end, string field, string value, string tableName);
    }
}
