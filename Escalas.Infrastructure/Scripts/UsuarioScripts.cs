namespace Escalas.Infrastructure.Scripts
{
    public static class UsuarioScripts
    {
        public const string SelectUsuario = @"
               SELECT id AS Id,
                      username AS Username,
                      password_hash AS PasswordHash,
                      password_salt AS PasswordSalt,
                      id_cargo AS IdCargo,
                      created AS Created
                 FROM tb_usuario
                WHERE username = @username";

        public const string SelectUsuarioById = @"
               SELECT id AS Id,
                      username AS Username,
                      password_hash AS PasswordHash,
                      password_salt AS PasswordSalt,
                      id_cargo AS IdCargo,
                      created AS Created
                 FROM tb_usuario
                WHERE id = @id";

        public const string UpdateCargoUsuario = @"
               UPDATE tb_usuario 
                  SET id_cargo=@id_cargo
                WHERE id=@id
            RETURNING id";

        public const string InsertUsuario = @"
               INSERT INTO tb_usuario(username, password_hash, password_salt, id_cargo, created)
	                VALUES (@username, @password_hash, @password_salt, @id_cargo, @created)
                 RETURNING id";
    }
}