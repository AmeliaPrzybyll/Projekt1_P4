using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window3.xaml
    /// </summary>
    public partial class Window3 : Window
    {

        string connectionString = "data source=Amelia\\SQLEXPRESS;initial catalog=ShoppingList;trusted_connection=true";
        private List<Produkt> listaProduktow = new List<Produkt>();

        public Window3()
        {
            InitializeComponent();

            // Pobierz wszystkie produkty i zainicjuj listę
            listaProduktow = PobierzWszystkieProdukty();
            listaProduktow.Clear();
            List<string> nazwyProduktow = PobierzNazwyProduktow();
            Wyswietlanie_nazwy_lista.ItemsSource = nazwyProduktow;
            Wyswietlanie_nazwy_lista.SelectionChanged += Wyswietlanie_nazwy_SelectionChanged;
            List<string> nazwyProducenta = PobierzNazwyProducentow();
            Wyswietlanie_producenta_lista.ItemsSource = nazwyProducenta;
            Wyswietlanie_producenta_lista.SelectionChanged += Wyswietlanie_nazwy_producenta_SelectionChanged;
            List<decimal> ceny = PobierzCeny();
            Wyswietlanie_ceny_lista.ItemsSource = ceny;
            Wyswietlanie_ceny_lista.SelectionChanged += Wyswietlanie_ceny_SelectionChanged;

            // Wyczyść istniejące elementy ComboBox przed przypisaniem nowego źródła danych
            Kategorie.Items.Clear();

            // Pobierz wszystkie kategorie z bazy danych
            List<string> kategorieZBazy = PobierzKategorie();

            // Usuń duplikaty z listy kategorii
            List<string> unikalneKategorie = kategorieZBazy.Distinct().ToList();
            unikalneKategorie.Insert(0, "Wszystkie produkty");

            // Przypisz unikalne kategorie do ComboBoxa
            Kategorie.ItemsSource = unikalneKategorie;
        }

        private List<string> PobierzKategorie()
        {
            List<string> kategorie = new List<string>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Category FROM List";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string kategoria = reader.GetString(0);
                            kategorie.Add(kategoria);
                        }
                    }
                }
            }

            return kategorie;
        }

        

        private List<Produkt> PobierzProduktyWedlugKategorii(string kategoria)
        {
            List<Produkt> produkty = new List<Produkt>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;
                if (kategoria == "Wszystkie produkty")
                {
                    query = "SELECT ProductName, Producer, Price FROM List";
                }
                else
                {
                    query = "SELECT ProductName, Producer, Price FROM List WHERE Category = @kategoria";
                }

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    if (kategoria != "Wszystkie produkty")
                    {
                        command.Parameters.AddWithValue("@kategoria", kategoria);
                    }

                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaProduktu = reader.GetString(0);
                            string producentProduktu = reader.GetString(1);
                            decimal cenaProduktu = reader.GetDecimal(2);
                            cenaProduktu = Math.Round(cenaProduktu, 2); // Zaokrąglenie do dwóch miejsc po przecinku
                            produkty.Add(new Produkt { Nazwa = nazwaProduktu, Producent = producentProduktu, Cena = cenaProduktu });
                        }
                    }
                }
            }

            return produkty;
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

        private List<Produkt> PobierzWszystkieProdukty()
        {
            List<Produkt> produkty = new List<Produkt>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT ProductName, Producer, Price FROM List";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaProduktu = reader.GetString(0);
                            string producentProduktu = reader.GetString(1);
                            decimal cenaProduktu = reader.GetDecimal(2);
                            produkty.Add(new Produkt { Nazwa = nazwaProduktu, Producent = producentProduktu, Cena = cenaProduktu });
                        }
                    }
                }
            }

            return produkty;
        }

        private bool SprawdzCzyProduktIstnieje(string nazwaProduktu, string producentProduktu, decimal cenaProduktu)
        {
            bool produktIstnieje = false;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM List WHERE ProductName = @nazwa AND Producer = @producent AND Price=@cena";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nazwa", nazwaProduktu);
                    command.Parameters.AddWithValue("@producent", producentProduktu);
                    command.Parameters.AddWithValue("@cena", cenaProduktu);

                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    produktIstnieje = count > 0;
                }
            }
            return produktIstnieje;
        }

        private void Dodawanie_Do_listy_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych wprowadzonych przez użytkownika
            string nazwaProduktu = Nazwa_Produktu_lista.Text;
            string producentProduktu = Producent_produktu_lista.Text;
            decimal cenaProduktu;
            if (decimal.TryParse(Cena_produktu_lista.Text, out cenaProduktu))
            {
                // Sprawdzenie, czy podany produkt istnieje w bazie danych
                if (!SprawdzCzyProduktIstnieje(nazwaProduktu, producentProduktu, cenaProduktu))
                {
                    MessageBox.Show("Podany produkt nie istnieje w bazie danych!");
                }
                else
                {
                    // Dodanie nowego produktu do listy
                    listaProduktow.Add(new Produkt { Nazwa = nazwaProduktu, Producent = producentProduktu, Cena = cenaProduktu });

                    // Wyczyszczenie TextBoxów po dodaniu produktu
                    Nazwa_Produktu_lista.Text = "";
                    Producent_produktu_lista.Text = "";
                    Cena_produktu_lista.Text = "";
                }
            }
            else
            {
                MessageBox.Show("Nieprawidłowa cena produktu!");
            }
        }

        private void Kategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sprawdź, czy wybrano opcję "Wszystkie produkty"
            if (Kategorie.SelectedItem != null && Kategorie.SelectedItem.ToString() == "Wszystkie produkty")
            {
                // Wyświetl wszystkie produkty
                Wyswietlanie_nazwy_lista.ItemsSource = listaProduktow.Select(p => p.Nazwa);
                Wyswietlanie_producenta_lista.ItemsSource = listaProduktow.Select(p => p.Producent);
                Wyswietlanie_ceny_lista.ItemsSource = listaProduktow.Select(p => Math.Round(p.Cena, 2));
            }
            else if (Kategorie.SelectedItem != null)
            {
                // Pobierz wybraną kategorię
                string wybranaKategoria = (Kategorie.SelectedItem as string);

                // Ponownie załaduj dane na wszystkich listach z uwzględnieniem wybranej kategorii
                List<Produkt> produktyWKategorii = PobierzProduktyWedlugKategorii(wybranaKategoria);

                // Aktualizuj źródło danych dla każdego listboxa
                Wyswietlanie_nazwy_lista.ItemsSource = produktyWKategorii.Select(p => p.Nazwa);
                Wyswietlanie_producenta_lista.ItemsSource = produktyWKategorii.Select(p => p.Producent);
                Wyswietlanie_ceny_lista.ItemsSource = produktyWKategorii.Select(p => Math.Round(p.Cena, 2));
            }
        }

        private void Podglad_listy_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetlenie zawartości listy
            string lista = "Lista produktów:\n";
            foreach (Produkt produkt in listaProduktow)
            {
                lista += $"{produkt.Nazwa} - {produkt.Producent} - {produkt.Cena}\n";
            }
            MessageBox.Show(lista);
        }
        private void Usuwanie_listy_click(object sender, RoutedEventArgs e)
        {
            listaProduktow.Clear();
            if (listaProduktow.Count == 0)
            {
                MessageBox.Show("Lista została usunięta!");
            }
            else
            {
                MessageBox.Show("Nie udało się usunąć listy :(");
            }
        }
        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            // Wyczyszczenie listy przed zamknięciem aplikacji
            listaProduktow.Clear();
        }

        private void Zgranie_Click(object sender, RoutedEventArgs e)
        {
            string filePath = @"C:\Users\Ameli\OneDrive\Pulpit\lista_produktow.txt";

            try
            {
                // Tworzenie nowego pliku tekstowego lub nadpisywanie istniejącego
                using (StreamWriter writer = new StreamWriter(filePath))
                {
                    // Zapis zawartości listy do pliku
                    foreach (Produkt produkt in listaProduktow)
                    {
                        writer.WriteLine($"Nazwa: {produkt.Nazwa}, Producent: {produkt.Producent}, Cena: {produkt.Cena}");
                    }
                }

                MessageBox.Show("Zapisano zawartość listy do pliku lista_produktow.txt na pulpicie użytkownika Amelia.");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Wystąpił błąd podczas zapisu do pliku: {ex.Message}");
            }
        }

        private void Wyswietlanie_nazwy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Wyswietlanie_nazwy_lista.SelectedItem != null)
            {
                string selectedProductName = Wyswietlanie_nazwy_lista.SelectedItem.ToString();
                Nazwa_Produktu_lista.Text = selectedProductName;
            }
        }
        private void Wyswietlanie_nazwy_producenta_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Wyswietlanie_producenta_lista.SelectedItem != null)
            {
                string selectedProducerName = Wyswietlanie_producenta_lista.SelectedItem.ToString();
                Producent_produktu_lista.Text = selectedProducerName;
            }
        }
        private void Wyswietlanie_ceny_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (Wyswietlanie_ceny_lista.SelectedItem != null)
            {
                string selectedProducerName = Wyswietlanie_ceny_lista.SelectedItem.ToString();
                Cena_produktu_lista.Text = selectedProducerName;
            }
        }
        public class Produkt
        {
            public string Nazwa { get; set; }
            public string Producent { get; set; }
            public decimal Cena { get; set; }
        }
    }
}
