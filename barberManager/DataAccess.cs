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
        public SQLiteConnection conn;

        public DataAccess()
        {
            conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db");
            Console.WriteLine("Database connected");
        }

        public List<Person> AddPeople()
        {
            throw new NotImplementedException();
        }

        public bool isPersonExist(string username,string password)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            SQLiteDataReader sqlite_datareader;
            SQLiteCommand sqlite_cmd;
            sqlite_cmd = conn.CreateCommand();
            sqlite_cmd.CommandText = "SELECT * FROM users WHERE username='"+username+"' AND "+"password="+password;
            try
            {
                sqlite_datareader = sqlite_cmd.ExecuteReader();
            }
            catch(Exception ex)
            {
                return false;
            }
            while (sqlite_datareader.Read())
            {
                string myreader = sqlite_datareader.GetString(0);
                Console.WriteLine(myreader);
            }
            conn.Close();
            if (sqlite_datareader!=null) return true;
            return false;
    }

        public List<Person> RemovePeople()
        {
            throw new NotImplementedException();
        }
    }
}
