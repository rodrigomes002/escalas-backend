using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Serilog;

namespace Escalas.Application;

public class MusicoApplication : IMusicoApplication
{
    private readonly IMusicoRepository _musicaRepository;
    public MusicoApplication(IMusicoRepository musicaRepository)
    {
        _musicaRepository = musicaRepository;
    }

    public async Task CadastrarMusicoAsync(MusicoModel model)
    {
        Log.Information("Cadastrando musica");

        var musica = new Musico(model.Nome, model.Instrumento);

        await _musicaRepository.CadastrarMusicoAsync(musica);
    }

    public async Task<IEnumerable<Musico>> GetMusicosAsync()
    {
        Log.Information("Buscando musicas");
        return await _musicaRepository.GetMusicosAsync();
    }
}
