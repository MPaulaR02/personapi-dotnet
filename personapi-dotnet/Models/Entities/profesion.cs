using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("profesion")]
public partial class profesion
{
    [Key]
    public int id { get; set; }

    [StringLength(90)]
    [Unicode(false)]
    public string nom { get; set; } = null!;

    [Unicode(false)]
    public string? des { get; set; }

    [InverseProperty("id_profNavigation")]
    public virtual ICollection<estudio> estudios { get; set; } = new List<estudio>();
}
