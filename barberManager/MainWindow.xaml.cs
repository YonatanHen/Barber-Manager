using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private User loggedInUser;

        /// <summary>
        /// main Window constructor.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// When enter button clicked , main page appear if user name and password are correct, else- error message will pop-up. 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void EnterBtn_Click(object sender, RoutedEventArgs e)
        {
            if (DataAccess.isUserExist(unameBox.Text, passwordBox.Password.ToString(), "welcome"))
            {
                this.Content = new mainMenu(this.Content, this);
                loggedInUser = new User(passwordBox.Password.ToString(), unameBox.Text);
                passwordBox.Clear();
                unameBox.Clear();
            }
            else MessageBox.Show("Illegal Password/username");
        }

        public User LoggedInUser {
            get { return loggedInUser; }
            set { loggedInUser=value;}
            }
    }
}
