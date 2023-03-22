using CRUD_Zoo.Catalogos;
using CRUD_Zoo.Models;
using CRUD_Zoo.Views;
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
    public class AnimalViewModel : INotifyPropertyChanged
    {
        //Catalogos
        HabitatCatalogo habitatCatalogo = new HabitatCatalogo();
        AnimalCatalogo animalCatalogo = new AnimalCatalogo();

        //Objetos modelos
        public Animal Animal { get; set; } = new Animal();
        public Habitat Habitat { get; set; } = new Habitat();

        //Listas
        public ObservableCollection<Habitat> listahabitats { get; set; } = new();
        public ObservableCollection<Animal> listaanimales { get; set; } = new();

        //PropiedadControlVistas
        private Accion operacion;
        public Accion Operacion
        {
            get { return operacion; }
            set { operacion = value; }
        }

        //Propiedad de errores de animal
        public string Error { get; set; }

        //Comandos
        public ICommand FiltrarAnimalesPorHabitatCommand { get; set; }
        public ICommand VerAgregarAnimalCommand { get; set; }
        public ICommand AgregarAnimalCommand { get; set; }
        public ICommand VerEliminarCommand { get; set; }
        public ICommand EliminarAnimalCommand { get; set; }
        public ICommand RegresarCommand { get; set; }
        public ICommand DeshacerFiltroCommand { get; set; }

        //Constructor
        public AnimalViewModel()
        {
            CargarAnimales();
            CargarHabitats();
            FiltrarAnimalesPorHabitatCommand = new RelayCommand(VerAnimalesXHabitat);
            VerAgregarAnimalCommand = new RelayCommand(VerAgregarAnimal);
            AgregarAnimalCommand = new RelayCommand(AgregarAnimal);
            VerEliminarCommand = new RelayCommand<int>(VerEliminar);
            EliminarAnimalCommand = new RelayCommand(EliminarAnimal);
            RegresarCommand = new RelayCommand(Regresar);
            DeshacerFiltroCommand = new RelayCommand(DeshacerFiltro);
        }

        private void DeshacerFiltro()
        {
            CargarAnimales();
            Actualizar();
        }

        public void CargarHabitats()
        {
            listahabitats.Clear();
            var proye = habitatCatalogo.GetAllHabitats();
            foreach (var item in proye)
            {
                listahabitats.Add(item);

            }
            Actualizar();
        }

        public void CargarAnimales()
        {
            listaanimales.Clear();
            var proye = animalCatalogo.GetAllAnimales();
            if (proye != null)
            {
                foreach (var item in proye)
                {
                    listaanimales.Add(item);
                }
            }
        }

        private void EliminarAnimal()
        {
            if (Animal is not null)
            {
                animalCatalogo.Delete(Animal);
                CargarAnimales();
                Regresar();
            }
        }

        private void VerEliminar(int id)
        {
            var temp = animalCatalogo.GetAnimalXId(id);
            if (temp is not null)
            {
                Animal = temp;
                Operacion = Accion.EliminarAnimales;
                Actualizar();
            }

        }

        private void Regresar()
        {
            Operacion = Accion.VerAnimales;
            Actualizar();
        }

        private void AgregarAnimal()
        {
            Error = "";
            if (animalCatalogo.Validar(Animal, out List<string> lista))
            {
                animalCatalogo.Create(Animal);
                //ActualizarTablaHabitat();
                habitatCatalogo.ActualizaHabitat(Animal.IdHabitat);
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

        private void VerAgregarAnimal()
        {
            Error = "";
            Animal = new Animal();
            Operacion = Accion.AgregarAnimales;
            Actualizar();
        }

        private void VerAnimalesXHabitat()
        {
            listaanimales.Clear();
            var proye = animalCatalogo.GetAnimalesXHabitat(Habitat);
            foreach (var item in proye)
            {
                listaanimales.Add(item);
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
