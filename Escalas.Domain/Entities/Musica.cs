using Escalas.Domain.Entities.Base;
using Flunt.Validations;

namespace Escalas.Domain.Entities;

public class Musica : Entity<int>
{
    public Musica(string nome, string cantor, string tom)
    {
        Nome = nome;
        Cantor = cantor;
        Tom = tom;

        AddNotifications(new Contract<Musica>()
            .Requires()
            .IsNotNullOrWhiteSpace(Nome, nameof(Nome), $"O campo {nameof(Nome)} deve ser preenchido")
            .IsNotNullOrWhiteSpace(Cantor, nameof(Cantor), $"O campo {nameof(Nome)} deve ser preenchido")
        );
    }

    public string Nome { get; private set; }
    public string Cantor { get; private set; }
    public string Tom { get; private set; }
}