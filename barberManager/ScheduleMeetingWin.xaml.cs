using System;
using System.Collections.Generic;
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


namespace barberManager
{
    /// <summary>
    /// Interaction logic for ScheduleMeetingWin.xaml
    /// </summary>

public partial class ScheduleMeetingWin : Window
    {
        private DateTime? selectedDate;
        private DataAccess data;

        public ScheduleMeetingWin(DateTime? selectedDate) //?=indicates that it is a nullable version of the primitive DateTime.
        {
            data = new DataAccess();
            this.selectedDate = selectedDate;
            InitializeComponent();
            DateBox.Text = string.Format("{0}/{1}/{2}", selectedDate.Value.Day, selectedDate.Value.Month, selectedDate.Value.Year);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //Convert appointment start time to dateTime time.
            selectedDate = DateTime.ParseExact(StartBox.Text, "HH:mm", null, System.Globalization.DateTimeStyles.None);
            data.AddPerson("(name,date,start,end) VALUES ('"+ NameBox.Text +"','" + DateBox.Text + 
                "','" + StartBox.Text + "','" + EndBox.Text +"')", "clients");

        }
    }
}
