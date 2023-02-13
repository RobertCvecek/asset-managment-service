using SolveX.Business.Assets.API.Dtos;

namespace SolveX.Business.Assets.API.Services;
public interface IAssetService
{
    public Task<int> Create(int id, string title, string data);

    public Task<AssetDto> Get(int id);

    public Task<AssetDto> Get(string title);

    public Task<AssetDto> Get(string atribute, string attributeValue);
}
