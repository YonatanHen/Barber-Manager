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
        User user;
        private object _content;
        private MainWindow mainWindow;
        private const string TABLE_NAME = "users";
        public RemAndUpdUser(object _content, MainWindow mainWindow)
        {
            InitializeComponent();
            this.mainWindow = mainWindow;
            this._content = _content;
            uNameBox.Text = mainWindow.UName;
            user = new User(mainWindow.Password, mainWindow.UName);
        }

        /// <summary>
        /// Return to main menu.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BackBtnClick(object sender, RoutedEventArgs e)
        {
            mainWindow.Content = _content;
        }

        /// <summary>
        /// Function removes currently logged in user from database.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            if (mainWindow.UName == uNameBox.Text)
            {
                //Check if user exists in db.
                if (DataAccess.isUserExist(uNameBox.Text, passwordBox.Password.ToString(), "user"))
                {
                    if (isPasswordsMatch())
                    {
                        DataAccess.RemoveUser(uNameBox.Text);
                        MessageBox.Show("User " + uNameBox.Text + " has been deleted.");
                        this.Content = MainWindow.MainContent;
                    }
                }
                else MessageBox.Show("User " + uNameBox.Text + " doesn't exist.");
                clearBoxes();
            }
        }

        /// <summary>
        /// Function update user password/name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            //Check if username isn't match to another username in the database.
            if (DataAccess.isUserExist(user.Uname, user.Password, "user") && isPasswordsMatch())
            {
                if (uNameBox.Text != user.Uname)
                {
                    DataAccess.updateUser(user.Uname, user.Password,
                    "username", uNameBox.Text, TABLE_NAME);
                    user.Uname = uNameBox.Text;
                }
                if (passwordBox.Password.ToString() != user.Password)
                {
                    DataAccess.updateUser(user.Uname, user.Password,
                    "password", passwordBox.Password.ToString(), TABLE_NAME);
                    user.Password = passwordBox.Password.ToString();
                }
                MessageBox.Show("Changes saved.", "Confirmation");
            } 
            clearBoxes();
        }

        /// <summary>
        /// Utility function which returns boolean value if password and password confirmation values are equals. 
        /// </summary>
        /// <returns></returns>
        private bool isPasswordsMatch()
        {
            if (passwordBox.Password.ToString() == confirmPasswordBox.Password.ToString()) return true;
            MessageBox.Show("Password hasn't equals to his confirmation.", "Error!");
            return false;
        }

        /// <summary>
        /// Function clear all text and password boxes.
        /// </summary>
        private void clearBoxes()
        {
            uNameBox.Clear();
            passwordBox.Clear();
            confirmPasswordBox.Clear();
        }
    }
}
