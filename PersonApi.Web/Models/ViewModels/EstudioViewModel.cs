namespace personapi_dotnet.Models.ViewModels;

public class EstudioViewModel
{
    public int IdProf { get; set; }
    public int CcPer { get; set; }
    public DateOnly? Fecha { get; set; }
    public string? Univer { get; set; }

    // Datos básicos sin navegación completa
    public string? ProfesionNombre { get; set; }
    public string? PersonaNombre { get; set; }
}