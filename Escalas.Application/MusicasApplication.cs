using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;

namespace Escalas.Application;

public class MusicasApplication : IMusicasApplication
{
    private readonly IMusicaRepository _musicaRepository;
    public MusicasApplication(IMusicaRepository musicaRepository)
    {
        _musicaRepository = musicaRepository;
    }

    public async Task<Result<int>> AtualizarMusicaAsync(int id, Musica musica)
    {
        if (id == 0)
            return Result<int>.Error("É necessário especificar um Id");

        var musicadb = await _musicaRepository.GetMusicaByIdAsync(id);

        if (musicadb is null)
            return Result<int>.NotFoundResult();

        var result = await _musicaRepository.AtualizarMusicaAsync(musica);

        if (result <= 0)
            return Result<int>.Error("Erro ao atualizar uma música");

        return Result<int>.Ok(result);
    }

    public async Task<Result<int>> CadastrarMusicaAsync(Musica musica)
    {
        var result = await _musicaRepository.CadastrarMusicaAsync(musica);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar uma música");

        return Result<int>.Ok(result);
    }

    public async Task<Result<Musica>> GetMusicaByIdAsync(int id)
    {
        var result = await _musicaRepository.GetMusicaByIdAsync(id);

        if (result is null)
            return Result<Musica>.NotFoundResult();

        return Result<Musica>.Ok(result);
    }

    public async Task<Result<IEnumerable<Musica>>> GetMusicasAsync()
    {
        var musicas = await _musicaRepository.GetMusicasAsync();

        return Result<IEnumerable<Musica>>.Ok(musicas);
    }
}
