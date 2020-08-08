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
    /// Interaction logic for addUser.xaml
    /// </summary>
    public partial class RemAndUpdUser : Page
    {
        private object _content;
        private MainWindow mainWindow;
        public RemAndUpdUser(object _content, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._content = _content;
            uNameBox.Text = mainWindow.UName;
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = _content;
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {

        }

        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
