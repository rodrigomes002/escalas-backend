namespace Escalas.Infrastructure.Scripts
{
    public static class UsuarioScripts
    {
        public const string SelectUsuario = @"
               SELECT u.id AS Id,
                      u.username AS Username,
                      u.password_hash AS PasswordHash,
                      u.password_salt AS PasswordSalt,
                      u.id_cargo AS IdCargo,
                      u.created AS Created,
                      c.nome AS Cargo
                 FROM tb_usuario AS u JOIN tb_cargo AS c
                   ON c.id = u.id_cargo
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
               INSERT INTO tb_usuario(username, password_hash, password_salt, created)
	                VALUES (@username, @password_hash, @password_salt, @created)
                 RETURNING id";
    }
}