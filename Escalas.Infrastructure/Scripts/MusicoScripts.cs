namespace Escalas.Infrastructure.Scripts;

public static class MusicoScripts
{
    public const string SelectMusicos = @"
         select id as Id, 
                nome as Nome, 
                instrumento as Instrumento
            from escalas.tb_musico";

    public const string SelectMusicoById = @"
           SELECT id AS Id,
                  nome AS Nome,
             FROM escalas.tb_musico
            WHERE id=@id";

    public const string InsertMusico = @"
            INSERT INTO escalas.tb_musico(nome, instrumento)
              VALUES (@nome, @instrumento)
            RETURNING id";

    public const string UpdateMusico = @"
            UPDATE escalas.tb_musico SET nome=@nome
                where id=@id";

    public const string DeleteMusico = @"
            DELETE FROM escalas.tb_musico
                where id=@id";
}
