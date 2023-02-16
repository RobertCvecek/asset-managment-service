using SolveX.Business.Assets.Domain.Models;

namespace SolveX.Business.Assets.Domain.DomainServices;
public interface IAssetDomainService
{
    /// <summary>
    /// Validates and creates asset
    /// </summary>
    /// <param name="asset">The <see cref="Asset"/> object that will be created</param>
    /// <param name="links">The links the <see cref="Asset"/> has to other assets already saved in database</param>
    /// <param name="validations">Validations for asset properties. Key value pair of property name and regex expression</param>
    /// <returns>Id of created asset</returns>
    public Task<int> Create(Asset asset, IEnumerable<int> links, Dictionary<string, string> validations);

    public Task<Asset> Get(int id);

    public Task<Asset?> Get(string title);

    public Task<Asset?> Get(string atribute, string attributeValue);

    public Task<IEnumerable<Asset>> GetLinkedAssets(int assetId);
}
