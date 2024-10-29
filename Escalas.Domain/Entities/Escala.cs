using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Entities;

public class Escala : Entity<int>
{
    public Escala() { }

    public Escala(DateTime data, string turno)
    {
        Data = data;
        Turno = turno;       
    }

    public DateTime Data { get; set; }
    public string Turno { get; set; }
    public List<Musica> Repertorio { get; set; }
    public List<Musico> Participantes { get; set; }
    public string RepertorioJson { get; set; }
    public string ParticipantesJson { get; set; }
}
