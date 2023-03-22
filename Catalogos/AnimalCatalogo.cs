using CRUD_Zoo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CRUD_Zoo.Catalogos
{
    public class AnimalCatalogo
    {
        ZoologicoContext contenedor = new ZoologicoContext();
        //public AnimalCatalogo()
        //{
        //    var result = contenedor.Animal.AsNoTracking().ToList();

        //}

        public IEnumerable<Animal> GetAllAnimales()
        {
            return contenedor.Animal.OrderBy(x => x.Nombre);
        }
        public Animal GetAnimalXId(int id)
        {
            return contenedor.Animal.FirstOrDefault(x => x.Id == id);

        }
        public IEnumerable<Animal> GetAnimalesXHabitat(Habitat a)
        {
            return GetAllAnimales().Where(x => x.IdHabitat == a.Id);
        }
        public void Create(Animal a)
        {
            
            contenedor.Database.ExecuteSqlRaw($"call zoologico.spAgregarAnimal({a.IdHabitat}, '{a.Nombre}', {a.Peso}, '{a.TipoAlimentacion}', '{a.NivelPeligroDeExtincion}');");
            contenedor.SaveChanges();
        }
        public void Delete(Animal a)
        {
            //call zoologico.spEliminarAnimal(5);
            contenedor.Database.ExecuteSqlRaw($"call zoologico.spEliminarAnimal({a.Id});");
            contenedor.SaveChanges();
        }
        private Habitat HayCapacidadEnHabitat(Animal a)
        {
            return contenedor.Habitat.Where(x => x.Id == a.IdHabitat).FirstOrDefault();
        }
        public bool Validar(Animal a, out List<string> lista)
        {
            lista = new List<string>();
            if (a is not null)
            {
                string patron = @"^[a-zA-ZñÑ\s]+$";
                if (a.Nombre is not null)
                {
                    if (!Regex.IsMatch(a.Nombre, patron))
                        lista.Add("El nombre solo debe contener letras y/o espacios");
                }
                var hab = HayCapacidadEnHabitat(a);
                if (hab != null)
                {
                    if (hab.Capacidad == 0)
                        lista.Add($"No hay capacidad en el habitat: {hab.Nombre}");
                }
                if (string.IsNullOrWhiteSpace(a.Nombre))
                    lista.Add("El nombre no puede quedar vacío");
                if (a.IdHabitat == null)
                    lista.Add("El debe elegir un hábitat");
                if (string.IsNullOrWhiteSpace(a.NivelPeligroDeExtincion))
                    lista.Add("Debe elegir la amenaza de extinción que corre el animal");
                if (a.Peso is null)
                    lista.Add("El peso no debe quedar vacío");
                if (a.Peso ==0)
                    lista.Add("El peso no debe quedar en 0");
                if (string.IsNullOrWhiteSpace(a.TipoAlimentacion))
                    lista.Add("El tipo de alumentación no puede quedar vacío");
                if (a.IdHabitat == 0)
                    lista.Add("El campo de habitat no puede quedar sin seleccionar");
            }
            return lista.Count == 0;
        }
    }
}
