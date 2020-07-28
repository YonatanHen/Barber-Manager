using System;
using System.Collections.Generic;
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

    struct Appointment
    {
        public Client client;
        public DateTime? dateTime;
        public double length;
    };

public partial class ScheduleMeetingWin : Window
    {
        private Appointment appointment;
        private DateTime? selectedDate;

        public ScheduleMeetingWin(DateTime? selectedDate) //?=indicates that it is a nullable version of the primitive DateTime.
        {
            this.selectedDate = selectedDate;
            appointment = new Appointment();
            appointment.dateTime = selectedDate;
            InitializeComponent();
            DateBox.Text = selectedDate.ToString();
        }
    }
}
