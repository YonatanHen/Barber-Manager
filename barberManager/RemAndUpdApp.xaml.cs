using System;
using System.Collections.Generic;
using System.Data;
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
    /// Interaction logic for RemAndUpdApp.xaml
    /// </summary>
    public partial class RemAndUpdApp : Window
    {
        DataAccess data;
        Client item;
        public RemAndUpdApp()
        {
            InitializeComponent();
            data = new DataAccess();
            DataTable dTable = data.getData("clients");
            // Clear the ListView control
            listView1.Items.Clear();
            // Display items in the ListView control
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                DataRow drow = dTable.Rows[i];
                listView1.Items.Add(new Client(Name = drow["name"].ToString())
                {
                    Name = drow["name"].ToString(),
                    Date = drow["date"].ToString(),
                    Start = drow["start"].ToString(),
                    End = drow["end"].ToString()
                });
            }
            item= listView1.SelectedItem as Client;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = listView1.SelectedItem as Client;
            //Reaching to null items raising an error
            if (item != null)
            {
                DateBox.Text = item.Date;
                NameBox.Text = item.Name;
                StartBox.Text = item.Start;
                EndBox.Text = item.End;
            }
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            if (DateBox.Text != "" && NameBox.Text != "" && StartBox.Text != "" && EndBox.Text != "")
            {
                if (data.RemovePeople(NameBox.Text, DateBox.Text, StartBox.Text, EndBox.Text))
                {
                    listView1.Items.Remove(listView1.SelectedItem);
                    MessageBox.Show("Appointment with " + NameBox.Text + " at " + DateBox.Text + " , " + StartBox.Text
                        + " has been deleted.");
                    //Initialize the textboxes to the first value from table by default.
                    listView1.SelectedIndex = 0;
                }
                else MessageBox.Show("Appointment value doesn't found.");
            }
        }
    }
}
