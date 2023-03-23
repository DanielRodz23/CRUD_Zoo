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
            var a= contenedor.Habitat.Include(x=>x.Animal).OrderBy(x => x.Nombre);

            return a;
        }
        public void ActualizaHabitat(int id)
        {
            var a = contenedor.Habitat.FirstOrDefault(x=>x.Id==id);
            if (a == null)
            {
                a.Capacidad--;
                contenedor.Habitat.Update(a);
                contenedor.SaveChanges();
            }
        }
        public Habitat? GetHabitatXId(int id)
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
                string patron = @"^[a-zA-ZñÑ\s]+$";
                if (a.Nombre is not null)
                {
                    if (!Regex.IsMatch(a.Nombre, patron))
                        lista.Add("El nombre solo debe contener letras y/o espacios");
                }
                if (string.IsNullOrWhiteSpace(a.Nombre))
                    lista.Add("El nombre no puede quedar vacío");
                if (string.IsNullOrWhiteSpace(a.TipoHabitat))
                    lista.Add("El tipo de hábitat no puede quedar vacío");
                if (a.Capacidad == 0)
                    lista.Add("La capacidad no puede ser 0 o quedar vacía");
                if (string.IsNullOrWhiteSpace(a.Vegetacion))
                    lista.Add("La vegetación no puede quedar vacía");
                if (a.Tamano is null)
                    lista.Add("El tamaño no puede quedar vacío");
                if (a.Tamano == 0)
                    lista.Add("El valor de tamaño no puede ser 0");
            }
            return lista.Count == 0;
        }


    }
}
