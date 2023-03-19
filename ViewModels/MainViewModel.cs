using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace CRUD_Zoo.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        //ViewModels
        AnimalViewModel animalViewModel = new AnimalViewModel();
        HabitatViewModel habitatViewModel = new HabitatViewModel();

        //Comandos
        public ICommand NavegarAnimalCommand { get; set; }
        public ICommand NavegarHabitatCommand { get; set; }


        private object viewactual;
        public object ViewModelAactual
        {
            get { return viewactual; }
            set { viewactual = value; Actualizar(); }
        }


        //Constructor
        public MainViewModel()
        {
            NavegarAnimalCommand = new RelayCommand(NavegarAnimal);
            NavegarHabitatCommand = new RelayCommand(NavegarHabitat);

        }

        private void NavegarHabitat()
        {
            ViewModelAactual = habitatViewModel;
        }

        private void NavegarAnimal()
        {
            ViewModelAactual = animalViewModel;
        }

        private void Actualizar(string? property = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }
        public event PropertyChangedEventHandler? PropertyChanged;
    }
}
