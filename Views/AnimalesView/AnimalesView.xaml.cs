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
using CRUD_Zoo.ViewModels;

namespace CRUD_Zoo.Views.AnimalesView
{
    /// <summary>
    /// Lógica de interacción para AnimalesView.xaml
    /// </summary>
    public partial class AnimalesView : UserControl
    {
        public AnimalesView()
        {
            InitializeComponent();
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var vw = this.DataContext as AnimalViewModel;
            if(vw != null)
            {
                vw.FiltrarAnimalesPorHabitatCommand.Execute(null);
            }

        }
    }
}
