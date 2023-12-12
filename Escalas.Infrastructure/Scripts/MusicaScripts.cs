namespace Escalas.Infrastructure.Scripts;

public static class MusicaScripts
{
    public const string SelectMusicas = @"
           SELECT id AS Id,
                  nome AS Nome,
                  cantor AS Cantor,
                  tom AS Tom
             FROM escalas.tb_musica";

    public const string SelectMusicaById = @"
           SELECT id AS Id,
                  nome AS Nome,
                  cantor AS Cantor,
                  tom AS Tom
             FROM escalas.tb_musica
            WHERE id=@id";

    public const string InsertMusica = @"
            INSERT INTO escalas.tb_musica(nome, cantor, tom)
              VALUES (@nome, @cantor, @tom)
            RETURNING id";

    public const string UpdateMusica = @"
            UPDATE escalas.tb_musica 
                SET nome=@nome,
                    cantor=@cantor,
                    tom=@tom
                where id=@id
            RETURNING id";

    public const string DeleteMusica = @"
            DELETE FROM escalas.tb_musica
                where id=@id";

}