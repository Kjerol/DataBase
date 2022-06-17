using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
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
using System.Windows.Shapes;

namespace BazyDanych
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        /// <summary>
        /// Inicjuje nową instancję <see cref="Window1" /> klasy.
        /// </summary>
        public Window1()
        {
            InitializeComponent();
            LoadGrid();
        }

        /// <summary>
        /// Połączenie
        ///</summary>
        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=RTV_Karol_Chodzba;Integrated Security=True");




        /// <summary>
        /// Czyści dane.
        /// </summary>
        public void clearData()
        {
            Produkt_ID_txt.Clear();
            Data_sprz_txt.Clear();
            Klient_ID_txt.Clear();
        }

        /// <summary>
        /// Ładuje zawartość.
        /// </summary>
        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from Sprzedaz", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }

        /// <summary>Kontroluje zdarzenie WstawPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e"> <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        
        private void ClearPrz_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }
        /// <summary>Okresla prawidłowość instancji.</summary>
        /// <returns>
        /// <c>true</c> jesli instancja jest prawidłowa; inaczej, <c>false</c>.</returns>
         public bool isValid()
        {
            if(Produkt_ID_txt.Text == String.Empty)
            {
                MessageBox.Show("ID produktu jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Data_sprz_txt.Text == String.Empty)
            {
                MessageBox.Show("Data sprzedazy jest wymagana", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Klient_ID_txt.Text == String.Empty)
            {
                MessageBox.Show("ID klienta jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    SqlCommand cmd = new SqlCommand("INSERT INTO Sprzedaz VALUES (@Produkt_ID, @Data_sprz, @Klient_ID)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Produkt_ID", Produkt_ID_txt.Text);
                    cmd.Parameters.AddWithValue("@Data_sprz", Data_sprz_txt.Text);
                    cmd.Parameters.AddWithValue("@Klient_ID", Klient_ID_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Wstawianie udane", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        /// <summary>Kontroluje zdarzenie AktualizujPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e">  <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>
        private void AktualizujPrz_Click(object sender, RoutedEventArgs e)

            {
            con.Open();
            SqlCommand cmd = new SqlCommand("update Sprzedaz set Produkt_ID = '"+Produkt_ID_txt.Text+ "', Data_Sprz = '"+Data_sprz_txt.Text+ "', Klient_ID = '"+Klient_ID_txt.Text+ "' WHERE ID = '"+Szukaj_txt.Text+"' ", con);
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
        /// <summary>Obsługuje zdarzenia UsunPrz.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>

        private void UsunPrz_Click(object sender, RoutedEventArgs e)
        {{
            con.Open();
            SqlCommand cmd = new SqlCommand("delete from Sprzedaz where ID = " +Szukaj_txt.Text+ " ", con);
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

        }

        /// <summary>Obsługuje zdarzenia Button.</summary>
        /// <param name="sender">Źródło zdarzenia.</param>
        /// <param name="e">The <see cref="RoutedEventArgs" /> Instancja zawierająca dane zdarzenia.</param>

        private void Button_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
