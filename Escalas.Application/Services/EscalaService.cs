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

            Serialize(escala);

            var result = await _escalaRepository.AtualizarEscalaAsync(escala);

            if (result <= 0)
                return Result<int>.Error("Erro ao atualizar a escala");

            return Result<int>.Ok(result);
        }

        public async Task<Result<int>> CadastrarEscalaAsync(Escala escala)
        {
            Serialize(escala);
            
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

            Deserialize(result);

            return Result<Escala>.Ok(result);
        }

        public async Task<Result<IEnumerable<Escala>>> GetEscalaAsync()
        {
            var result = await _escalaRepository.GetEscalasAsync();

            foreach (var item in result)
            {
                Deserialize(item);
            }

            return Result<IEnumerable<Escala>>.Ok(result);
        }

        private void Deserialize(Escala escala)
        {
            escala.MusicasManha = JsonConvert.DeserializeObject<List<Musica>>(escala.MusicasManhaJson) ?? Enumerable.Empty<Musica>();
            escala.MusicasNoite = JsonConvert.DeserializeObject<List<Musica>>(escala.MusicasNoiteJson) ?? Enumerable.Empty<Musica>();
            escala.Instrumental = JsonConvert.DeserializeObject<List<Musico>>(escala.InstrumentalJson) ?? Enumerable.Empty<Musico>();
            escala.Vocal = JsonConvert.DeserializeObject<List<Musico>>(escala.VocalJson) ?? Enumerable.Empty<Musico>();
        }
        
        private void Serialize(Escala escala)
        {
            var musicasManha = escala.MusicasManha.Select(x => new { x.Nome, x.Cantor, x.Tom });
            var musicasnoite = escala.MusicasNoite.Select(x => new { x.Nome, x.Cantor, x.Tom });
            var instrumental = escala.Instrumental.Select(x => new { x.Nome, x.Funcao });
            var vocal = escala.Vocal.Select(x => new { x.Nome, x.Funcao });

            escala.MusicasManhaJson = JsonConvert.SerializeObject(musicasManha);
            escala.MusicasNoiteJson = JsonConvert.SerializeObject(musicasnoite);
            escala.InstrumentalJson = JsonConvert.SerializeObject(instrumental);
            escala.VocalJson = JsonConvert.SerializeObject(vocal);
        }
    }
}
