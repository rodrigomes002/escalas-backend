namespace Escalas.Infrastructure.Scripts
{
    public static class EscalaScripts
    {
        public const string SelectEscala = @"
               SELECT id AS Id,
                      data AS Data,
                      turno AS Turno,
                      repertorio AS RepertorioJson,
                      participantes AS ParticipantesJson
               FROM escalas.tb_escala";

        public const string SelectEscalaById = @"
               SELECT id AS Id,
                      data AS Data,
                      turno AS Turno,
                      repertorio AS RepertorioJson,
                      participantes AS ParticipantesJson
                FROM escalas.tb_escala
               WHERE id=@Id";

        public const string InsertEscala = @"
               INSERT INTO escalas.tb_escala(data, turno, repertorio, participantes)
                 VALUES (@Data, @Turno, @Repertorio::jsonb, @Participantes::jsonb)
               RETURNING id";

        public const string UpdateEscala = @"
               UPDATE escalas.tb_escala
                   SET data=@Data,
                       turno=@Turno,
                       repertorio=@Repertorio::jsonb,
                       participantes=@Participantes::jsonb
                   WHERE id=@Id
               RETURNING id";

        public const string DeleteEscala = @"
               DELETE FROM escalas.tb_escala
                   WHERE id=@Id";
    }
}
