      namespace Escalas.Infrastructure.Scripts
{
    public static class EscalaScripts
    {
        public const string CountEscalas = @"
          SELECT COUNT(1) FROM tb_escala";

        public const string SelectEscala = @"
               SELECT id AS Id,
                      data AS Data,
                      musicas_manha AS MusicasManhaJson,
                      musicas_noite AS MusicasNoiteJson,
                      instrumental AS InstrumentalJson,
                      vocal AS VocalJson
               FROM tb_escala";

        public const string SelectEscalaById = @"
               SELECT id AS Id,
                      data AS Data,
                      musicas_manha AS MusicasManhaJson,
                      musicas_noite AS MusicasNoiteJson,
                      instrumental AS InstrumentalJson,
                      vocal AS VocalJson
                FROM tb_escala
               WHERE id=@Id";

        public const string InsertEscala = @"
               INSERT INTO tb_escala(data, musicas_manha, musicas_noite, instrumental, vocal)
                 VALUES (@Data, @MusicasManha::jsonb, @MusicasNoite::jsonb, @Instrumental::jsonb, @Vocal::jsonb)
               RETURNING id";

        public const string UpdateEscala = @"
               UPDATE tb_escala
                   SET data=@Data,
                       musicas_manha=@MusicasManha::jsonb,
                       musicas_noite=@MusicasNoite::jsonb,
                       instrumental=@Instrumental::jsonb,
                       vocal=@Vocal::jsonb
                   WHERE id=@Id
               RETURNING id";

        public const string DeleteEscala = @"
               DELETE FROM tb_escala
                   WHERE id=@Id";
    }
}
