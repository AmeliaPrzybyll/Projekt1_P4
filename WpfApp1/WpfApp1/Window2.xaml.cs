using System;
using System.Collections.Generic;
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



namespace WpfApp1
{
    public partial class Window2 : Window
    {
        string connectionString = "data source=Amelia\\SQLEXPRESS;initial catalog=ShoppingList;trusted_connection=true";

        public Window2()
        {
            InitializeComponent();
            List<string> nazwyProduktow = PobierzNazwyProduktow();
            Wyswietlanie_nazwy.ItemsSource = nazwyProduktow;
            List<string> nazwyProducenta = PobierzNazwyProducentow();
            Wyswietlanie_producenta.ItemsSource = nazwyProducenta;
            List<decimal> ceny = PobierzCeny();
            Wyswietlanie_ceny.ItemsSource = ceny;
        }

        private List<decimal> PobierzCeny()
        {
            List<decimal> ceny = new List<decimal>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Price FROM List";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            decimal cena = reader.GetDecimal(0);
                            cena = Math.Round(cena, 2); // Zaokrąglenie do dwóch miejsc po przecinku
                            ceny.Add(cena);
                        }
                    }
                }
            }

            return ceny;
        }



        private List<string> PobierzNazwyProducentow()
        {
            List<string> nazwyProducentow = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Producer FROM List";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaProducenta = reader.GetString(0);
                            nazwyProducentow.Add(nazwaProducenta);
                        }
                    }
                }
            }

            return nazwyProducentow;
        }
        private List<string> PobierzNazwyProduktow()
        {
            List<string> nazwyProduktow = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName FROM List";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaProduktu = reader.GetString(0);
                            nazwyProduktow.Add(nazwaProduktu);
                        }
                    }
                }
            }

            return nazwyProduktow;
        }

        private string nazwa;
        private string producent;
        private void Usuwanie_Click(object sender, RoutedEventArgs e)
        {
            
                // Pobranie nazwy
                string nazwaProduktu = Nazwa_Produktu_usuwanie.Text;
                string producentProduktu = Producent_produktu_usuwanie.Text;

                // Sprawdzenie, czy produkt istnieje w bazie danych
                if (CzyProduktIstnieje(nazwaProduktu, producentProduktu))
                {
                    // Produkt istnieje, wykonaj operację usuwania
                    UsunProdukt(nazwaProduktu, producentProduktu);
                }
                else
                {
                    // Produkt nie istnieje, wyświetl komunikat o błędzie
                    MessageBox.Show("Podany produkt nie istnieje w bazie danych!");
                }
        }

            private bool CzyProduktIstnieje(string nazwaProduktu, string producentProduktu)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "SELECT COUNT(*) FROM List WHERE ProductName = @nazwa AND Producer = @producent";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nazwa", nazwaProduktu);
                        command.Parameters.AddWithValue("@producent", producentProduktu);
                        connection.Open();
                        int count = (int)command.ExecuteScalar();
                        return count > 0; // Zwraca true, jeśli produkt istnieje w bazie danych
                    }
                }
            }

            private void UsunProdukt(string nazwaProduktu, string producentProduktu)
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    string query = "DELETE FROM List WHERE ProductName = @nazwa AND Producer = @producent";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.Parameters.AddWithValue("@nazwa", nazwaProduktu);
                        command.Parameters.AddWithValue("@producent", producentProduktu);
                        connection.Open();
                        int rowsAffected = command.ExecuteNonQuery();
                        if (rowsAffected > 0)
                        {
                            MessageBox.Show("Produkt został pomyślnie usunięty z bazy danych.");
                        }
                        else
                        {
                            MessageBox.Show("Wystąpił problem podczas usuwania produktu z bazy danych.");
                        }
                    }
                }
            }





        
    }
}
