using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json.Linq;
using SolveX.Business.Assets.Domain.Models;
using SolveX.Business.Assets.Domain.Repositories;
using SolveX.Framework.Domain.Exceptions;
using SolveX.Framework.Utilities.Common;
using System.Numerics;
using System.Text.Json;

namespace SolveX.Business.Assets.Domain.DomainServices;

public class AssetDomainService : IAssetDomainService
{
    private readonly IAssetRepository _assetRepository;

    public AssetDomainService(IAssetRepository assetRepository)
    {
        _assetRepository = assetRepository;
    }

    public async Task<int> Create(int id, string title, string data)
    {
        if (await _assetRepository.GetAsync(id) is not null)
        {
            throw new BadDataException($"The asset with id [{id}] already exists");
        }

        if ((await _assetRepository.Query().Where(asset => asset.Title == title).ToListAsync()).FirstOrDefault() is not null)
        {
            throw new BadDataException($"The asset with title [{title}] already exists");
        }

        var token = JToken.Parse(data);

        if (token is JArray)
        {
            throw new BadDataException("The JSON should be an object not array");
        }

        try
        {
            JsonSerializer.Deserialize<object>(data);
        }
        catch
        {
            throw new BadDataException("The JSON is not valid");
        }

        await _assetRepository.InsertAsync(new Asset { Title = title, Data = data, Id = id });

        return id;
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

   
}
