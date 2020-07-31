using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

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

        public void AddPerson(string textQuery,string tableName)
        {
            try
            {
                conn.Open();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            SQLiteCommand cmd = conn.CreateCommand();
            cmd.CommandText = "INSERT INTO " + tableName + textQuery;
            cmd.ExecuteNonQuery();
            conn.Close();
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
            SQLiteCommand cmd;
            cmd = conn.CreateCommand();
            cmd.CommandText = "SELECT * FROM users WHERE username='"+username+"' AND "+"password="+password;
            try
            {
                sqlite_datareader = cmd.ExecuteReader();
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

        public DataTable getData(string tableName,string date)
        {
            if (tableName == "clients")
            {
                DataTable dt = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT name,start,end FROM clients WHERE date='" + date + "'",conn);
                da.Fill(dt);
                return dt;
            }
            return null;
        }
    }
}
