using SolveX.Business.Assets.API.Dtos;

namespace SolveX.Business.Assets.API.Services;
public interface IAssetService
{
    public Task<int> Create(int id, string title, string data, IEnumerable<int> links);

    public Task<AssetDto> Get(int id);

    public Task<AssetDto> Get(string title);

    public Task<AssetDto> Get(string atribute, string attributeValue);

    public Task<IEnumerable<AssetDto>> GetLinkedAssets(int assetId);
}
