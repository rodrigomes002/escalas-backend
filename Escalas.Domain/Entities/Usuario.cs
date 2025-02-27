using Escalas.Domain.Entities.Base;
using Flunt.Validations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Escalas.Domain.Entities
{
    public class Usuario : Entity<int>
    {
        public Usuario(){}
        public Usuario(string username, string password)
        {      
            Username = username;
            Password = password;

            AddNotifications(new Contract<Usuario>()
                .Requires()
                .IsNotNullOrWhiteSpace(Username, nameof(Username), $"O campo {nameof(Username)} deve ser preenchido")
                .IsNotNullOrWhiteSpace(Password, nameof(Password), $"O campo {nameof(Password)} deve ser preenchido")
            );
        }
        
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string CargoJson { get; set; } = string.Empty;
        public DateTime Created { get; set; } = DateTime.Now;

        public int IdCargo { get; set; }
    }
}