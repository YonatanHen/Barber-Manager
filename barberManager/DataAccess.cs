using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;

namespace barberManager
{
    class DataAccess: IDataAccess
    {
        public SQLiteConnection myConnection;

        public DataAccess()
        {
            myConnection = new SQLiteConnection("Data Source=Users.db");
            Console.WriteLine("Databse connected");
        }

        public List<Person> AddPeople()
        {
            throw new NotImplementedException();
        }

        public List<Person> LoadPeople()
        {
            throw new NotImplementedException();
        }

        public List<Person> RemovePeople()
        {
            throw new NotImplementedException();
        }
    }
}
