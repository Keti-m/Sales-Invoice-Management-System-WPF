using SaleInvoice.DatabaseProject;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows;

namespace SaleInvoice
{
    public partial class EditKlientWindow : Window
    {
        private DBAccess objDbAccess = new DBAccess();
        private DataRowView klientRow; // Rreshti i klientit nga DataGrid

        public EditKlientWindow(DataRowView row)
        {
            InitializeComponent();
            klientRow = row;
            LoadKlientData();
        }

        // Mbush të gjitha fushat me të dhënat e klientit
        private void LoadKlientData()
        {
            txtId.Text = klientRow["ID(Nipt)"].ToString();
            txtEmri.Text = klientRow["Emri"].ToString();
            txtKodi.Text = klientRow["Kodi"].ToString();

            cmbShteti.Text = klientRow["Shteti"].ToString();
            cmbQyteti.Text = klientRow["Qyteti"].ToString();
            txtEmail.Text = klientRow["Email"].ToString();
            txtTel.Text = klientRow["Tel"].ToString();
            cmbLlogariaKontabel.Text = klientRow["Llogaria Kontabel"].ToString();
            txtBarcode.Text = klientRow["Barcode"].ToString();
            cmbListaCmimeve.Text = klientRow["Lista e Cmimeve"].ToString();
            txtAdresa.Text = klientRow["Adresa"].ToString();
            txtNote.Text = klientRow["Note"].ToString();
        }

        // Klik butoni "Ruaj Ndryshimet"
        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                
                string query = @"
                    UPDATE SaleInvoice.dbo.Kliente
                    SET 
                        Emri = @Emri,
                        Kodi = @Kodi,
                        Shteti = @Shteti,
                        Qyteti = @Qyteti,
                        Email = @Email,
                        Tel = @Tel,
                        [Llogaria Kontabel] = @LlogariaKontabel,
                        Barcode = @Barcode,
                        [Lista e Cmimeve] = @ListaCmimeve,
                        Adresa = @Adresa,
                        Note = @Note
                    WHERE [ID(Nipt)] = @ID";

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
                cmd.Parameters.AddWithValue("@ID", txtId.Text);

                objDbAccess.executeQuery(cmd);

                MessageBox.Show("Ndryshimet u ruajtën me sukses.");
                this.DialogResult = true; 
            }
            catch (Exception ex)
            {
                MessageBox.Show("Gabim: " + ex.Message);
            }
        }
    }
}
