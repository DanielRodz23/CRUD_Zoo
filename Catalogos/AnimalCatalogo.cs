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
        

        public IEnumerable<Animal> GetAllAnimales()
        {
            return contenedor.Animal.Include(x=>x.IdHabitatNavigation).OrderBy(x => x.Nombre);
        }
        public Animal? GetAnimalXId(int id)
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
            contenedor.Entry(a).Reload();
            contenedor.SaveChanges();
        }
        public void Delete(Animal a)
        {
            //call zoologico.spEliminarAnimal(5);
            contenedor.Database.ExecuteSqlRaw($"call zoologico.spEliminarAnimal({a.Id});");
            contenedor.Entry(a).State = EntityState.Deleted;
            contenedor.Entry(a).Reload();
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
                    if (!Regex.IsMatch(a.Nombre, patron) && a.Nombre!="")
                        lista.Add("El nombre solo debe contener letras y/o espacios.");
                }
                if (string.IsNullOrWhiteSpace(a.Nombre))
                  lista.Add("Debe escribir algún nombre para el animal.");
                 
                var hab = HayCapacidadEnHabitat(a);
                if (hab != null)
                {
                    if (hab.Capacidad == 0)
                        lista.Add($"No hay capacidad en el hábitat: {hab.Nombre}.");
                }
                if (a.IdHabitat == null)
                    lista.Add("Se debe elegir un hábitat para el animal.");
                if (string.IsNullOrWhiteSpace(a.NivelPeligroDeExtincion))
                    lista.Add("Debe elegir el nivel de amenaza de extinción que corre el animal.");
                if (a.Peso is null)
                    lista.Add("Debe escribir el peso del animal.");
                if (a.Peso ==0)
                    lista.Add("Un animal debe pesar más de 0 kg.");
                if (string.IsNullOrWhiteSpace(a.TipoAlimentacion))
                    lista.Add("Debe escribir el tipo de alimentación del animal.");
                if (a.IdHabitat == 0)
                    lista.Add("Debe elegir algún hábitat para el animal.");
            }
            return lista.Count == 0;
        }
    }
}
