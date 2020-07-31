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

        public bool isAppointmentPossible(string date, string start,string end)
        {
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "SELECT * FROM clients WHERE date='" + date + "'";
            SQLiteDataReader sqReader = sqCommand.ExecuteReader();
            TimeSpan startTime = TimeSpan.Parse(start);
            TimeSpan endTime = TimeSpan.Parse(end);
            try
            {
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    //TODO: check immpossible combinations of hours, but first convert to dateTime
                    TimeSpan readedStart = TimeSpan.Parse(sqReader.GetString(2));
                    TimeSpan readedEnd = TimeSpan.Parse(sqReader.GetString(3));
                    if ((startTime >= readedStart && startTime <= readedEnd) || (endTime >= readedStart && endTime <= readedEnd)) return false;
                    //Console.WriteLine(sqReader.GetString(2) + " " + sqReader.GetString(3));
                }
            }
            finally
            {
                // always call Close when done reading.
                sqReader.Close();
                conn.Close();
            }
            return true;
        }
    }
}
