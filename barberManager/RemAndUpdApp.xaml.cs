using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        private const string TABLE_NAME = "appointments";
        DataRow drow;
        public RemAndUpdApp()
        {
            InitializeComponent();
            data = new DataAccess();
            DataTable dTable = data.getData(TABLE_NAME);
            // Clear the ListView control
            listView1.Items.Clear();
            // Display items in the ListView control
            for (int i = 0; i < dTable.Rows.Count; i++)
            {
                drow = dTable.Rows[i];
                listView1.Items.Add(new Client(Name = drow["name"].ToString())
                {
                    Name = drow["name"].ToString(),
                    Date = drow["date"].ToString(),
                    Start = drow["start"].ToString(),
                    End = drow["end"].ToString()
                });
            }
            item = listView1.SelectedItem as Client;
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            item = listView1.SelectedItem as Client;
            //Reaching to null items raising an error
            try
            {
                DateBox.Text = item.Date;
                NameBox.Text = item.Name;
                StartBox.Text = item.Start;
                EndBox.Text = item.End;
            }
            catch (Exception ex) { }
        }

        private void RemoveBtnClick(object sender, RoutedEventArgs e)
        {
            if (data.RemovePeople(NameBox.Text, DateBox.Text, StartBox.Text, EndBox.Text) && noEmptyTextBoxes())
            {
                listView1.Items.Remove(listView1.SelectedItem);
                MessageBox.Show("Appointment with " + NameBox.Text + " at " + DateBox.Text + " , " + StartBox.Text
                    + " has been deleted.");
                //Initialize the textboxes to the first value from table by default.
                listView1.SelectedIndex = 0;
            }
            else MessageBox.Show("Appointment doesn't found.");
        }

        private void updateBtnClick(object sender, RoutedEventArgs e)
        {
            if (noEmptyTextBoxes())
            {
                if (data.isAppointmentExist(NameBox.Text, DateBox.Text, StartBox.Text, EndBox.Text))
                    MessageBox.Show("Can't change appointment to this values because they are already exist in the system.",
                        "Existing appointment");
                else
                {
                    //Change the values that they will be match to the text boxes values.
                    item = listView1.SelectedItem as Client;
                    if (DateBox.Text != item.Date)
                    {
                        data.updateAppointment(item.Name, item.Date, item.Start, item.End,
                        "date", DateBox.Text, TABLE_NAME);
                        item.Date = DateBox.Text;
                    }
                    if (NameBox.Text != item.Name)
                    {
                        data.updateAppointment(item.Name, item.Date, item.Start, item.End,
                        "name", NameBox.Text, TABLE_NAME);
                        item.Name = NameBox.Text;
                    }
                    if (StartBox.Text != item.Start)
                    {
                        data.updateAppointment(item.Name, item.Date, item.Start, item.End,
                        "start", StartBox.Text, TABLE_NAME);
                        item.Start = StartBox.Text;
                    }
                    if (StartBox.Text != item.Start)
                    {
                        data.updateAppointment(item.Name, item.Date, item.Start, item.End,
                        "end", EndBox.Text, TABLE_NAME);
                        item.End = EndBox.Text;
                    }
                    listView1.Items.Refresh();
                    listView1.SelectedIndex = 0;
                }
            }
        }


        /// <summary>
        /// Utility function=checking if there are empty text boxes, returns boolean value.
        /// </summary>
        private bool noEmptyTextBoxes()
        {
            if (DateBox.Text != "" && NameBox.Text != "" && StartBox.Text != "" && EndBox.Text != "") return true;
            else
            {
                MessageBox.Show("There are empty text boxes!!!", "Error!");
                return false;
            }
        }
    }
}