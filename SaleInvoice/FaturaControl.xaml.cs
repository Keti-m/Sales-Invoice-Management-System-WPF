using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SaleInvoice
{
    public partial class FaturaControl : UserControl
    {
        DBAccess objDbAccess = new DBAccess();

        public FaturaControl()
        {
            InitializeComponent();
            LoadFatura();
        }

        // Ngarkon të gjitha faturat në DataGrid
        private void LoadFatura()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SaleInvoice.dbo.Fatura;";
            objDbAccess.readDatathroughAdapter(query, dt);
            dgFatura.ItemsSource = dt.DefaultView;
        }

        // Ndrysho një rresht
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext;

            EditFatureWindow editWindow = new EditFatureWindow(row);
            if (editWindow.ShowDialog() == true)
            {
                LoadFatura(); // Rifreskon gridin pas ndryshimit
            }
        }

        // Fshij një rresht
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext;

            // Konfirmimi i fshirjes
            if (MessageBox.Show("A jeni të sigurt që dëshironi ta fshini këtë faturë?",
                "Konfirmo Fshirjen", MessageBoxButton.YesNo, MessageBoxImage.Warning) == MessageBoxResult.Yes)
            {
                try
                {
                    string query = "DELETE FROM SaleInvoice.dbo.Fatura WHERE NR = @NR";
                    SqlCommand cmd = new SqlCommand(query);
                    cmd.Parameters.AddWithValue("@NR", row["NR"]);

                    objDbAccess.executeQuery(cmd);

                    MessageBox.Show("Fatura u fshi me sukses.");
                    LoadFatura(); // Rifreskon gridin pas fshirjes
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Gabim gjatë fshirjes: " + ex.Message);
                }
            }
        }
        private void BtnAdd_Click(object sneder, RoutedEventArgs e)
        {
            AddFaturaWindow addWindow = new AddFaturaWindow();
            if (addWindow.ShowDialog() == true)
                LoadFatura();
        }
    }
}
