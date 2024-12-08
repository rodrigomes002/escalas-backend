namespace Escalas.Application.Models
{
    public class EscalaModel
    {
        public DateTime Data { get; set; }
        public IEnumerable<MusicoModel> Instrumental { get; set; }
        public IEnumerable<MusicoModel> Vocal { get; set; }
        public IEnumerable<MusicaModel> MusicasManha { get; set; }
        public IEnumerable<MusicaModel> MusicasNoite { get; set; }
    }
}
