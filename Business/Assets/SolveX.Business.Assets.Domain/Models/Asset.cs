using SolveX.Framework.Integration.Repositories.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace SolveX.Business.Assets.Domain.Models;
public class Asset : BaseEntity
{
    [System.ComponentModel.DataAnnotations.KeyAttribute()]
    [DatabaseGenerated(DatabaseGeneratedOption.None)]
    public new int Id { get; set; }

    public string Title { get; set; } = String.Empty;

    /// <summary>
    /// Asset json data
    /// </summary>
    public string Data { get; set; } = String.Empty;
}
