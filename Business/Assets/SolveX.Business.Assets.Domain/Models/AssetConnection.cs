using SolveX.Framework.Integration.Repositories.Models;

namespace SolveX.Business.Assets.Domain.Models;

public class AssetConnection : BaseEntity
{
    public int AssetId { get; set; }

    /// <summary>
    /// The id of asset that its connected to
    /// </summary>
    public int ConnectedTo {get;set;}
}
