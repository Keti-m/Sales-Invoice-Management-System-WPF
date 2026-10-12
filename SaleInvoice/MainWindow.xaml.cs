using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace SaleInvoice
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            // Lidh eventet e butonave me metodat e tyre
            btnKliente.Click += BtnKliente_Click;
            btnArtikuj.Click += BtnArtikuj_Click;
            btnFatura.Click += BtnFatura_Click;
        }

        private void BtnKliente_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new KlienteControl());
        }

        private void BtnArtikuj_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new ArtikujControl());
        }

        private void BtnFatura_Click(object sender, RoutedEventArgs e)
        {
            ContentGrid.Children.Clear();
            ContentGrid.Children.Add(new FaturaControl());
        }
    }
}
