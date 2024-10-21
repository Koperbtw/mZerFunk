using System.Data;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace mZerFunk
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }
        public static double ZnajdzMiejsceZerowe(Func<double, double> funkcja, double poczatek, double koniec, double tolerancja = 1e-12)
        {
            double srodek = 0.0;
            while ((koniec - poczatek) / 2.0 > tolerancja)
            {
                srodek = (poczatek + koniec) / 2.0;
                if (Math.Abs(funkcja(srodek)) < tolerancja)
                {
                    return srodek;
                }
                else if (funkcja(poczatek) * funkcja(srodek) < 0)
                {
                    koniec = srodek;
                }
                else
                {
                    poczatek = srodek;
                }
            }
            return srodek;
        }

        public static List<double> ZnajdzWszystkieMiejscaZerowe(Func<double, double> funkcja, double poczatekZakresu, double koniecZakresu, double krok = 0.001, double tolerancja = 1e-12)
        {
            List<double> miejscaZerowe = new List<double>();
            for (double x = poczatekZakresu; x <= koniecZakresu; x += krok)
            {
                double f1 = funkcja(x);
                double f2 = funkcja(x + krok);

                if (Math.Abs(f1) < tolerancja)
                {
                    if (!miejscaZerowe.Contains(x))
                    {
                        miejscaZerowe.Add(x);
                    }
                }
                else if (f1 * f2 < 0)
                {
                    double miejsceZerowe = ZnajdzMiejsceZerowe(funkcja, x, x + krok, tolerancja);
                    if (!miejscaZerowe.Contains(miejsceZerowe))
                    {
                        miejscaZerowe.Add(miejsceZerowe);
                    }
                }
            }
            return miejscaZerowe;
        }

        private void btnOblicz_Click(object sender, RoutedEventArgs e)
        {
            Func<double, double> funkcja = x => (x - 1) * (x - 1) - 2000;

            double poczatek = Convert.ToDouble(txtPoczatek.Text);
            double koniec = Convert.ToDouble(txtKoniec.Text);

            List<double> wszystkieMiejscaZerowe = ZnajdzWszystkieMiejscaZerowe(funkcja, poczatek, koniec);

            lblWynik2.Content = "Miejsca zerowe: ";
            foreach (var miejsceZerowe in wszystkieMiejscaZerowe)
            {
                lblWynik2.Content += miejsceZerowe.ToString("F12") + " ";
            }
        }
    }
}