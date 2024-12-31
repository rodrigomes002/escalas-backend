﻿using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;

namespace Escalas.Application.Interfaces
{
    public interface IEscalaService
    {
        Task<Result<PaginatedBase<Escala>>> GetEscalaAsync(int pageNumber, int pageSize, string? data);
        Task<Result<Escala>> GetEscalaByIdAsync(int id);
        Task<Result<int>> CadastrarEscalaAsync(Escala escala);
        Task<Result<int>> AtualizarEscalaAsync(int id, Escala escala);
        Task<Result<int>> DeletarEscalaAsync(int id);
    }
}
