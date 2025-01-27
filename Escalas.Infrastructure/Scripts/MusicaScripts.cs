﻿namespace Escalas.Infrastructure.Scripts;

public static class MusicaScripts
{
    public const string SelectMusicas = @"
           SELECT COUNT(1) 
             FROM tb_musica
            WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome);
        
           SELECT id AS Id,
                  nome AS Nome,
                  cantor AS Cantor,
                  tom AS Tom
             FROM tb_musica
            WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome)
         ORDER BY nome
            LIMIT @pageSize OFFSET @pageNumber;
            ";

    public const string SelectMusicaById = @"
           SELECT id AS Id,
                  nome AS Nome,
                  cantor AS Cantor,
                  tom AS Tom
             FROM tb_musica
            WHERE id=@id";

    public const string InsertMusica = @"
            INSERT INTO tb_musica(nome, cantor, tom)
                 VALUES (@nome, @cantor, @tom)
              RETURNING id";

    public const string UpdateMusica = @"
            UPDATE tb_musica 
               SET nome=@nome,
                   cantor=@cantor,
                   tom=@tom
             WHERE id=@id
         RETURNING id";

    public const string DeleteMusica = @"
            DELETE 
              FROM tb_musica
             WHERE id=@id";
}