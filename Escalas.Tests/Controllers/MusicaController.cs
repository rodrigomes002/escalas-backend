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
    #endregion

    #region GetTest
    #endregion

    private MusicaController CreateController(Mock<IMusicaRepository>? mockMusicaRepository = null)
    {
        var musicaRepositoryMock = mockMusicaRepository ?? new Mock<IMusicaRepository>();
        var musicaApplication = new MusicaApplication(musicaRepositoryMock.Object);

        return new MusicaController(_mapperFixture.Mapper, musicaApplication);
    }
}
