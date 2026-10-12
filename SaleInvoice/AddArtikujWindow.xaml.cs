using SaleInvoice.DatabaseProject;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class AddArtikujWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();

        public AddArtikujWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"
                    INSERT INTO SaleInvoice.dbo.Artikuj
                    (
                        [Kodi i Artikullit],
                        Emri,
                        Pershkrimi,
                        Barkodi,
                        [Cmimi i Shitjes],
                        [Valuta e Shitjes],
                        [Me TVSH Shitje],
                        [Cmimi i Blerjes],
                        [Valuta e Blerjes],
                        [Me TVSH Blerje],
                        [TVSH %],
                        Njesia,
                        Lloji,
                        [Skema e Llogarise],
                        [Garancia(Muaj)],
                        Keyword,
                        Aktiv
                    )
                    VALUES
                    (
                        @KodiArtikuj,
                        @Emri,
                        @Pershkrimi,
                        @Barkodi,
                        @CmimiShitjes,
                        @ValutaShitjes,
                        @MeTVSHShitje,
                        @CmimiBlerjes,
                        @ValutaBlerjes,
                        @MeTVSHBlerje,
                        @TVSH,
                        @Njesia,
                        @Lloji,
                        @SkemaLlogarise,
                        @Garancia,
                        @Keyword,
                        @Aktiv
                    )";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@KodiArtikuj", txtKodiArtikuj.Text);
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

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Artikulli u shtua me sukses.");
                this.DialogResult = true; // Mbyll dritaren dhe rikthen true
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
