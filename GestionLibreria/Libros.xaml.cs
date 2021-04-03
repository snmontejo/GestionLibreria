using Negocios.Data;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GestionLibreria
{
    /// <summary>
    /// Lógica de interacción para Libros.xaml
    /// </summary>
    public partial class Libros : Window
    {
        public Libros()
        {
            InitializeComponent();
            CargaAutores();
            CargarGridLibros();
        }
        public static int IdLibro = 0;
        void CargarGridLibros()
        {
            try
            { 
            var url = "http://localhost:42580/Libros/ListarLibros";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                DataTable dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));

                gvLibros.ItemsSource = dt.DefaultView;// dt.DefaultView;


                Console.WriteLine(httpResponse.StatusCode);


            }
        }catch
            {
                MessageBox.Show("ha Ocurrido un Error");
            }

}
        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            const string Comill = "\"";

            string Nombre = Comill + txtNombre.Text + Comill;
            string FehchaCreacion = Comill + Convert.ToDateTime(dtpFechaEscritura.Text).ToString("yyyy-MM-dd") + Comill;


            if (IdLibro == 0)
            {

                var url = "http://localhost:42580/Libros/CreateLibro";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";
                string data = @"{""Nombre"":" + Nombre + @",""FechaEscritura"":" + FehchaCreacion + @",""Costo"":" + txtCosto.Text + @",""Id_Autor"":" + cbxAutores.SelectedValue + " }";



                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show("Libro Registrado Con Exito");
                }
            }
            else

            {
                var url = "http://localhost:42580/Libros/EditarLibro";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";

                string data = @"{""Id"":" + IdLibro + @",""Nombre"":" + Nombre + @",""FechaEscritura"":" + FehchaCreacion + @",""Costo"":" + txtCosto.Text + @",""Id_Autor"":" + cbxAutores.SelectedValue + " }";
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show("Libro Actualizado Con Exito");
                }
            }

            LimpiarControles();
        }catch
            {
                MessageBox.Show("ha Ocurrido un Error");
            }
}
        void CargaAutores()
        {
            IAutorRepository na = new IAutorRepository();
            DataTable dt = na.ListarAutores();
            cbxAutores.ItemsSource = dt.DefaultView;
        }
        protected void LimpiarControles()
        {
            CargarGridLibros();
            IdLibro = 0;
            txtNombre.Text = "";
            txtCosto.Text = "";
            dtpFechaEscritura.Text = "";
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }
        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DataRowView rowview = gvLibros.SelectedItem as DataRowView;

                IdLibro = Convert.ToInt32(rowview.Row.ItemArray[0].ToString());
                txtNombre.Text = rowview.Row.ItemArray[1].ToString();
                dtpFechaEscritura.Text = rowview.Row.ItemArray[2].ToString();
                txtCosto.Text = rowview.Row.ItemArray[3].ToString();
                cbxAutores.SelectedValue = rowview.Row.ItemArray[4].ToString();
            }catch
            {
                MessageBox.Show("ha Ocurrido un Error");
            }
}
        private void btnEliminar_Click(object sender, RoutedEventArgs e)
            {
                try
                {
                    if (MessageBox.Show("Esta Seguro De Eliminar El Libro?", "Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                    {
                        DataRowView rowview = gvLibros.SelectedItem as DataRowView;

                        var url = "http://localhost:42580/Libros/BorrarLibro?Id=" + rowview.Row.ItemArray[0].ToString();
                        var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                        httpRequest.Method = "DELETE";


                        var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                        using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                        {
                            var result = streamReader.ReadToEnd();
                        }
                        CargarGridLibros();
                        MessageBox.Show("Eliminado Con Exito");

                        Console.WriteLine(httpResponse.StatusCode);
                    }
                    else
                    {

                    }
                }
                catch
                {
                    MessageBox.Show("ha Ocurrido un Error");
                }

            
        }
    }
}
