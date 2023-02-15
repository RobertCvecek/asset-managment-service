using SolveX.Business.Assets.Domain.Models;

namespace SolveX.Business.Assets.Domain.DomainServices;
public interface IAssetDomainService
{
    public Task<int> Create(Asset asset, IEnumerable<int> links, Dictionary<string, string> validations);

    public Task<Asset> Get(int id);

    public Task<Asset?> Get(string title);

    public Task<Asset?> Get(string atribute, string attributeValue);
    
    public Task<IEnumerable<Asset>> GetLinkedAssets(int assetId);
}
