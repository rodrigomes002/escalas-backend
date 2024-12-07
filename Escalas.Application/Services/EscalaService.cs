using Escalas.Application.Interfaces;
using Escalas.Application.Models.Result;
using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Newtonsoft.Json;

namespace Escalas.Application.Services
{
    public class EscalaService : IEscalaService
    {
        private readonly IEscalaRepository _escalaRepository;

        public EscalaService(IEscalaRepository escalaRepository)
        {
            _escalaRepository = escalaRepository;
        }

        public async Task<Result<int>> AtualizarEscalaAsync(int id, Escala escala)
        {
            if (id == 0)
                return Result<int>.Error("É necessário especificar um Id");

            var escalaDb = await _escalaRepository.GetEscalaByIdAsync(id);

            if (escalaDb is null)
                return Result<int>.NotFoundResult();

            var repertorio = escala.Repertorio.Select(x => new { x.Nome, x.Cantor, x.Tom });
            var participantes = escala.Participantes.Select(x => new { x.Nome, x.Funcao });

            escala.RepertorioJson = JsonConvert.SerializeObject(repertorio);
            escala.ParticipantesJson = JsonConvert.SerializeObject(participantes);

            var result = await _escalaRepository.AtualizarEscalaAsync(escala);

            if (result <= 0)
                return Result<int>.Error("Erro ao atualizar a escala");

            return Result<int>.Ok(result);
        }

        public async Task<Result<int>> CadastrarEscalaAsync(Escala escala)
        {
            var repertorio = escala.Repertorio.Select(x => new { x.Nome, x.Cantor, x.Tom });
            var participantes = escala.Participantes.Select(x => new { x.Nome, x.Funcao });

            escala.RepertorioJson = JsonConvert.SerializeObject(repertorio);
            escala.ParticipantesJson = JsonConvert.SerializeObject(participantes);

            var result = await _escalaRepository.CadastrarEscalaAsync(escala);

            if (result <= 0)
                return Result<int>.Error("Erro ao cadastrar escala");

            return Result<int>.Ok(result);
        }

        public async Task<Result<int>> DeletarEscalaAsync(int id)
        {
            var result = await _escalaRepository.DeletarEscalaAsync(id);

            if (result <= 0)
                return Result<int>.Error("Erro ao deletar escala");

            return Result<int>.Ok(result);
        }

        public async Task<Result<Escala>> GetEscalaByIdAsync(int id)
        {
            var result = await _escalaRepository.GetEscalaByIdAsync(id);

            if (result is null)
                return Result<Escala>.NotFoundResult();

            result.Repertorio = JsonConvert.DeserializeObject<List<Musica>>(result.RepertorioJson);
            result.Participantes = JsonConvert.DeserializeObject<List<Musico>>(result.ParticipantesJson);

            return Result<Escala>.Ok(result);
        }
        public async Task<Result<IEnumerable<Escala>>> GetEscalaAsync()
        {
            var result = await _escalaRepository.GetEscalasAsync();

            foreach (var item in result)
            {
                item.Repertorio = JsonConvert.DeserializeObject<List<Musica>>(item.RepertorioJson);
                item.Participantes = JsonConvert.DeserializeObject<List<Musico>>(item.ParticipantesJson);
            }

            return Result<IEnumerable<Escala>>.Ok(result);
        }
    }
}
