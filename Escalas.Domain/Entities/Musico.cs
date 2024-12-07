using Escalas.Domain.Entities.Base;
using Escalas.Domain.Enums;
using Flunt.Validations;

namespace Escalas.Domain.Entities;

public class Musico : Entity<int>
{
    public Musico(){}
    public Musico(string nome, FuncaoEnum funcao)
    {
        Nome = nome;
        Funcao = funcao;

        AddNotifications(new Contract<Musico>()
            .Requires()
            .IsNotNullOrWhiteSpace(Nome, nameof(Nome), $"O campo {nameof(Nome)} deve ser preenchido")
            .IsBetween((int)Funcao, (int)FuncaoEnum.Vocal, (int)FuncaoEnum.Bateria, nameof(Funcao), $"O campo {nameof(Funcao)} deve ser preenchido")
        );
    }

    public string Nome { get; set; }
    public FuncaoEnum Funcao { get; set; }
}
