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
        }

        private void selectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Client item = listView1.SelectedItem as Client;
            DateBox.Text = item.Date;
            NameBox.Text = item.Name;
            StartBox.Text = item.Start;
            EndBox.Text = item.End;
        }
    }
}
