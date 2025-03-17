using Escalas.Domain.Entities.Base;
using Escalas.Domain.Enums;
using Flunt.Validations;
using System;

namespace Escalas.Domain.Entities
{
    public class Cargo : Entity<int>
    {

        public Cargo() { }

        public Cargo(string nome, NivelAutorizacao nivelAutorizacao)
        {
            Nome = nome;
            NivelAutorizacao = nivelAutorizacao;

            AddNotifications(new Contract<Cargo>()
                .Requires()
                .IsNotNullOrWhiteSpace(Nome, nameof(Nome), $"O campo {nameof(Nome)} deve ser preenchido")
                .IsBetween((int)NivelAutorizacao, (int)NivelAutorizacao.Alto, (int)NivelAutorizacao.Baixo, nameof(NivelAutorizacao), $"O campo {nameof(NivelAutorizacao)} deve ser preenchido")
            );
        }

        public string Nome { get; set; }
        public NivelAutorizacao NivelAutorizacao { get; set; }
    }
}
