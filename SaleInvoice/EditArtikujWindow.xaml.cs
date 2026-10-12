using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class EditArtikujWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();
        private DataRowView artikujRow; // Rreshti i artikullit nga DataGrid

        public EditArtikujWindow(DataRowView row)
        {
            InitializeComponent();
            artikujRow = row;
            LoadArtikujData();
        }

        // Mbush të dhënat e artikullit në fushat e formës
        private void LoadArtikujData()
        {
            txtKodiArtikuj.Text = artikujRow["Kodi i Artikullit"].ToString();
            txtEmri.Text = artikujRow["Emri"].ToString();
            txtPershkrimi.Text = artikujRow["Pershkrimi"].ToString();
            txtBarkodi.Text = artikujRow["Barkodi"].ToString();
            txtCmimiShitjes.Text = artikujRow["Cmimi i Shitjes"].ToString();
            cmbValutaShitjes.Text = artikujRow["Valuta e Shitjes"].ToString();
            chkMeTVSHShitje.IsChecked = Convert.ToBoolean(artikujRow["Me TVSH Shitje"]);
            txtCmimiBlerjes.Text = artikujRow["Cmimi i Blerjes"].ToString();
            cmbValutaBlerjes.Text = artikujRow["Valuta e Blerjes"].ToString();
            chkMeTvshBlerje.IsChecked = Convert.ToBoolean(artikujRow["Me TVSH Blerje"]);
            txtTVSH.Text = artikujRow["TVSH %"].ToString();
            cmbNjesia.Text = artikujRow["Njesia"].ToString();
            cmbLloji.Text = artikujRow["Lloji"].ToString();
            cmbSkemaLlogarise.Text = artikujRow["Skema e Llogarise"].ToString();
            txtGarancia.Text = artikujRow["Garancia(Muaj)"].ToString();
            txtKeyword.Text = artikujRow["Keyword"].ToString();
            chkAktiv.IsChecked = Convert.ToBoolean(artikujRow["Aktiv"]);
        }

        // Klik butoni "Ruaj Ndryshimet"
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"
                    UPDATE SaleInvoice.dbo.Artikuj
                    SET 
                        Emri = @Emri,
                        Pershkrimi = @Pershkrimi,
                        Barkodi = @Barkodi,
                        [Cmimi i Shitjes] = @CmimiShitjes,
                        [Valuta e Shitjes] = @ValutaShitjes,
                        [Me TVSH Shitje] = @MeTVSHShitje,
                        [Cmimi i Blerjes] = @CmimiBlerjes,
                        [Valuta e Blerjes] = @ValutaBlerjes,
                        [Me TVSH Blerje] = @MeTVSHBlerje,
                        [TVSH %] = @TVSH,
                        Njesia = @Njesia,
                        Lloji = @Lloji,
                        [Skema e Llogarise] = @SkemaLlogarise,
                        [Garancia(Muaj)] = @Garancia,
                        Keyword = @Keyword,
                        Aktiv = @Aktiv
                    WHERE [Kodi i Artikullit] = @KodiArtikuj";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Emri", txtEmri.Text);
                cmd.Parameters.AddWithValue("@Pershkrimi", txtPershkrimi.Text);
                cmd.Parameters.AddWithValue("@Barkodi", txtBarkodi.Text);
                cmd.Parameters.AddWithValue("@CmimiShitjes", txtCmimiShitjes.Text);
                cmd.Parameters.AddWithValue("@ValutaShitjes", cmbValutaShitjes.Text);
                cmd.Parameters.AddWithValue("@MeTVSHShitje", chkMeTVSHShitje.IsChecked ?? false);
                cmd.Parameters.AddWithValue("@CmimiBlerjes", txtCmimiBlerjes.Text);
                cmd.Parameters.AddWithValue("@ValutaBlerjes", cmbValutaBlerjes.Text);
                cmd.Parameters.AddWithValue("@MeTVSHBlerje", chkMeTvshBlerje.IsChecked ?? false);
                cmd.Parameters.AddWithValue("@TVSH", txtTVSH.Text);
                cmd.Parameters.AddWithValue("@Njesia", cmbNjesia.Text);
                cmd.Parameters.AddWithValue("@Lloji", cmbLloji.Text);
                cmd.Parameters.AddWithValue("@SkemaLlogarise", cmbSkemaLlogarise.Text);
                cmd.Parameters.AddWithValue("@Garancia", txtGarancia.Text);
                cmd.Parameters.AddWithValue("@Keyword", txtKeyword.Text);
                cmd.Parameters.AddWithValue("@Aktiv", chkAktiv.IsChecked ?? false);
                cmd.Parameters.AddWithValue("@KodiArtikuj", txtKodiArtikuj.Text);

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Ndryshimet e artikujve u ruajtën me sukses.");
                this.DialogResult = true; // Mbyll dritaren dhe rikthen true
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
