using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[Table("telefono")]
[Index("duenio", Name = "IX_telefono_duenio")]
public partial class telefono
{
    [Key]
    [StringLength(15)]
    [Unicode(false)]
    public string num { get; set; } = null!;

    [StringLength(45)]
    [Unicode(false)]
    public string oper { get; set; } = null!;

    public long duenio { get; set; }

    [ForeignKey("duenio")]
    [InverseProperty("telefonos")]
    public virtual persona duenioNavigation { get; set; } = null!;
}
