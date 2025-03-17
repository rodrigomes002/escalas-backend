namespace Escalas.Infrastructure.Scripts;

public static class MusicoScripts
{
    public const string SelectMusicos = @"
            SELECT COUNT(1) 
              FROM tb_musico
             WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome);

            SELECT id AS Id, 
                   nome AS Nome, 
                   funcao AS Funcao
              FROM tb_musico
             WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome)
          ORDER BY nome
             LIMIT @pageSize OFFSET @pageNumber;
             ";

    public const string SelectMusicoById = @"
           SELECT id AS Id,
                  nome AS Nome,
                  funcao AS Funcao
             FROM tb_musico
            WHERE id=@id
            ";

    public const string InsertMusico = @"
            INSERT INTO tb_musico(nome, funcao)
                 VALUES (@nome, @funcao)
              RETURNING id
            ";

    public const string UpdateMusico = @"
           UPDATE tb_musico 
              SET nome=@nome,
                  funcao=@funcao
            WHERE id=@id
        RETURNING id";

    public const string DeleteMusico = @"
        DELETE 
          FROM tb_musico 
         WHERE id=@id";
}
