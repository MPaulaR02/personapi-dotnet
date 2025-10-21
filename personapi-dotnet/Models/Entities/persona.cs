using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("persona")]
public partial class persona
{
    [Key]
    public long cc { get; set; }

    [StringLength(45)]
    [Unicode(false)]
    public string nombre { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string apellido { get; set; } = null!;

    [StringLength(1)]
    [Unicode(false)]
    public string genero { get; set; } = null!;

    public int? edad { get; set; }

    [InverseProperty("cc_perNavigation")]
    public virtual ICollection<estudio> estudios { get; set; } = new List<estudio>();

    [InverseProperty("duenioNavigation")]
    public virtual ICollection<telefono> telefonos { get; set; } = new List<telefono>();
}
