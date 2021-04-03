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

namespace GestionLibreria
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

        private void ListarAutoresItem_Click(object sender, RoutedEventArgs e)
        {
            Autores form = new Autores();
            form.ShowDialog();
        }
        private void ListarLibros_Click(object sender, RoutedEventArgs e)
        {
            Libros form = new Libros();
            form.ShowDialog();
        }

    }
}
