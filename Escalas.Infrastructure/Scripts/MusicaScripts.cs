namespace Escalas.Infra.Data.Scripts;

public static class MusicaScripts
{
    public const string SelectMusicas = @"
           SELECT id AS Id,
                  nome AS Nome,
                  cantor AS Cantor,
                  tom AS Tom
            FROM escalas.tb_musica";

    public const string InsertMusica = @"
            INSERT INTO escalas.tb_musica(nome, cantor, tom)
              VALUES (@nome, @cantor, @tom)
            RETURNING id";

}