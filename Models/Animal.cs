using System;
using System.Collections.Generic;

namespace CRUD_Zoo.Models;

public partial class Animal
{
    public int Id { get; set; }

    public string Nombre { get; set; } = null!;

    public int IdHabitat { get; set; }

    public double? Peso { get; set; }

    public string? TipoAlimentacion { get; set; }

    public string? NivelPeligroDeExtincion { get; set; }

    public virtual Habitat IdHabitatNavigation { get; set; } = null!;
}
