namespace Escalas.Infrastructure.Scripts;

public static class CargoScripts
{
    public const string SelectCargos = @"
            SELECT COUNT(1)
              FROM tb_cargo
             WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome);

            SELECT id AS Id,
                   nome AS Nome,
                   nivel_autorizacao AS NivelAutorizacao
              FROM tb_cargo
             WHERE (@nome IS NULL OR LOWER(nome) LIKE @nome)
          ORDER BY nome
             LIMIT @pageSize OFFSET @pageNumber;
             ";

    public const string SelectCargoById = @"
           SELECT id AS Id,
                  nome AS Nome,
                  nivel_Autorizacao AS NivelAutorizacao
             FROM tb_cargo
            WHERE id=@id";

    public const string SelectDefaultCargo = @"
           SELECT id AS Id,
                  nome AS Nome,
                  nivel_Autorizacao AS NivelAutorizacao
             FROM tb_cargo
            WHERE nivel_Autorizacao=3";

    public const string InsertCargo = @"
            INSERT INTO tb_cargo(nome, nivel_autorizacao)
                 VALUES (@nome, @nivel_autorizacao)
              RETURNING id";

    public const string UpdateCargo = @"
            UPDATE tb_cargo
               SET nome=@nome,
                   nivel_autorizacao=@nivel_autorizacao
             WHERE id=@id
         RETURNING id";

    public const string DeleteCargo = @"
            DELETE
              FROM tb_cargo
             WHERE id=@id";
}

