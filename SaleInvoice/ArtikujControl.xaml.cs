using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;
using System.Windows.Controls;

namespace SaleInvoice
{
    public partial class ArtikujControl : UserControl
    {
        private DBAccess objDbAccess = new DBAccess();

        public ArtikujControl()
        {
            InitializeComponent();
            LoadArtikuj();
        }

        // Ngarkon të dhënat e artikujve në DataGrid
        private void LoadArtikuj()
        {
            DataTable dt = new DataTable();
            string query = "SELECT * FROM SaleInvoice.dbo.Artikuj;";
            objDbAccess.readDatathroughAdapter(query, dt);
            dgArtikuj.ItemsSource = dt.DefaultView;
        }

        // Klik butoni Ndrysho
        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext;

            EditArtikujWindow editWindow = new EditArtikujWindow(row);
            if (editWindow.ShowDialog() == true)
            {
                LoadArtikuj();
            }
        }

        // Klik butoni Fshij
        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            Button btn = sender as Button;
            DataRowView row = (DataRowView)btn.DataContext;

            if (MessageBox.Show("A jeni të sigurt që doni të fshini këtë artikull?", "Konfirmo Fshirjen", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                string query = "DELETE FROM SaleInvoice.dbo.Artikuj WHERE [Kodi i Artikullit] = @KodiArtikullit";
                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@KodiArtikullit", row["Kodi i Artikullit"]);
                objDbAccess.executeQuery(cmd);

                LoadArtikuj();
            }
        }

        private void BtnAdd_Click(object sneder, RoutedEventArgs e)
        {
            AddKlientWindow addWindow = new AddKlientWindow();
            if (addWindow.ShowDialog() == true)
                LoadArtikuj();
        }
    }
}
