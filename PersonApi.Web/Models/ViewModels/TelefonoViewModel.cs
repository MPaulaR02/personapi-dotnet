namespace personapi_dotnet.Models.ViewModels;

public class TelefonoViewModel
{
    public string Num { get; set; } = null!;
    public string Oper { get; set; } = null!;
    public int Duenio { get; set; }

    // Opcional: datos básicos de la persona sin navegación completa
    public string? PersonaNombre { get; set; }
}