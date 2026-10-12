using SaleInvoice.DatabaseProject;
using System;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class AddKlientWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();

        public AddKlientWindow()
        {
            InitializeComponent();
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                string query = @"
                    INSERT INTO SaleInvoice.dbo.Kliente
                    (
                        Emri,
                        Kodi,
                        Shteti,
                        Qyteti,
                        Email,
                        Tel,
                        [Llogaria Kontabel],
                        Barcode,
                        [Lista e Cmimeve],
                        Adresa,
                        Note
                    )
                    VALUES
                    (
                        @Emri,
                        @Kodi,
                        @Shteti,
                        @Qyteti,
                        @Email,
                        @Tel,
                        @LlogariaKontabel,
                        @Barcode,
                        @ListaCmimeve,
                        @Adresa,
                        @Note
                    )";

                SqlCommand cmd = new SqlCommand(query);
                cmd.Parameters.AddWithValue("@Emri", txtEmri.Text);
                cmd.Parameters.AddWithValue("@Kodi", txtKodi.Text);
                cmd.Parameters.AddWithValue("@Shteti", cmbShteti.Text);
                cmd.Parameters.AddWithValue("@Qyteti", cmbQyteti.Text);
                cmd.Parameters.AddWithValue("@Email", txtEmail.Text);
                cmd.Parameters.AddWithValue("@Tel", txtTel.Text);
                cmd.Parameters.AddWithValue("@LlogariaKontabel", cmbLlogariaKontabel.Text);
                cmd.Parameters.AddWithValue("@Barcode", txtBarcode.Text);
                cmd.Parameters.AddWithValue("@ListaCmimeve", cmbListaCmimeve.Text);
                cmd.Parameters.AddWithValue("@Adresa", txtAdresa.Text);
                cmd.Parameters.AddWithValue("@Note", txtNote.Text);

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Klienti u shtua me sukses.");
                this.DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
