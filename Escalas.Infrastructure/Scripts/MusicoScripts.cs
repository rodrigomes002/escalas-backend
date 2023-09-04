namespace Escalas.Infrastructure.Scripts;

public static class MusicoScripts
{
    public const string SelectMusicos = @"
         SELECT id AS Id,
                  nome AS Nome,
                  instrumento AS Instrumento,                  
            FROM escalas.tb_musico";

    public const string InsertMusico = @"
            INSERT INTO escalas.tb_musico(nome, id_instrumento)
              VALUES (@nome, @instrumento)
            RETURNING id";
}
