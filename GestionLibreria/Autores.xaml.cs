using Negocios.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
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
    /// Lógica de interacción para Autores.xaml
    /// </summary>
    public partial class Autores : Window
    {
        public Autores()
        {
            InitializeComponent();
            CargarGridAutores();
            CargaNacionalidades();
        }

        public static  int IdAutor=0;
        void CargarGridAutores()
        {
            try
            {

                var url = "http://localhost:42580/Autores/ListarAutores";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    DataTable dt = (DataTable)JsonConvert.DeserializeObject(result, (typeof(DataTable)));

                    gvAutores.ItemsSource = dt.DefaultView;// dt.DefaultView;


                    Console.WriteLine(httpResponse.StatusCode);


                }
            }catch
            {
                MessageBox.Show("ha Ocurrido un Error");
            }
           
        }
        void CargaNacionalidades()
        {
            INacionalidadRepository na = new INacionalidadRepository();
            DataTable dt = na.ListarNacionalidades();
            cbxNacionalidad.ItemsSource = dt.DefaultView;


        }

        
        private void cbxNacionalidad_SelectedChanged(object sender, RoutedEventArgs e)
        {
            ICiudadRepository ci = new ICiudadRepository();
            DataTable dt = ci.ListarCiudadXNacionalidad(Convert.ToInt32(cbxNacionalidad.SelectedValue));
            cbxCiudad.ItemsSource = dt.DefaultView;
        }

        private void btnGuardar_Click(object sender, RoutedEventArgs e)
        {
            try
            {

            
            const string Comill = "\"";

            string Nombre = Comill + txtNombre.Text + Comill;
            string IdCiudad = cbxCiudad.SelectedValue.ToString();
            string Sexo = Comill + cbxSexo.SelectedValue.ToString() + Comill;

            if (IdAutor == 0)
            {

            var url = "http://localhost:42580/Autores/CreateAutor";
            var httpRequest = (HttpWebRequest)WebRequest.Create(url);
            httpRequest.Method = "POST";
            httpRequest.ContentType = "application/json";
            string data = @"{""nombre"":"+Nombre+ @",""id_Ciudad"":" + IdCiudad + @",""sexo"":" + Sexo + " }";



            using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
            {
                streamWriter.Write(data);
            }

            var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var result = streamReader.ReadToEnd();
                MessageBox.Show("Autor Registrado Con Exito");
            }
            }
            else

            {
                var url = "http://localhost:42580/Autores/EditarAutor";
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "POST";
                httpRequest.ContentType = "application/json";

                string data = @"{""Id"":" + IdAutor + @",""nombre"":" + Nombre + @",""id_Ciudad"":" + IdCiudad + @",""sexo"":" + Sexo + " }";
                using (var streamWriter = new StreamWriter(httpRequest.GetRequestStream()))
                {
                    streamWriter.Write(data);
                }

                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    MessageBox.Show("Autor Actualizado Con Exito");
                }
            }
            
            LimpiarControles();
        }catch
            {
                MessageBox.Show("ha Ocurrido un Error");
            }
}

        protected void LimpiarControles()
        {
            CargarGridAutores();
            IdAutor = 0;
            txtNombre.Text = "";
        }
        private void btnCancelar_Click(object sender, RoutedEventArgs e)
        {

        }

        private void BtnEditar_Click(object sender, RoutedEventArgs e)
        {
            DataRowView rowview = gvAutores.SelectedItem as DataRowView;

                IdAutor = Convert.ToInt32(rowview.Row.ItemArray[0].ToString());
                txtNombre.Text = rowview.Row.ItemArray[1].ToString();
                cbxNacionalidad.SelectedValue= rowview.Row.ItemArray[6].ToString();
                cbxCiudad.SelectedValue = rowview.Row.ItemArray[4].ToString();
                cbxSexo.Text = rowview.Row.ItemArray[3].ToString();


        }

        private void btnEliminar_Click(object sender, RoutedEventArgs e)
        {
            try
            { 
            if (MessageBox.Show("Esta Seguro De Eliminar El Autor?","Confirmation", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
            {
                DataRowView rowview = gvAutores.SelectedItem as DataRowView;

                var url = "http://localhost:42580/Autores/BorrarAutor?Id="+rowview.Row.ItemArray[0].ToString() ;
                var httpRequest = (HttpWebRequest)WebRequest.Create(url);
                httpRequest.Method = "DELETE";


                var httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    var result = streamReader.ReadToEnd();
                    if (result=="-1")
                    {
                        MessageBox.Show("No es Posible eliminar el Autor");
                        return;
                    }
                }
                CargarGridAutores();
                
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
