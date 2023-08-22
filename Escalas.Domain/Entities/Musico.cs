using Escalas.Domain.Entities.Base;
using Escalas.Domain.Enums;
using Flunt.Validations;

namespace Escalas.Domain.Entities;

public class Musico : Entity<int>
{

    public Musico(string nome, InstrumentoEnum instrumento)
    {
        Nome = nome;
        Instrumento = instrumento;

        AddNotifications(new Contract<Musico>()
            .Requires()
            .IsNotNullOrWhiteSpace(Nome, nameof(Nome), $"O campo {nameof(Nome)} deve ser preenchido")
            .IsBetween((int)Instrumento, (int)InstrumentoEnum.Vocal, (int)InstrumentoEnum.Bateria, nameof(Instrumento), $"O campo {nameof(Instrumento)} deve ser preenchido")
        );
    }

    public string Nome { get; set; }
    public InstrumentoEnum Instrumento { get; set; }
}
