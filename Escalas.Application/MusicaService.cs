using Escalas.Application.Interfaces;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Serilog;

namespace Escalas.Application
{
    public class MusicaService : IMusicaService
    {
        private readonly IMusicaRepository _musicaRepository;
        public MusicaService(IMusicaRepository musicaRepository)
        {
            _musicaRepository = musicaRepository;
        }

        public async Task CadastrarMusicaAsync(MusicaRequest request)
        {
            Log.Information("Cadastrando musica");

            var musica = new Musica();
            musica.Nome = request.Nome;
            musica.Cantor = request.Cantor;
            musica.Tom = request.Tom;

            await _musicaRepository.CadastrarMusicaAsync(musica);
        }

        public async Task<IEnumerable<Musica>> GetMusicasAsync()
        {
            Log.Information("Buscando musicas");
            return await _musicaRepository.GetMusicasAsync();
        }
    }
}
