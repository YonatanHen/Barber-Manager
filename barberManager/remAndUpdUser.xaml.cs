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
            uNameBox.Text =  mainWindow.LoggedInUser.Uname;
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
            if (mainWindow.LoggedInUser.Name == uNameBox.Text)
            {
                //Check if user exists in db.
                if (DataAccess.isUserExist(uNameBox.Text, passwordBox.Password.ToString(), "user"))
                {
                    if (isPasswordsMatch())
                    {
                        DataAccess.RemoveUser(uNameBox.Text);
                        MessageBox.Show("User " + uNameBox.Text + " has been deleted.");
                        mainWindow.Content = mainWindow.mainPage;
                    }
                }
                else MessageBox.Show("User " + uNameBox.Text + " doesn't exist.");
                clearBoxes();
            }else MessageBox.Show("You can only delete your own account.");
        }

        /// <summary>
        /// Function update user password/name
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UpdateBtnClick(object sender, RoutedEventArgs e)
        {
            //Check if username isn't match to another username in the database.
            if (DataAccess.isUserExist(mainWindow.LoggedInUser.Name, mainWindow.LoggedInUser.Password, "user") && isPasswordsMatch())
            {
                if (uNameBox.Text != mainWindow.LoggedInUser.Name)
                {
                    DataAccess.updateUser(mainWindow.LoggedInUser.Name, mainWindow.LoggedInUser.Password,
                    "username", uNameBox.Text);
                    mainWindow.LoggedInUser.Name = uNameBox.Text;
                }
                if (passwordBox.Password.ToString() != mainWindow.LoggedInUser.Password)
                {
                    DataAccess.updateUser(mainWindow.LoggedInUser.Name, mainWindow.LoggedInUser.Password,
                    "password", passwordBox.Password.ToString());
                    mainWindow.LoggedInUser.Password = passwordBox.Password.ToString();
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
            if(passwordBox.Password.ToString() == "" || confirmPasswordBox.Password.ToString()== "")
            {
                MessageBox.Show("Empty values are not acceptable", "Error!");
                return false;
            }
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
