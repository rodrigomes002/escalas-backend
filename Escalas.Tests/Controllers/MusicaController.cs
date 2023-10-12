using Escalas.API.Controllers;
using Escalas.Application;
using Escalas.Application.Models;
using Escalas.Domain.Interfaces;
using Escalas.Tests.Fixture;
using Escalas.Tests.Mocks;
using Escalas.Tests.Mocks.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace Escalas.Tests.Controllers;

[Collection("Mapper")]
public class MusicaControllerTest
{
    private readonly MapperFixture _mapperFixture;

    public MusicaControllerTest(MapperFixture mapperFixture)
    {
        _mapperFixture = mapperFixture;
    }

    #region CreateTest

    [Theory]
    [MemberData(nameof(Post_Success))]
    public async Task Post_Success_Test(MusicaModel model, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock().CreateMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Post_BadRequest))]
    public async Task Post_BadRequest_Test(MusicaModel model, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock().CreateMusicaAsync()
        };

        yield return new object[]
        {
            MusicaModelMock.FullObject(),
            new MusicaRepositoryMock().CreateMusicaAsync_Fail()
        };
    }


    #endregion

    #region UpdateTest

    [Theory]
    [MemberData(nameof(Put_Success))]
    public async Task Put_Success_Test(MusicaModel model, int id, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Put_NotFound))]
    public async Task Put_NotFound_Test(MusicaModel model, int id, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync_Null()
        };
    }

    [Theory]
    [MemberData(nameof(Put_BadRequest))]
    public async Task Put_BadRequest_Test(MusicaModel model, int id, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock()
            .UpdateMusicaAsync()
            .GetMusicaAsync()
        };
    }

    #endregion

    #region GetTest

    [Theory]
    [MemberData(nameof(Get_Success))]
    public async Task Get_Success_Test(MusicaRepositoryMock repositoryMock)
    {
        var controller = CreateController(repositoryMock);
        var result = await controller.Get();

        Assert.IsType<OkObjectResult>(result);
    }
    public static IEnumerable<object[]> Get_Success()
    {
        yield return new object[]
        {
            new MusicaRepositoryMock()
            .GetMusicasAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Get_Musica_Success))]
    public async Task Get_Musica_Success_Test(int id, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock()
            .GetMusicaAsync()
        };
    }

    [Theory]
    [MemberData(nameof(Get_Musica_NotFound))]
    public async Task Get_Musica_NotFound_Test(int id, MusicaRepositoryMock repositoryMock)
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
            new MusicaRepositoryMock()
            .GetMusicaAsync_Null()
        };
    }

    #endregion

    private MusicaController CreateController(Mock<IMusicaRepository>? mockMusicaRepository = null)
    {
        var musicaRepositoryMock = mockMusicaRepository ?? new Mock<IMusicaRepository>();
        var musicaApplication = new MusicaApplication(musicaRepositoryMock.Object);

        return new MusicaController(_mapperFixture.Mapper, musicaApplication);
    }
}
