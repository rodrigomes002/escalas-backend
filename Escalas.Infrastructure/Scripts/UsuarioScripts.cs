namespace Escalas.Infrastructure.Scripts
{
    public static class UsuarioScripts
    {
         public const string SelectUsuario = @"
         SELECT id AS Id,
                  username AS Username,
                  password_hash AS PasswordHash,
                  password_salt AS PasswordSalt,
                  created AS Created
            FROM services.tb_usuario
          WHERE username = @username";

    public const string InsertUsuario = @"
    INSERT INTO services.tb_usuario(username, password_hash, password_salt, created)
	VALUES (@username, @password_hash, @password_salt, @created)           
     RETURNING id";  
    }
}