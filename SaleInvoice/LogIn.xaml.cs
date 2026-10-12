using SaleInvoice.DatabaseProject;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace SaleInvoice
{
  
    public partial class LogIn : Window
    {

        DBAccess objDbAccess = new DBAccess();
        DataTable dtUsers = new DataTable();
        public LogIn()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string userName = txtUsername.Text;
            string userPassword = txtPassword.Text;

            if (userName.Equals(""))
                    {
                MessageBox.Show("Vendos username.");
            }
            else if (userPassword.Equals("")) 
            {
                MessageBox.Show("Vendos password.");
            }
            else
            {

                string query = "SELECT * FROM Users WHERE Username = '" + userName + "' AND Password = '" + userPassword + "'";
                objDbAccess.readDatathroughAdapter(query, dtUsers);


                if (dtUsers.Rows.Count == 1)
                {

                    MessageBox.Show("Login i suksesshem.");
                    objDbAccess.closeConn();

                    this.Hide();
                    MainWindow window = new MainWindow();
                    window.Show();
                }
            }


            }
        }

    }

