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

namespace WpfApp1
{
    /// <summary>
    /// Logika interakcji dla klasy MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
           // AddButton.Content = "You click button";
           Window1 dodaj = new Window1();
            dodaj.Show();
        }

        private void DeleteButton_Click(object sender, RoutedEventArgs e)
        {
            Window2 usun = new Window2();  
            usun.Show();
        }

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
