namespace Escalas.Application.Models
{
    public class EscalaModel
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public string Turno { get; set; }
        public List<MusicaModel> Repertorio { get; set; }
        public List<MusicoModel> Participantes { get; set; }
    }
}
