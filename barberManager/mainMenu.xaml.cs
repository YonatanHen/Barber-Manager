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
        private object _content; // page content
        private MainWindow mainWindow; // the main window in welcome page xaml file.
        public mainMenu(object _content,MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._content = _content;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = _content;
        }

        private void calendar_MouseDoubleClick(Object sender, SelectionChangedEventArgs e)
        {
            mainWindow.Content = new ScheduleMeeting(this.Content,mainWindow,Calendar.SelectedDate);
        }

        private void remAndupdApp(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new RemAndUpdApp(this.Content,mainWindow);
        }

        private void addUser(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new addUser(this.Content, mainWindow);
        }

        private void remAndupdUser(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = new RemAndUpdUser(this.Content, mainWindow);
        }
    }
}
