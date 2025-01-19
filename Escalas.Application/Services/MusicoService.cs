using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;

namespace Escalas.Application.Services;

public class MusicoService : IMusicosService
{
    private readonly IMusicoRepository _musicoRepository;
    public MusicoService(IMusicoRepository musicoRepository)
    {
        _musicoRepository = musicoRepository;
    }

    public async Task<Result<int>> AtualizarMusicoAsync(int id, Musico musico)
    {
        if (id == 0)
            return Result<int>.Error("É necessário especificar um Id");

        var musicodb = await _musicoRepository.GetMusicoByIdAsync(id);

        if (musicodb is null)
            return Result<int>.NotFoundResult();

        var result = await _musicoRepository.AtualizarMusicoAsync(musico);

        if (result <= 0)
            return Result<int>.Error("Erro ao atualizar um músico");

        return Result<int>.Ok(result);
    }

    public async Task<Result<int>> CadastrarMusicoAsync(Musico musico)
    {
        var result = await _musicoRepository.CadastrarMusicoAsync(musico);

        if (result <= 0)
            return Result<int>.Error("Erro ao cadastrar um musico");

        return Result<int>.Ok(result);

    }

    public async Task<Result<int>> DeletarMusicoAsync(int id)
    {
        if (id == 0)
            return Result<int>.Error("É necessário especificar um Id");

        var musicadb = await _musicoRepository.GetMusicoByIdAsync(id);

        if (musicadb is null)
            return Result<int>.NotFoundResult();

        var result = await _musicoRepository.DeletarMusicoAsync(id);

        if (result <= 0)
            return Result<int>.Error("Erro ao deletar uma musico");

        return Result<int>.Ok(result);
    }

    public async Task<Result<Musico>> GetMusicoByIdAsync(int id)
    {
        var result = await _musicoRepository.GetMusicoByIdAsync(id);

        if (result is null)
            return Result<Musico>.NotFoundResult();

        return Result<Musico>.Ok(result);
    }

    public async Task<Result<PaginatedBase<Musico>>> GetMusicosAsync(int pageNumber, int pageSize, string? nome)
    {
        var musicos = await _musicoRepository.GetMusicosAsync(pageNumber, pageSize, nome);

        return Result<PaginatedBase<Musico>>.Ok(musicos);
    }
}
