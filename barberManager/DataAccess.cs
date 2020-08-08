using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace barberManager
{
    class DataAccess
    {
        public static void AddPerson(string textQuery, string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO " + tableName + textQuery;
                cmd.ExecuteNonQuery();
            }
        }

        public static bool isUserExist(string username, string password, string pageName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                bool flag = false;
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                if (pageName == "welcome") sqCommand.CommandText = "SELECT * FROM users WHERE username='" + username + "' AND password='" + password + "'";
                if (pageName == "user") sqCommand.CommandText = "SELECT * FROM users WHERE username='" + username + "'";
                SQLiteDataReader sqReader = sqCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    if (pageName == "welcome" && username == sqReader.GetString(0) && password == sqReader.GetString(1)) flag = true;
                    else if (pageName == "user" && username == sqReader.GetString(0)) flag = true;
                }
                return flag;
            }
        }

        public static bool isAppointmentExist(string name, string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                bool flag = false;
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "SELECT * FROM appointments WHERE name='" + name + "' AND date='" + date +
                    "' AND start='" + start + "' AND end='" + end + "'";
                SQLiteDataReader sqReader = sqCommand.ExecuteReader();
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    if (name == sqReader.GetString(0) && date == sqReader.GetString(1) &&
                        start == sqReader.GetString(2) && end == sqReader.GetString(3)) flag = true;
                }
                return flag;
            }
        }

        public static DataTable getData(string tableName, string date)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                if (tableName == "appointments")
                {
                    DataTable dt = new DataTable();
                    SQLiteDataAdapter da = new SQLiteDataAdapter("SELECT name,start,end FROM appointments WHERE date='" + date + "'", conn);
                    da.Fill(dt);
                    return dt;
                }
                return null;
            }
        }

        public static DataTable getData(string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
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
        }

        public static bool isAppointmentPossible(string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "SELECT * FROM appointments WHERE date='" + date + "'";
                SQLiteDataReader sqReader = sqCommand.ExecuteReader();
                TimeSpan startTime = TimeSpan.Parse(start);
                TimeSpan endTime = TimeSpan.Parse(end);
                // Always call Read before accessing data.
                while (sqReader.Read())
                {
                    TimeSpan readedStart = TimeSpan.Parse(sqReader.GetString(2));
                    TimeSpan readedEnd = TimeSpan.Parse(sqReader.GetString(3));
                    if ((startTime >= readedStart && startTime <= readedEnd) || (endTime >= readedStart && endTime <= readedEnd))
                        return false;
                }
                return true;
            }
        }

        public static bool RemovePeople(string name, string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "DELETE FROM appointments WHERE name='" + name + "' AND date='" + date +
                    "' AND start='" + start + "' AND end='" + end + "'";
                //keep the command execution in an int value which keeps the number of rows who deleted.
                int deletedVal = sqCommand.ExecuteNonQuery();
                // Value > 0 means that rows was deleted successfully.
                if (deletedVal > 0) return true;
                return false;
            }
        }

        public static bool RemoveUser(string username)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "DELETE FROM users WHERE username='" + username + "' ";
                //keep the command execution in an int value which keeps the number of rows who deleted.
                int deletedVal = sqCommand.ExecuteNonQuery();
                // Value > 0 means that rows was deleted successfully.
                if (deletedVal > 0) return true;
                return false;
            }
        }

        public static bool updateAppointment(string name, string date, string start, string end, string field, string value, string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
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

        public static bool updateUser(string username, string password, string field, string value, string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\Users.db"))
            {
                bool flag = true;
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "UPDATE users SET " + field + "=:value WHERE username=@username AND password=@password";
                sqCommand.Parameters.AddWithValue("@username", username);
                sqCommand.Parameters.AddWithValue("@password", password);
                sqCommand.ExecuteNonQuery();
                conn.Close();
                return flag;
            }
        }
    }
}
