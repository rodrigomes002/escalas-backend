namespace Escalas.Infrastructure.Scripts;

public static class MusicoScripts
{
    public const string CountMusicos = @"
          SELECT COUNT(1) FROM tb_musico";

    public const string SelectMusicos = @"
          SELECT id as Id, 
                 nome AS Nome, 
                 funcao AS Funcao
            FROM tb_musico
           ";

    public const string SelectMusicoById = @"
           SELECT id AS Id,
                  nome AS Nome,
                  funcao AS Funcao
             FROM tb_musico
            WHERE id=@id";

    public const string InsertMusico = @"
            INSERT INTO tb_musico(nome, funcao)
              VALUES (@nome, @funcao)
            RETURNING id";

    public const string UpdateMusico = @"
           UPDATE tb_musico 
                SET nome=@nome,
                    funcao=@funcao
                WHERE id=@id
            RETURNING id";

    public const string DeleteMusico = @"delete from tb_musico where id=@id";
}
