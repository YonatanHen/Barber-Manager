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
    public partial class addUser : Page
    {
        private object _content;
        private MainWindow mainWindow;
        public addUser(object _content, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._content = _content;
        }

        /// <summary>
        /// Adding new user by using AddPerson function from DataAccess class.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddBtnClick(object sender, RoutedEventArgs e)
        {
            if (!DataAccess.isUserExist(uNameBox.Text, passwordBox.Text, "user"))
            {
                DataAccess.AddPerson("(username,password) VALUES ('" + uNameBox.Text + "','" + passwordBox.Text + "')", "users");
                MessageBox.Show("User has been added!!","Message");
                uNameBox.Clear();
                passwordBox.Clear();
            }else MessageBox.Show("User is already exist!!", "Message");
        }

        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = _content;
        }
    }
}
