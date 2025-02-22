using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Entities.Base;
using Escalas.Domain.Interfaces;

namespace Escalas.Application.Services
{
    public class CargoService : ICargoService
    {
        private readonly ICargoRepository _cargoRepository;

        public CargoService(ICargoRepository cargoRepository)
        {
            _cargoRepository = cargoRepository;
        }

        public async Task<Result<int>> CadastrarCargoAsync(Cargo cargo)
        {
            var result = await _cargoRepository.CadastrarCargoAsync(cargo);

            if (result <= 0)
                return Result<int>.Error("Erro ao cadastrar cargo");

            return Result<int>.Ok(result);
        }

        public async Task<Result<int>> DeletarCargoAsync(int id)
        {
            if (id == 0)
                return Result<int>.Error("É necessário especificar um Id");

            var cargoDb = await _cargoRepository.GetCargosByIdAsync(id);

            if (cargoDb is null)
                return Result<int>.NotFoundResult();

            var result = await _cargoRepository.DeletarCargoAsync(id);

            if (result <= 0)
                return Result<int>.Error("Erro ao deletar um cargo");

            return Result<int>.Ok(result);
        }

        public async Task<Result<PaginatedBase<Cargo>>> GetCargosAsync(int pageNumber, int pageSize, string? nome)
        {
            var cargos = await _cargoRepository.GetCargosAsync(pageNumber, pageSize, nome);

            return Result<PaginatedBase<Cargo>>.Ok(cargos);
        }

        public async Task<Result<Cargo>> GetCargosByIdAsync(int id)
        {
            var result = await _cargoRepository.GetCargosByIdAsync(id);
            
            if (result is null)
                return Result<Cargo>.NotFoundResult();

            return Result<Cargo>.Ok(result);
        }

        public async Task<Result<int>> AtualizarCargoAsync(int id, Cargo cargo)
        {
            if (id == 0)
                return Result<int>.Error("É necessário especificar um Id");

            var cargoDb = await _cargoRepository.GetCargosByIdAsync(id);

            if (cargoDb is null)
                return Result<int>.NotFoundResult();

            var result = await _cargoRepository.AtualizarCargoAsync(cargo);

            if (result <= 0)
                return Result<int>.Error("Erro ao atualizar um cargo");

            return Result<int>.Ok(result);
        }
    }
}
