using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;

namespace Escalas.Application.Services;

public class MusicaService : IMusicasService
{
    private readonly IMusicaRepository _musicaRepository;
    public MusicaService(IMusicaRepository musicaRepository)
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

    public async Task<Result<int>> DeletarMusicaAsync(int id)
    {
        if (id == 0)
            return Result<int>.Error("É necessário especificar um Id");

        var musicadb = await _musicaRepository.GetMusicaByIdAsync(id);

        if (musicadb is null)
            return Result<int>.NotFoundResult();

        var result = await _musicaRepository.DeletarMusicaAsync(id);

        if (result <= 0)
            return Result<int>.Error("Erro ao deletar uma música");

        return Result<int>.Ok(result);
    }

    public async Task<Result<Musica>> GetMusicaByIdAsync(int id)
    {
        var result = await _musicaRepository.GetMusicaByIdAsync(id);

        if (result is null)
            return Result<Musica>.NotFoundResult();

        return Result<Musica>.Ok(result);
    }

    public async Task<Result<PaginatedBase<Musica>>> GetMusicasAsync(int pageNumber, int pageSize, string? nome)
    {
        var musicas = await _musicaRepository.GetMusicasAsync(pageNumber, pageSize, nome);

        return Result<PaginatedBase<Musica>>.Ok(musicas);
    }
}
