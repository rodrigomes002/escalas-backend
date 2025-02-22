using Escalas.Domain.Enums;

namespace Escalas.Application.Models
{
    public class CargoModel
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public NivelAutorizacao nivelAutorizacao { get; set; }
    }
}
