using CRUD_Zoo.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRUD_Zoo.Catalogos
{
    public class AnimalCatalogo
    {
        ZoologicoContext contenedor = new ZoologicoContext();
        public IEnumerable<Animal> GetAllAnimales()
        {
            return contenedor.Animal.OrderBy(x=>x.Nombre);
        }
        public Animal GetAnimalXId(int id)
        {
            return contenedor.Animal.FirstOrDefault(x => x.Id == id);

        }
        public IEnumerable<Animal> GetAnimalesXHabitat(Habitat a)
        {
            return GetAllAnimales().Where(x => x.IdHabitat == a.Id);
        }
        public void Create (Animal a)
        {
            contenedor.Database.ExecuteSqlRaw($"call zoologico.spAgregarAnimal({a.IdHabitat}, '{a.Nombre}', {a.Peso}, '{a.TipoAlimentacion}', '{a.NivelPeligroDeExtincion}');");
            contenedor.SaveChanges();
        }
        public void Delete (Animal a)
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
                var hab = HayCapacidadEnHabitat(a);
                if (hab.Capacidad == 0)
                    lista.Add($"No hay capacidad en el habitat: {hab.Nombre}");
                if (string.IsNullOrWhiteSpace(a.Nombre))
                    lista.Add("El nombre no puede quedar vacío");
                if (a.IdHabitat == null)
                    lista.Add("El debe elegir un hábitat");
                if (string.IsNullOrWhiteSpace(a.NivelPeligroDeExtincion))
                    lista.Add("Debe elegir la amenaza de extinción que corre el animal");
                if (a.Peso is null)
                    lista.Add("El peso no debe quedar vacío");
                if (string.IsNullOrWhiteSpace(a.TipoAlimentacion))
                    lista.Add("El tipo de alumentación no puede quedar vacío");
            }
            return lista.Count == 0;
        }
    }
}
