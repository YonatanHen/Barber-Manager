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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace barberManager
{
    /// <summary>
    /// Interaction logic for mainMenu.xaml
    /// </summary>
    public partial class mainMenu : Page
    {
        private object _content;
        private MainWindow mainWindow;
        public mainMenu(object _content,MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this._content = _content;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = _content;
        }

        private void calendar_MouseDoubleClick(Object sender, SelectionChangedEventArgs e)
        {
            ScheduleMeetingWin meetingWin = new ScheduleMeetingWin(Calendar.SelectedDate);
            meetingWin.Show();
            Console.WriteLine(Calendar.SelectedDate.ToString());
        }

        private void MenuItem_MouseMove(object sender, MouseEventArgs e)
        {
            
        }
    }
}
