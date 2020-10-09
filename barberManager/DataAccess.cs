using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data;

namespace barberManager
{
    /// <summary>
    /// Class represent access to the SQLite databse and make related operations on this database.
    /// </summary>
    class DataAccess
    {
        /// <summary>
        /// Adding new person to one of the tables in the db, depends on the value that method receives. 
        /// </summary>
        /// <param name="textQuery"></param>
        /// <param name="tableName"></param>
        public static void AddPerson(string textQuery, string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
            {
                conn.Open();
                SQLiteCommand cmd = conn.CreateCommand();
                cmd.CommandText = "INSERT INTO " + tableName + textQuery;
                cmd.ExecuteNonQuery();
            }
        }

        /// <summary>
        /// Method checks if user exist in the databse.
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="pageName"></param>
        /// <returns></returns>
        public static bool isUserExist(string username, string password, string pageName)
        {
            using (var conn = new SQLiteConnection("Data Source = C:\\Users\\yonat\\Source\\Repos\\YehonatanHen\\Barber-Manager\\barberManager\\BarberManager.db"))
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

        /// <summary>
        /// Method checks if appointment of some client exist in the database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool isAppointmentExist(string name, string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// get data from table in the database depends of date value.
        /// </summary>
        /// <param name="tableName"></param>
        /// <param name="date"></param>
        /// <returns></returns>
        public static DataTable getData(string tableName, string date)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// get data from table in the database with no dependency of some value.
        /// </summary>
        /// <param name="tableName"></param>
        /// <returns></returns>
        public static DataTable getData(string tableName)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// Checks if appointment is possible. i.e. there are no appointment that scheduled in the same time of the
        /// appointment that being cheked. 
        /// </summary>
        /// <param name="date"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool isAppointmentPossible(string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// Remove client/appointment from database.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static bool RemoveAppointment(string name, string date, string start, string end)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// Remove user from database.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public static bool RemoveUser(string username)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// Update database specific appointment. 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="date"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool updateAppointment(string name, string date, string start, string end, string field, string value)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
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

        /// <summary>
        /// Update database specific user. 
        /// </summary>
        /// <param name="username"></param>
        /// <param name="password"></param>
        /// <param name="field"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public static bool updateUser(string username, string password, string field, string value)
        {
            using (var conn = new SQLiteConnection("Data Source=C:\\SQLiteDatabaseBrowserPortable\\Data\\BarberManager.db"))
            {
                bool flag = true;
                conn.Open();
                SQLiteCommand sqCommand = (SQLiteCommand)conn.CreateCommand();
                sqCommand.CommandText = "UPDATE users SET " + field + "=:value WHERE username=@username AND password=@password";
                sqCommand.Parameters.AddWithValue("@username", username);
                sqCommand.Parameters.AddWithValue("@password", password);
                sqCommand.Parameters.AddWithValue("value", value);
                sqCommand.ExecuteNonQuery();
                conn.Close();
                return flag;
            }
        }
    }
}
