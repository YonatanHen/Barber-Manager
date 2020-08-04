using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Globalization;

namespace barberManager
{
    /// <summary>
    /// Interaction logic for ScheduleMeetingWin.xaml
    /// </summary>

public partial class ScheduleMeetingWin : Window
    {
        private DateTime? selectedDate;
        private DataAccess data;
        private string dateStr;

        public ScheduleMeetingWin(DateTime? selectedDate) //?=indicates that it is a nullable version of the primitive DateTime.
        {
            InitializeComponent();
            data = new DataAccess();
            this.selectedDate = selectedDate;
            dateStr= string.Format("{0}/{1}/{2}", selectedDate.Value.Day, selectedDate.Value.Month, selectedDate.Value.Year);
            DateBox.Text = dateStr;
            DataTable dTable = data.getData("appointments", dateStr);
            // Clear the ListView control
            listView1.Items.Clear();
            // Display items in the ListView control
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DataRow drow = dTable.Rows[i];
                listView1.Items.Add(new Client(Name = drow["name"].ToString()) { Name=drow["name"].ToString(),
                Start=drow["start"].ToString(),
                End=drow["end"].ToString() });
            }
        }

        private void Add(object sender, RoutedEventArgs e)
        {
            //Convert appointment start time to dateTime time.
            selectedDate = DateTime.ParseExact(StartBox.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            if (data.isAppointmentPossible(DateBox.Text, StartBox.Text, EndBox.Text))
            {
                data.AddPerson("(name,date,start,end) VALUES ('" + NameBox.Text + "','" + DateBox.Text +
                    "','" + StartBox.Text + "','" + EndBox.Text + "')", "appointments");
                this.Close();
            }
            //Show message,don't set an appointment and don't exit from window
            else MessageBox.Show("Illegal hour selected, appointments colliding!");

        }
    }
}
