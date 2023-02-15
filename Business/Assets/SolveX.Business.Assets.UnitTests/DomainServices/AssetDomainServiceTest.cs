using AutoBogus;
using Bogus;
using DocumentFormat.OpenXml.Spreadsheet;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using SolveX.Business.Assets.Domain.DomainServices;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Framework.Domain.Exceptions;

namespace SolveX.Business.Assets.UnitTests.DomainServices;

[TestFixture]
public class SightingsServiceTest
{
    private AssetDomainService _service;
    private Mock<IAssetConnectionRepository> _assetsConnectionsRepository;
    private Mock<IAssetRepository> _assetRepository;


    [OneTimeSetUp]
    public void Setup()
    {
        _assetsConnectionsRepository = new();
        _assetRepository = new();

        _service = new AssetDomainService(_assetRepository.Object,
                                             _assetsConnectionsRepository.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _assetsConnectionsRepository.Invocations.Clear();
        _assetRepository.Invocations.Clear();
    }

    [Test]
    public async Task ShouldGetAssetById()
    {
        Asset asset = new AutoFaker<Asset>().Generate();

        _assetRepository.Setup(cr => cr.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(asset);

        int assetId = new Faker().Random.Int();
        Asset retrivedAsset = await _service.Get(assetId);

        _assetRepository.Verify(fr => fr.GetAsync(It.Is<int>(id => assetId == id)), Times.Once);
        retrivedAsset.Should().BeEquivalentTo(asset);
    }

    [Test]
    public async Task ShouldUnsuccessfullyInsertAsset_AssetAlreadyExistsWithSameId()
    {
        Asset asset = new AutoFaker<Asset>().Generate();

        _assetRepository.Setup(cr => cr.GetAsync(It.IsAny<int>()))
            .ReturnsAsync(asset);

        await _service.Invoking(service => service.Create(asset, Enumerable.Empty<int>(), new Dictionary<string, string>()))
          .Should().ThrowAsync<BadDataException>()
          .WithMessage($"The asset with id [{asset.Id}] already exists");
    }
}