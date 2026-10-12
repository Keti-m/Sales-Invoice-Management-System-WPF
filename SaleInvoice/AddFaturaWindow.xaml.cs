using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class AddFaturaWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();

        public AddFaturaWindow()
        {
            InitializeComponent();
            LoadComboBoxes(); // Mbush ComboBox-et me klientët ekzistues
        }

        // Mbush ComboBox-et me te dhenat nga tabela Kliente
        private void LoadComboBoxes()
        {
            DataTable dtKliente = new DataTable();
            objDbAccess.readDatathroughAdapter("SELECT DISTINCT Emri, Qyteti, Adresa FROM SaleInvoice.dbo.Kliente", dtKliente);

            foreach (DataRow r in dtKliente.Rows)
            {
                cmbKlienti.Items.Add(r["Emri"].ToString());
                cmbQyteti.Items.Add(r["Qyteti"].ToString());
                cmbAdresa.Items.Add(r["Adresa"].ToString());
            }
        }

        // Klik butoni "Ruaj Faturen"
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"
                    INSERT INTO SaleInvoice.dbo.Fatura
                    (
                        NR,
                        [Nr Serise],
                        [Nr Fature],
                        Data,
                        Qyteti,
                        Klienti,
                        Adresa,
                        NIPT_ID,
                        Totali,
                        [Totali Mon Baze],
                        [Pagesa Total],
                        Valuta,
                        Shenim
                    )
                    VALUES
                    (
                        @NR,
                        @NrSerise,
                        @NrFature,
                        @Data,
                        @Qyteti,
                        @Klienti,
                        @Adresa,
                        @NIPT,
                        @Totali,
                        @TotaliMonBaze,
                        @PagesaTotal,
                        @Valuta,
                        @Shenim
                    )";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@NR", txtNR.Text);
                cmd.Parameters.AddWithValue("@NrSerise", txtNrSerise.Text);
                cmd.Parameters.AddWithValue("@NrFature", txtNrFature.Text);
                cmd.Parameters.AddWithValue("@Data", dpData.SelectedDate ?? DateTime.Now);
                cmd.Parameters.AddWithValue("@Qyteti", cmbQyteti.Text);
                cmd.Parameters.AddWithValue("@Klienti", cmbKlienti.Text);
                cmd.Parameters.AddWithValue("@Adresa", cmbAdresa.Text);
                cmd.Parameters.AddWithValue("@NIPT", txtNIPT.Text);
                cmd.Parameters.AddWithValue("@Totali", txtTotali.Text);
                cmd.Parameters.AddWithValue("@TotaliMonBaze", txtTotaliMonBaze.Text);
                cmd.Parameters.AddWithValue("@PagesaTotal", txtPagesaTotal.Text);
                cmd.Parameters.AddWithValue("@Valuta", cmbValuta.Text);
                cmd.Parameters.AddWithValue("@Shenim", txtShenim.Text);

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Fatura u shtua me sukses.");
                this.DialogResult = true; // Mbyll dritaren dhe rikthen true
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
