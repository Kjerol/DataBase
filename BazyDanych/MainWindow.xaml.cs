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
        public MainWindow()
        {
            InitializeComponent();
            LoadGrid();
        }

        SqlConnection con = new SqlConnection(@"Data Source=(localdb)\Local;Initial Catalog=NewDB;Integrated Security=True");
        public void clearData()
        {
            Imie_txt.Clear();
            Wiek_txt.Clear();
            Plec_txt.Clear();
            Miasto_txt.Clear();
        }

        public void LoadGrid()
        {
            SqlCommand cmd = new SqlCommand("select * from FirstTable", con);
            DataTable dt = new DataTable();
            con.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            con.Close();
            datagrid.ItemsSource = dt.DefaultView;
        }
        private void ClearPrz_Click(object sender, RoutedEventArgs e)
        {
            clearData();
        }

        public bool isValid()
        {
            if(Imie_txt.Text == String.Empty)
            {
                MessageBox.Show("Imie jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Wiek_txt.Text == String.Empty)
            {
                MessageBox.Show("Wiek jest wymagany", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Plec_txt.Text == String.Empty)
            {
                MessageBox.Show("Plec jest wymagana", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (Miasto_txt.Text == String.Empty)
            {
                MessageBox.Show("Miasto jest wymagane", "Nie udało się", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void WstawPrz_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (isValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO FirstTable VALUES (@Imie, @Wiek, @Plec, @Miasto)", con);
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@Imie", Imie_txt.Text);
                    cmd.Parameters.AddWithValue("@Wiek", Wiek_txt.Text);
                    cmd.Parameters.AddWithValue("@Plec", Plec_txt.Text);
                    cmd.Parameters.AddWithValue("@Miasto", Miasto_txt.Text);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    LoadGrid();
                    MessageBox.Show("Udana rejestracja", "Zapisano", MessageBoxButton.OK, MessageBoxImage.Information);
                    clearData();
                }
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
