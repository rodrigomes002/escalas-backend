namespace Escalas.Application.Models
{
    public class UsuarioTokenModel
    {
         public bool Authenticated { get; set; }
        public string Token { get; set; } = string.Empty;
        public DateTime Expiration { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}