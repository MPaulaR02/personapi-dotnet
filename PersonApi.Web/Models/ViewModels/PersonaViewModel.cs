namespace personapi_dotnet.Models.ViewModels;

public class PersonaViewModel
{
    public int Cc { get; set; }
    public string Nombre { get; set; } = null!;
    public string Apellido { get; set; } = null!;
    public string Genero { get; set; } = null!;
    public int? Edad { get; set; }

    // Listas simples sin navegación circular
    public List<TelefonoViewModel>? Telefonos { get; set; }
    public List<EstudioViewModel>? Estudios { get; set; }
}