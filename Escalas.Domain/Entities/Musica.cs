﻿using Escalas.Domain.Entities.Base;
using Flunt.Validations;

namespace Escalas.Domain.Entities;

public class Musica : Entity<int>
{
    public Musica() { }

    public Musica(string nome, string cantor, string tom)
    {      
        Nome = nome;
        Cantor = cantor;
        Tom = tom;

        AddNotifications(new Contract<Musica>()
            .Requires()
            .IsNotNullOrWhiteSpace(Nome, nameof(Nome), $"O campo {nameof(Nome)} deve ser preenchido")
            .IsNotNullOrWhiteSpace(Cantor, nameof(Cantor), $"O campo {nameof(Cantor)} deve ser preenchido")
        );
    }

    public string Nome { get; set; }
    public string Cantor { get; set; }
    public string Tom { get; set; }
}