using Domain.Models;
using Domain.ValueObjects;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace Presentation.Forms
{
    /// <summary>
    /// Lógica de interacción para ClienteForm.xaml
    /// </summary>
    public partial class ClienteForm : UserControl
    {
        private ClienteModel cliente = new ClienteModel();

        private string imagenpath;
        bool isModifying = false;
        string idUsuario;
        public string message;
        public ClienteForm()
        {
            InitializeComponent();
            FillGenero();
        }
        private void FillGenero()
        {
            GeneroCombox.Items.Add("Masculino");
            GeneroCombox.Items.Add("Femenino");
        }
        public void SetData(string id, string imgPath, string apodo, string pin, string nombre, string aPaterno, string aMaterno, string correo, string fNacimiento, string peso, string estatura, string genero)
        {
            isModifying = true;
            idUsuario = id;
            imagenpath = imgPath;
            try
            {
                ImagenBrush.ImageSource = new BitmapImage(new Uri(imgPath));
            }
            catch
            {
                MessageBox.Show("No se encontro imagen");
            }
            ApodoTextBox.Text = apodo;
            PinTextBox.Password = pin;
            NombreTextBox.Text = nombre;
            APaternoTextBox.Text = aPaterno;
            AMaternoTextBox.Text = aMaterno;
            CorreoTextBox.Text = correo;

            if (genero.Equals("M"))
            {
                GeneroCombox.SelectedIndex = 0;
            }
            else
                GeneroCombox.SelectedIndex = 1;
            try
            {
                FNacimientoPicker.SelectedDate = Convert.ToDateTime(fNacimiento);
            }
            catch
            {
                MessageBox.Show("Es administrador no cliente");
            }
            PesoTextBox.Text = peso;
            EstaturaTextBox.Text = estatura;
        }

        private void GuardarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (isModifying == true)
            {
                cliente.EntityState = EntityState.Modified;
                cliente.Id = idUsuario;
            }
            else
                cliente.EntityState = EntityState.Added;

            cliente.Apodo = ApodoTextBox.Text;
            cliente.Pin = PinTextBox.Password;
            cliente.Nombre = NombreTextBox.Text;
            cliente.ApellidoPaterno = APaternoTextBox.Text;
            cliente.ApellidoMaterno = AMaternoTextBox.Text;
            cliente.Correo = CorreoTextBox.Text;
            cliente.ImgPath = imagenpath;
            cliente.FNacimiento = FNacimientoPicker.SelectedDate.ToString().Substring(0,10);
            cliente.Peso = PesoTextBox.Text;
            cliente.Estatura = EstaturaTextBox.Text;
            cliente.Genero = GeneroCombox.Text.Substring(0, 1);

            bool validation = new Helps.DataValidation(cliente).Validate();

            if (validation == true)
            {
                string result = cliente.Savechanges();
                MessageBox.Show(result);
                //message = result;
                //DialogResult = true;
            }
        }

        private void SubirImagenBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog op = new OpenFileDialog
            {
                Title = "Selecciona una imagen",
                Filter = "All supported graphics|*.jpg;*.jpeg;*.png|" +
              "JPEG (*.jpg;*.jpeg)|*.jpg;*.jpeg|" +
              "Portable Network Graphic (*.png)|*.png"
            };

            string appPath = System.AppDomain.CurrentDomain.BaseDirectory;

            if (op.ShowDialog() == true)
            {
                try
                {
                    string iName = op.SafeFileName;
                    string filepath = op.FileName;
                    if (!File.Exists(appPath + iName))
                    {
                        File.Copy(filepath, appPath + iName);
                        imagenpath = appPath + iName;
                        ImagenBrush.ImageSource = new BitmapImage(new Uri(op.FileName));
                    }
                    else
                    {
                        MessageBox.Show("El archivo ya existe");
                        imagenpath = appPath + iName;
                        ImagenBrush.ImageSource = new BitmapImage(new Uri(op.FileName));
                    }
                    //ImgPathBox.Text = appPath + iName;
                }
                catch (Exception exp)
                {
                    MessageBox.Show("No es posible abrir el archivo " + exp.Message);
                }
            }
        }
    }
}
