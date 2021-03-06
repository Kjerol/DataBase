using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace BazyDanych
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        /// <summary>
        /// Inicjuje nową instancję <see cref="MainWindow" />.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        /// <summary>
        /// Połączenie
        /// </summary>
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=RTV_Karol_Chodzba;Integrated Security=True");


        /// <summary>
        /// Czyści dane.
        /// </summary>
        public void clearData()
        {
            Imie_txt.Clear();
            Nazwisko_txt.Clear();
            Miejscowosc_txt.Clear();
            Ulica_txt.Clear();
            Szukaj_txt.Clear();
            Lokal_txt.Clear();
        }

        /// <summary>
        /// Wczytuje zawartość.
        /// </summary>
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from Klient", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        /// <summary>Obsługuje zdarzenie ClearPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e"> <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void ClearPrz_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        /// <summary>Okresla prawidłowość instancji.</summary>
        /// <returns>
        ///   <c>true</c> jesli instancja jest prawidłowa; inaczej, <c>false</c>.</returns>
        public bool isValid()
        {
            if(Imie_txt.Text == String.Empty)
            {
                MessageBox.Show("Imie jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Nazwisko_txt.Text == String.Empty)
            {
                MessageBox.Show("Nazwisko jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Miejscowosc_txt.Text == String.Empty)
            {
                MessageBox.Show("Miejscowosc jest wymagana", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Ulica_txt.Text == String.Empty)
            {
                MessageBox.Show("Ulica jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        /// <summary>Obsługuje zdarzenia WstawPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e"> <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void WstawPrz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO Klient VALUES (@Imie, @Nazwisko, @Miejscowosc, @Ulica, @Lokal)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Imie", Imie_txt.Text);
                    cmd.Parameters.AddWithValue("@Nazwisko", Nazwisko_txt.Text);
                    cmd.Parameters.AddWithValue("@Miejscowosc", Miejscowosc_txt.Text);
                    cmd.Parameters.AddWithValue("@Ulica", Ulica_txt.Text);
                    cmd.Parameters.AddWithValue("@Lokal", Lokal_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Wstawianie udane", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Obsługuje zdarzenia UsunPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void UsunPrz_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Klient where ID = " +Szukaj_txt.Text+ " ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rekord zostal usuniety", "Usunieto", MessageBoxButton.OK, MessageBoxImage.Information);
                con.Close();
                clearData();
                LoadGrid();
                con.Close();
            }
            catch(SqlException ex)
            {
                MessageBox.Show("Nie usunieto" +ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        /// <summary>Obsługuje zdarzenie AktualizujPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e"> <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void AktualizujPrz_Click(object sender, RoutedEventArgs e)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Klient set Imie = '"+Imie_txt.Text+ "', Nazwisko = '"+Nazwisko_txt.Text+ "', Miejscowosc = '"+Miejscowosc_txt.Text+ "', Ulica = '"+Ulica_txt.Text+"' WHERE ID = '"+Szukaj_txt.Text+"' ", con);
            try
            {
                cmd.ExecuteNonQuery();
                MessageBox.Show("Rekord zaktualizowany", "Zaktualizowany", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                con.Close();
                clearData();
                LoadGrid();
            }
        }

        /// <summary>Obsługuje zdarzenie Button.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var Window1 = new Window1();
            Window1.Show();
        }
    }
}
