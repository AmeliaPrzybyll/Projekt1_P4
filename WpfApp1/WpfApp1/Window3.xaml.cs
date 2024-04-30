
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

            List<decimal> ceny = PobierzCeny();


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

        private void Kategorie_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Sprawdź, czy wybrano opcję "Wszystkie produkty"
            if (Kategorie.SelectedItem.ToString() == "Wszystkie produkty")
            {
                // Wyświetl wszystkie produkty
                Wyswietlanie_nazwy_lista.ItemsSource = listaProduktow.Select(p => p.Nazwa);
            }
            else
            {
                // Pobierz wybraną kategorię
                string wybranaKategoria = (Kategorie.SelectedItem as string);

                // Ponownie załaduj dane na wszystkich listach z uwzględnieniem wybranej kategorii
                List<Produkt> produktyWKategorii = PobierzProduktyWedlugKategorii(wybranaKategoria);

                // Aktualizuj źródło danych dla każdego listboxa
                Wyswietlanie_nazwy_lista.ItemsSource = produktyWKategorii.Select(p => p.Nazwa);

            }
        }

        private List<Produkt> PobierzProduktyWedlugKategorii(string kategoria)
        {
            List<Produkt> produkty = new List<Produkt>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query;
                if (kategoria == "Wszystkie produkty")
                {
                    query = "SELECT ProductName FROM List";
                }
                else
                {
                    query = "SELECT ProductName FROM List WHERE Category = @kategoria";
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

                            produkty.Add(new Produkt { Nazwa = nazwaProduktu });
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
                string query = "SELECT ProductName FROM List";

                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nazwaProduktu = reader.GetString(0);

                            produkty.Add(new Produkt { Nazwa = nazwaProduktu });
                        }
                    }
                }
            }

            return produkty;
        }

        private void Usuwanie_listy_click(object sender, RoutedEventArgs e)
        {
            listaProduktow.Clear(); // Usuń wszystkie elementy z listy

            MessageBox.Show("Lista została usunięta!");
        }


        private void Dodawanie_Do_listy_Click(object sender, RoutedEventArgs e)
        {
            // Pobranie danych wprowadzonych przez użytkownika
            string nazwaProduktu = Nazwa_Produktu_lista.Text;

            // Sprawdzenie, czy podany produkt istnieje w bazie danych
            if (SprawdzCzyProduktIstnieje(nazwaProduktu))
            {
                // Pobierz cenę produktu z bazy danych
                decimal cenaProduktu = PobierzCeneProduktu(nazwaProduktu);
                cenaProduktu = Math.Round(cenaProduktu, 2);
                // Dodanie nowego produktu do listy wraz z ceną
                listaProduktow.Add(new Produkt { Nazwa = nazwaProduktu, Cena = cenaProduktu });

                // Wyczyszczenie TextBoxów po dodaniu produktu
                Nazwa_Produktu_lista.Text = "";

                // Odśwież wyświetlanie listy
                //OdswiezListeProduktow();
            }
            else
            {
                MessageBox.Show("Podany produkt nie istnieje w bazie danych!");
            }
        }
        private decimal PobierzCeneProduktu(string nazwaProduktu)
        {
            decimal cena = 0;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT Price FROM List WHERE ProductName = @nazwaProduktu";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nazwaProduktu", nazwaProduktu);
                    connection.Open();
                    object result = command.ExecuteScalar();
                    if (result != null)
                    {
                        cena = Convert.ToDecimal(result);
                    }
                }
            }

            return cena;
        }
        private bool SprawdzCzyProduktIstnieje(string nazwaProduktu)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM List WHERE ProductName = @nazwa ";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@nazwa", nazwaProduktu);


                    connection.Open();
                    int count = (int)command.ExecuteScalar();
                    return count > 0;
                }
            }
        }
      
        private void Podglad_listy_Click(object sender, RoutedEventArgs e)
        {
            // Wyświetlenie zawartości listy
            string lista = "Lista produktów:\n";
            decimal suma = 0; // Zmienna do przechowywania sumy cen produktów
            foreach (Produkt produkt in listaProduktow)
            {
                // Dodajemy cenę produktu do sumy
                suma += produkt.Cena;
                // Wyświetlamy nazwę produktu i jego cenę
                lista += $"{produkt.Nazwa} - {produkt.Cena} PLN\n";
            }
            // Zaokrąglamy sumę do dwóch miejsc po przecinku
            suma = Math.Round(suma, 2);
            // Wyświetlamy sumę całkowitą
            lista += $"\nSuma: {suma} PLN";
            MessageBox.Show(lista);
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
                        writer.WriteLine($"Nazwa: {produkt.Nazwa}, Cena: {produkt.Cena}");
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

        private void Window_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
                DragMove();

        }

        private void Zmniejszanie_Click(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Zamykanie_Click(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((Button)sender);
            window.Close();
        }
        public class Produkt
        {
            public string Nazwa { get; set; }
            public string Producent { get; set; }
            public decimal Cena { get; set; }
        }
    }
}
