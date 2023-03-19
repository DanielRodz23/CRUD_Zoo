using CRUD_Zoo.Catalogos;
using CRUD_Zoo.Models;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD_Zoo.ViewModels
{
    public class HabitatViewModel : INotifyPropertyChanged
    {
        HabitatCatalogo catalogo = new();
        //Lista Hbabitats
        public ObservableCollection<Habitat> listahabitats { get; set; } = new();
        //Habitat de la clase
        public Habitat habitat { get; set; } = new();
        //Propiedad para controlar las vistas
        public Accion Operacion { get; set; }
        //Comandos
        public ICommand CargarhabitatsCommand { get; set; }
        public ICommand VerAgregarCommand { get; set; }
        public ICommand AgregarHabitatCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand EliminarHabitatCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public string Error { get; set; }

        //Constructor
        public HabitatViewModel()
        {
            CargarHabitats();
            CargarhabitatsCommand = new RelayCommand(CargarHabitats);
            VerAgregarCommand = new RelayCommand(VerAgregar);
            AgregarHabitatCommand = new RelayCommand(AgregarHabitat);
            RegresarCommand = new RelayCommand(Regresar);
            VerEliminarCommand = new RelayCommand<int>(VerEliminar);
            EliminarHabitatCommand = new RelayCommand(EliminarHabitat);
        }

        private void EliminarHabitat()
        {
            if (habitat is not null)
            {

                catalogo.Delete(habitat);
                Regresar();

            }
        }

        private void VerEliminar(int id)
        {
            habitat = catalogo.GetHabitatXId(id);
            Operacion = Accion.EliminarHabitats;
            Actualizar();
        }

        private void Regresar()
        {
            Operacion = Accion.VerHabitats;
            Actualizar();
        }

        private void AgregarHabitat()
        {
            Error = null;
            if (catalogo.Validar(habitat, out List<string> lista))
            {
                catalogo.Create(habitat);
                CargarHabitats();
                Regresar();
            }
            else
            {
                foreach (var item in lista)
                {
                    Error = $"{Error} {item} {Environment.NewLine}";
                    Actualizar();
                }
            }
        }

        private void VerAgregar()
        {
            habitat = new Habitat();
            Operacion = Accion.VerHabitats;
            Actualizar();
        }

        private void CargarHabitats()
        {
            listahabitats.Clear();
            var proye = catalogo.GetAllHabitats();
            foreach (var item in proye)
            {
                listahabitats.Add(item);

            }
            Actualizar();
        }

        private void Actualizar(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
