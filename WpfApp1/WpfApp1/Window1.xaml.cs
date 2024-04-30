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
using static WpfApp1.Window3;

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    
    public partial class Window1 : Window
    {
        private string nazwa;
        private string producent;
        private float cena;
        private string kategoria;
        string connectionString = "data source=Amelia\\SQLEXPRESS;initial catalog=ShoppingList;trusted_connection=true";
        public Window1()
        {
            InitializeComponent();
            List<string> kategorieZBazy = PobierzKategorie();
            List<string> unikalneKategorie = kategorieZBazy.Distinct().ToList();
            unikalneKategorie.Insert(0, "Wszystkie produkty");

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

      
        private void Potwierdzenie_dodania_Click(object sender, RoutedEventArgs e)
        {
            nazwa = Nazwa_Produktu.Text;//odczytywanie z textboxów
            producent = Producent_produktu.Text;
            kategoria = Kategoria_produktu.Text;

            if (float.TryParse(Cena_produktu.Text, out cena))
            {
           
                try
                {
         
                    string connectionString = "data source=Amelia\\SQLEXPRESS;initial catalog=ShoppingList;trusted_connection=true";
                    using (SqlConnection connection = new SqlConnection(connectionString))
                    {
                 
                        string query = "INSERT INTO List  VALUES (@Nazwa, @Producent, @Cena, @Kategoria)";

                   
                        using (SqlCommand command = new SqlCommand(query, connection))
                        {

                            command.Parameters.AddWithValue("@Nazwa", nazwa);
                            command.Parameters.AddWithValue("@Producent", producent);
                            command.Parameters.AddWithValue("@Cena", cena);
                            command.Parameters.AddWithValue("@Kategoria",kategoria);
                            connection.Open();
                            int rowsAffected = command.ExecuteNonQuery();
                            if (rowsAffected > 0)
                            {
                                MessageBox.Show("Dane zostały zapisane poprawnie!");
                            }
                            else
                            {
                                MessageBox.Show("Wystąpił problem podczas zapisu danych!");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Wystąpił błąd: {ex.Message}");
                }
            }
            else
            {
                MessageBox.Show("Wprowadź poprawną wartość dla ceny produktu!");
            }
        }

        private void Window1_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if(e.LeftButton == MouseButtonState.Pressed)
                DragMove();
        }
        private void Zmniejszanie_Click1(object sender, RoutedEventArgs e)
        {
            WindowState = WindowState.Minimized;
        }

        private void Zamykanie_Click1(object sender, RoutedEventArgs e)
        {
            Window window = Window.GetWindow((Button)sender); // żeby zamykało tylko to okno
            window.Close();

        }
    }
}
