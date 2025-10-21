using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace personapi_dotnet.Models.Entities;

[PrimaryKey("id_prof", "cc_per")]
[Index("cc_per", Name = "IX_estudios_cc_per")]
[Index("id_prof", Name = "IX_estudios_id_prof")]
public partial class estudio
{
    [Key]
    public int id_prof { get; set; }

    [Key]
    public long cc_per { get; set; }

    public DateOnly? fecha { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string? univer { get; set; }

    [ForeignKey("cc_per")]
    [InverseProperty("estudios")]
    public virtual persona cc_perNavigation { get; set; } = null!;

    [ForeignKey("id_prof")]
    [InverseProperty("estudios")]
    public virtual profesion id_profNavigation { get; set; } = null!;
}
