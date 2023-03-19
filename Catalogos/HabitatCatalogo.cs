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
    public class HabitatCatalogo
    {
        ZoologicoContext contenedor = new ZoologicoContext();

        public IEnumerable<Habitat> GetAllHabitats()
        {
            return contenedor.Habitat.OrderBy(x => x.Nombre);
        }
        public Habitat GetHabitatXId(int id)
        {
            return contenedor.Habitat.FirstOrDefault(x=>x.Id==id);

        }
        public void Create(Habitat a)
        {
            contenedor.Habitat.Add(a);
            contenedor.SaveChanges();
        }
        public void Delete(Habitat a)
        {
            contenedor.Habitat.Remove(a);
            contenedor.SaveChanges();
        }
        public bool Validar(Habitat a, out List<string> lista)
        {
            lista = new List<string>();
            if (a is not null)
            {
                if (string.IsNullOrWhiteSpace(a.Nombre))
                    lista.Add("El nombre no puede quedar vacío");
                if (string.IsNullOrWhiteSpace(a.TipoHabitat))
                    lista.Add("El tipo de hábitat no puede quedar vacío");
                if (a.Capacidad == 0)
                    lista.Add("La capacidad no puede ser 0");
            }
            return lista.Count == 0;
        }


    }
}
