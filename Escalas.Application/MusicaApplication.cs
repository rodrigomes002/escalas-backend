using Escalas.Application.Interfaces;
using Escalas.Application.Models;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;

namespace Escalas.Application;

public class MusicaApplication : IMusicaApplication
{
    private readonly IMusicaRepository _musicaRepository;
    public MusicaApplication(IMusicaRepository musicaRepository)
    {
        _musicaRepository = musicaRepository;
    }

    public async Task<Result<int>> CadastrarMusicaAsync(Musica musica)
    {
        var result = await _musicaRepository.CadastrarMusicaAsync(musica);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar uma música");

        return Result<int>.Ok(result);
    }

    public async Task<Result<IEnumerable<Musica>>> GetMusicasAsync()
    {
        var musicas = await _musicaRepository.GetMusicasAsync();

        return Result<IEnumerable<Musica>>.Ok(musicas);
    }
}
