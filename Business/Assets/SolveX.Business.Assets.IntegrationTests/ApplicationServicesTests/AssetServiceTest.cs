
using AutoBogus;
using Bogus;
using FluentAssertions;
using NUnit.Framework;
using SolveX.Business.Assets.API.Dtos;
using SolveX.Business.Assets.API.Services;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Integration.Context;
using SolveX.Business.Assets.IntegrationTests.Generators;
using SolveX.Framework.TestingUtility.DependencyInjectionTests;

namespace SolveX.Business.Assets.IntegrationTests.ApplicationServices;

public class AssetServiceTest : InjectionBasedTest
{
    private IAssetService _service;
    private AssetTestDataGenerator _generator;

    [OneTimeSetUp]
    public void Setup()
    {
        _service = Resolve<IAssetService>();
        _generator = new(Resolve<AssetContext>());
    }

    /// <summary>
    /// Test if asset service inserts asset
    /// </summary>
    /// <returns></returns>
    [Test]
    public async Task ShouldGetAssetById()
    {
        Asset asset = new AutoFaker<Asset>().Generate();
        int id = await _generator.Insert(asset);

        AssetDto assetDto = await _service.Get(asset.Id);

        assetDto.Should().BeEquivalentTo(asset);
    }
}
