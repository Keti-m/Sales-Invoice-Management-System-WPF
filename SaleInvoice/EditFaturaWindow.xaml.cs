using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class EditFatureWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();
        private DataRowView fatureRow;

        public EditFatureWindow(DataRowView row)
        {
            InitializeComponent();
            fatureRow = row;

            LoadComboBoxes(); // Mbush combobox-et
            LoadFatureData(); // Mbush fushat me të dhënat e fatures
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

        // Mbush të dhënat e fatures
        private void LoadFatureData()
        {
            txtNR.Text = fatureRow["NR"].ToString();
            txtNrSerise.Text = fatureRow["Nr Serise"].ToString();
            txtNrFature.Text = fatureRow["Nr Fature"].ToString();
            dpData.SelectedDate = Convert.ToDateTime(fatureRow["Data"]);
            cmbQyteti.Text = fatureRow["Qyteti"].ToString();
            cmbKlienti.Text = fatureRow["Klienti"].ToString();
            cmbAdresa.Text = fatureRow["Adresa"].ToString();
            txtNIPT.Text = fatureRow["NIPT_ID"].ToString();
            txtTotali.Text = fatureRow["Totali"].ToString();
            txtTotaliMonBaze.Text = fatureRow["Totali Mon Baze"].ToString();
            txtPagesaTotal.Text = fatureRow["Pagesa Total"].ToString();
            cmbValuta.Text = fatureRow["Valuta"].ToString();
            txtShenim.Text = fatureRow["Shenim"].ToString();
        }

        // Klik butoni "Ruaj Ndryshimet"
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"
                    UPDATE SaleInvoice.dbo.Fatura
                    SET 
                        [Nr Serise] = @NrSerise,
                        [Nr Fature] = @NrFature,
                        Data = @Data,
                        Qyteti = @Qyteti,
                        Klienti = @Klienti,
                        Adresa = @Adresa,
                        NIPT_ID = @NIPT,
                        Totali = @Totali,
                        [Totali Mon Baze] = @TotaliMonBaze,
                        [Pagesa Total] = @PagesaTotal,
                        Valuta = @Valuta,
                        Shenim = @Shenim
                    WHERE NR = @NR";

                SqlCommand cmd = new SqlCommand(query);
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
                cmd.Parameters.AddWithValue("@NR", txtNR.Text);

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Ndryshimet e fatures u ruajtën me sukses.");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
