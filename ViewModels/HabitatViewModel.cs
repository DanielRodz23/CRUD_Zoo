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
using CRUD_Zoo.Views;


namespace CRUD_Zoo.ViewModels
{
    public class HabitatViewModel : INotifyPropertyChanged
    {
        HabitatCatalogo catalogo = new();
        //Lista Hbabitats
        public ObservableCollection<Habitat> listahabitats { get; set; } = new();
        //Habitat de la clase
        public Habitat Habitat { get; set; } = new();
        //Propiedad para controlar las vistas

        private Accion operacion;
        public Accion Operacion
        {
            get { return operacion; }
            set { 
                operacion = value;
                
            }
        }
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
            if (Habitat is not null)
            {

                catalogo.Delete(Habitat);
                Regresar();

            }
        }

        private void VerEliminar(int id)
        {
            Habitat = catalogo.GetHabitatXId(id);
            if (Habitat != null)
            {
                Operacion = Accion.EliminarHabitats;
                Actualizar();
            }
        }

        private void Regresar()
        {
            Operacion = Accion.VerHabitats;
            Actualizar();
        }

        private void AgregarHabitat()
        {
            Error = "";
            if (catalogo.Validar(Habitat, out List<string> lista))
            {
                catalogo.Create(Habitat);

                CargarHabitats();
                var dict = catalogo.GetUltimoAgregado();
                AnimalViewModel animal= new AnimalViewModel();
                animal.diccionariohabitats.Add(dict.Id,dict.Capacidad);
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
            Habitat = new Habitat();
            Operacion = Accion.AgregarHabitats;
            Actualizar();
        }

        public void CargarHabitats()
        {
            listahabitats.Clear();
            listahabitats = new();

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
