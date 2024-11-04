using Escalas.API.Controllers;
using Escalas.Application.Models;
using Escalas.Application.Services;
using Escalas.Domain.Interfaces;
using Escalas.Tests.Fixture;
using Escalas.Tests.Mocks;
using Escalas.Tests.Mocks.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Escalas.Tests.Controllers;

[Collection("Mapper")]
public class MusicasControllerTest
{
    private readonly MapperFixture _mapperFixture;

    public MusicasControllerTest(MapperFixture mapperFixture)
    {
        _mapperFixture = mapperFixture;
    }

    #region CreateTest

    [Theory]
    [MemberData(nameof(Post_Success))]
    public async Task Post_Success_Test(MusicaModel model, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Post(model);

        Assert.IsType<OkObjectResult>(result);
    }

    public static IEnumerable<object[]> Post_Success()
    {
        yield return new object[]
        {
            MusicaModelMock.FullObject(),
            new MusicasRepositoryMock().CreateMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Post_BadRequest))]
    public async Task Post_BadRequest_Test(MusicaModel model, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Post(model);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    public static IEnumerable<object[]> Post_BadRequest()
    {
        yield return new object[]
        {
            MusicaModelMock.EmptyName(),
            new MusicasRepositoryMock().CreateMusicaAsync()
        };

        yield return new object[]
        {
            MusicaModelMock.FullObject(),
            new MusicasRepositoryMock().CreateMusicaAsync_Fail()
        };
    }


    #endregion

    #region UpdateTest

    [Theory]
    [MemberData(nameof(Put_Success))]
    public async Task Put_Success_Test(MusicaModel model, int id, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Put(model, id);

        Assert.IsType<OkObjectResult>(result);
    }

    public static IEnumerable<object[]> Put_Success()
    {
        yield return new object[]
        {
            MusicaModelMock.FullObject(),
            1,
            new MusicasRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Put_NotFound))]
    public async Task Put_NotFound_Test(MusicaModel model, int id, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Put(model, id);

        Assert.IsType<NotFoundResult>(result);
    }

    public static IEnumerable<object[]> Put_NotFound()
    {
        yield return new object[]
        {
            MusicaModelMock.FullObject(),
            1,
            new MusicasRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync_Null()
        };
    }

    [Theory]
    [MemberData(nameof(Put_BadRequest))]
    public async Task Put_BadRequest_Test(MusicaModel model, int id, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Put(model, id);

        Assert.IsType<BadRequestObjectResult>(result);
    }

    public static IEnumerable<object[]> Put_BadRequest()
    {
        yield return new object[]
        {
            MusicaModelMock.EmptyName(),
            1,
            new MusicasRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync()
        };
    }

    #endregion

    #region GetTest

    [Theory]
    [MemberData(nameof(Get_Success))]
    public async Task Get_Success_Test(MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Get();

        Assert.IsType<OkObjectResult>(result);
    }
    public static IEnumerable<object[]> Get_Success()
    {
        yield return new object[]
        {
            new MusicasRepositoryMock()
            .GetMusicasAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Get_Musica_Success))]
    public async Task Get_Musica_Success_Test(int id, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Get(id);

        Assert.IsType<OkObjectResult>(result);
    }
    public static IEnumerable<object[]> Get_Musica_Success()
    {
        yield return new object[]
        {
            1,
            new MusicasRepositoryMock()
            .GetMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Get_Musica_NotFound))]
    public async Task Get_Musica_NotFound_Test(int id, MusicasRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Get(id);

        Assert.IsType<NotFoundResult>(result);
    }
    public static IEnumerable<object[]> Get_Musica_NotFound()
    {
        yield return new object[]
        {
            1,
            new MusicasRepositoryMock()
            .GetMusicaAsync_Null()
        };
    }

    #endregion

    private MusicasController CreateController(
        Mock<IMusicaRepository>? mockMusicaRepository = null,
        Mock<IMusicoRepository>? mockMusicoRepository = null)
    {
        var musicaRepositoryMock = mockMusicaRepository ?? new Mock<IMusicaRepository>();
        var musicoRepositoryMock = mockMusicoRepository ?? new Mock<IMusicoRepository>();
        var musicoApplication = new MusicoService(musicoRepositoryMock.Object);
        var musicaApplication = new MusicaService(musicaRepositoryMock.Object);
        
        return new MusicasController(
            _mapperFixture.Mapper, 
            musicaApplication, 
            musicoApplication);
    }
}
