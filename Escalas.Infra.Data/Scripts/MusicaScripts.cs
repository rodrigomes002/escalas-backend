namespace Escalas.Infra.Data.Scripts
{
    public static class MusicaScripts
    {
        public const string SelectMusicas = @"select id as Id, nome as Nome, cantor as Cantor, tom as Tom from musica.musicas";
        public const string InsertMusica = @"insert into musica.musicas(nome, cantor, tom) VALUES (@nome, @cantor, @tom)";

    }
}
