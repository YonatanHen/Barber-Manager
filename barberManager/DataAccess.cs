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

        public bool isUserExist(string username,string password)
        {
            bool flag = false;
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "SELECT * FROM users WHERE username='" + username + "' AND password='" + password + "'";
            SQLiteDataReader sqReader = sqCommand.ExecuteReader();
            try
            {
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    if (username == sqReader.GetString(0) && password == sqReader.GetString(1)) flag = true;                    
                }
            }
            finally
            {
                // always call Close when done reading.
                sqReader.Close();
                conn.Close();
            }
            return flag;
        }

        public bool isAppointmentExist(string name, string date, string start, string end)
        {
            bool flag = false;
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "SELECT * FROM appointments WHERE name='" + name + "' AND date='" + date +
                "' AND start='" + start + "' AND end='" + end + "'";
            SQLiteDataReader sqReader = sqCommand.ExecuteReader();
            try
            {
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    if (name == sqReader.GetString(0) && date == sqReader.GetString(1) &&
                        start == sqReader.GetString(2) && end == sqReader.GetString(3)) flag = true;
                }
            }
            finally
            {
                // always call Close when done reading.
                sqReader.Close();
                conn.Close();
            }
            return flag;
        }

        public DataTable getData(string tableName,string date)
        {
            if (tableName == "appointments")
            {
                DataTable dt = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT name,start,end FROM appointments WHERE date='" + date + "'",conn);
                da.Fill(dt);
                return dt;
            }
            return null;
        }

        public DataTable getData(string tableName)
        {
            if (tableName == "appointments")
            {
                DataTable dt = new DataTable();
                SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT * FROM appointments", conn);
                da.Fill(dt);
                return dt;
            }
            return null;
        }

        public bool isAppointmentPossible(string date, string start,string end)
        {
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "SELECT * FROM appointments WHERE date='" + date + "'";
            SQLiteDataReader sqReader = sqCommand.ExecuteReader();
            TimeSpan startTime = TimeSpan.Parse(start);
            TimeSpan endTime = TimeSpan.Parse(end);
            try
            {
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    TimeSpan readedStart = TimeSpan.Parse(sqReader.GetString(2));
                    TimeSpan readedEnd = TimeSpan.Parse(sqReader.GetString(3));
                    if ((startTime >= readedStart && startTime <= readedEnd) || (endTime >= readedStart && endTime <= readedEnd))
                    {
                        conn.Close();
                        return false;
                    }
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

        public bool RemovePeople(string name,string date,string start,string end)
        {
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "DELETE FROM appointments WHERE name='" + name + "' AND date='" + date +
                "' AND start='" + start + "' AND end='" + end + "'";
            //keep the command execution in an int value which keeps the number of rows who deleted.
            int deletedVal=sqCommand.ExecuteNonQuery();
            conn.Close();
            // Value > 0 means that rows was deleted successfully.
            if (deletedVal > 0) return true;
            return false;
        }

        public bool updateAppointment(string name, string date, string start, string end, string field, string value, string tableName)
        {
            bool flag = true;
            conn.Open();
            SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
            sqCommand.CommandText = "UPDATE appointments SET " + field + "=:value WHERE name=@name AND date=@date AND start=@start " +
                "AND end=@end";
            sqCommand.Parameters.AddWithValue("@name", name);
            sqCommand.Parameters.AddWithValue("@date", date);
            sqCommand.Parameters.AddWithValue("@start", start);
            sqCommand.Parameters.AddWithValue("@end", end);
            sqCommand.Parameters.AddWithValue("value", value);
            sqCommand.ExecuteNonQuery();
            conn.Close();
            return flag;
        }
    }
}
