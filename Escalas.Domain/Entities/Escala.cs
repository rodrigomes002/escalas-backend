using Escalas.Domain.Entities.Base;

namespace Escalas.Domain.Entities;

public class Escala : Entity<int>
{
    public Escala()
    {
    }

    public Escala(DateTime data)
    {
        Data = data;
    }

    public DateTime Data { get; set; }
    public string MusicasManhaJson { get; set; } = string.Empty;
    public string MusicasNoiteJson { get; set; } = string.Empty;
    public string InstrumentalJson { get; set; } = string.Empty;
    public string VocalJson { get; set; } = string.Empty;
    
    public IEnumerable<Musica> MusicasManha { get; set; }
    public IEnumerable<Musica> MusicasNoite { get; set; }
    public IEnumerable<Musico> Instrumental { get; set; }
    public IEnumerable<Musico> Vocal { get; set; }
}