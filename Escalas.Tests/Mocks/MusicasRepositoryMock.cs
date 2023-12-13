using Escalas.Domain.Entities;
using Escalas.Domain.Interfaces;
using Escalas.Tests.Mocks.Entities;
using Moq;

namespace Escalas.Tests.Mocks;

public class MusicasRepositoryMock : Mock<IMusicasRepository>
{
    public MusicasRepositoryMock CreateMusicaAsync()
    {
        Setup(repository => repository.CadastrarMusicaAsync(It.IsAny<Musica>()))
            .ReturnsAsync(1)
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock CreateMusicaAsync_Fail()
    {
        Setup(repository => repository.CadastrarMusicaAsync(It.IsAny<Musica>()))
            .ReturnsAsync(0)
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock UpdateMusicaAsync()
    {
        Setup(repository => repository.AtualizarMusicaAsync(It.IsAny<Musica>()))
            .ReturnsAsync(1)
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock UpdateMusicaAsync_Fail()
    {
        Setup(repository => repository.AtualizarMusicaAsync(It.IsAny<Musica>()))
            .ReturnsAsync(0)
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock GetMusicaAsync()
    {
        Setup(repository => repository.GetMusicaByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(MusicaMock.Musica())
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock GetMusicaAsync_Null()
    {
        Setup(repository => repository.GetMusicaByIdAsync(It.IsAny<int>()))
            .ReturnsAsync(MusicaMock.Musica_Null())
            .Verifiable();

        return this;
    }

    public MusicasRepositoryMock GetMusicasAsync()
    {
        Setup(repository => repository.GetMusicasAsync())
            .ReturnsAsync(MusicaMock.Musicas())
            .Verifiable();

        return this;
    }
}
