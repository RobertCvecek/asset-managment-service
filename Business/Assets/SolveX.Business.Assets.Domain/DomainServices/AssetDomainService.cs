using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Framework.Domain.Exceptions;
using SolveX.Framework.Utilities.Common;
using System.Text.Json;
using System.Text.RegularExpressions;

namespace SolveX.Business.Assets.Domain.DomainServices;

public class AssetDomainService : IAssetDomainService
{
    private readonly IAssetRepository _assetRepository;
    private readonly IAssetConnectionRepository _assetConnectionRepository;

    public AssetDomainService(IAssetRepository assetRepository, IAssetConnectionRepository assetConnectionRepository)
    {
        _assetConnectionRepository = assetConnectionRepository;
        _assetRepository = assetRepository;
    }

    /// <inheritdoc/>
    public async Task<int> Create(Asset asset, IEnumerable<int> links, Dictionary<string, string> validations)
    {
        if (await _assetRepository.GetAsync(asset.Id) is not null)
        {
            throw new BadDataException($"The asset with id [{asset.Id}] already exists");
        }

        if ((await _assetRepository.Query().Where(asset => asset.Title == asset.Title).ToListAsync()).FirstOrDefault() is not null)
        {
            throw new BadDataException($"The asset with title [{asset.Title}] already exists");
        }

        try
        {
            JsonSerializer.Deserialize<object>(asset.Data);
        }
        catch
        {
            throw new BadDataException("The JSON is not valid");
        }

        var token = JToken.Parse(asset.Data);

        if (token is JArray)
        {
            throw new BadDataException("The JSON should be an object not array");
        }

        foreach (int link in links)
        {
            if (await _assetRepository.GetAsync(link) is null)
            {
                throw new BadDataException($"The asset with id [{link}] does not exist. Connection cannot be created");
            }
        }

        foreach (KeyValuePair<string, string> kvp in validations)
        {
            try
            {
                Regex.Match("", kvp.Value);
            }
            catch
            {
                throw new BadDataException($"The regex {kvp.Value} for {kvp.Key} is not valid");
            }

            string value = JsonHelpers.GetPropertyValue(asset.Data, kvp.Key);

            if (value == null)
            {
                throw new BadDataException($"Property with name {kvp.Key} does not exist");
            }

            if (!Regex.Match(value, kvp.Value).Success)
            {
                throw new BadDataException($"The property {kvp.Key} does not meet the crieteria of {kvp.Value}");
            }
        }

        await _assetRepository.InsertAsync(asset);

        if (links.Any())
        {
            await _assetConnectionRepository.InsertAsync(links.Select(link => new AssetConnection()
            {
                AssetId = asset.Id,
                ConnectedTo = link
            }));
        }


        return asset.Id;
    }

    public Task<Asset> Get(int id)
    {
        return _assetRepository.GetAsync(id);
    }

    public async Task<Asset?> Get(string title)
    {
        return (await _assetRepository.Query().Where(asset => asset.Title == title).ToListAsync()).FirstOrDefault();
    }

    public async Task<Asset?> Get(string atribute, string attributeValue)
    {
        return (await _assetRepository.GetAll()).FirstOrDefault(asset =>
        {
            try
            {
                JObject json = JObject.Parse(asset.Data);

                return JsonHelpers.PropertyExistsWithValue(asset.Data, atribute, attributeValue);
            }
            catch
            {
                return false;
            }
        });
    }

    public async Task<IEnumerable<Asset>> GetLinkedAssets(int assetId)
    {
        List<AssetConnection> assetsConnections = await _assetConnectionRepository.Query().Where(assetConnection => assetConnection.AssetId == assetId).ToListAsync();

        List<Asset> assets = new List<Asset>();
        foreach (AssetConnection assetConnection in assetsConnections)
        {
            assets.Add(await _assetRepository.GetAsync(assetConnection.ConnectedTo));
        }
        return assets;
    }


}
