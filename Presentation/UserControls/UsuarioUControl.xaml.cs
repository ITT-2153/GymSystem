using Presentation.Forms;
using Presentation.Windows;
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

namespace Presentation.UserControls
{
    /// <summary>
    /// Lógica de interacción para UsuarioUControl.xaml
    /// </summary>
    public partial class UsuarioUControl : UserControl
    {
        public UsuarioUControl()
        {
            InitializeComponent();
        }
        private void AgregarBtn_Click(object sender, RoutedEventArgs e)
        {
            UsuarioForm form = new UsuarioForm();
            foreach (Window window in Application.Current.Windows)
            {
                if (window.GetType() == typeof(Dashboard))
                {
                    (window as Dashboard).SwitchScreen(form, "Usuarios • Agregar usuario");
                }
            }
        }
    }
}
