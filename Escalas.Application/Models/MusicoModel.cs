using Escalas.Domain.Enums;

namespace Escalas.Application.Models;

public class MusicoModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public InstrumentoEnum Instrumento { get; set; }
}
