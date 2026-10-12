using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SaleInvoice
{
    public partial class KlienteControl : UserControl
    {
        DBAccess objDbAccess = new DBAccess();

        public KlienteControl()
        {
            InitializeComponent();
            LoadKliente();
        }

        private void LoadKliente()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SaleInvoice.dbo.Kliente;";
            objDbAccess.readDatathroughAdapter(query, dt);

            dgKliente.ItemsSource = dt.DefaultView;
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext;

            // Hap form për editim dhe i kalon rreshtin përkatës
            EditKlientWindow editWindow = new EditKlientWindow(row);
            if (editWindow.ShowDialog() == true)
            {
                // Rifreskon DataGrid pas ruajtjes
                LoadKliente();
            }
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext; // <-- korrigjuar

            if (MessageBox.Show("Konfirmo fshirjen e klientit?", "Fshij", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                // DELETE query bazuar në ID të klientit
                string query = $"DELETE FROM SaleInvoice.dbo.Kliente WHERE Id = {row["Id"]}";
                SqlCommand cmd = new SqlCommand(query);

                // Kërkon që DBAccess të ketë metodë executeQuery për DELETE/UPDATE/INSERT
                objDbAccess.executeQuery(cmd);

                // Rifreskon DataGrid pas fshirjes
                LoadKliente();
            }
           
        }
       private void BtnAdd_Click(object sneder,RoutedEventArgs e )
        {
            AddKlientWindow addWindow = new AddKlientWindow();
            if (addWindow.ShowDialog() == true)
                LoadKliente();
        }
    }
}
