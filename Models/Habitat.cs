using System;
using System.Collections.Generic;

namespace CRUD_Zoo.Models;

public partial class Habitat
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public string? TipoHabitat { get; set; }

    public int Capacidad { get; set; }

    public string? Vegetacion { get; set; }

    public double? Tamano { get; set; }

    public virtual ICollection<Animal> Animal { get; } = new List<Animal>();
}
