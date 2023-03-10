using System.ComponentModel.DataAnnotations;

namespace SolveX.AssetManagment.Models.Requests;

public class CreateAssetRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; } = String.Empty;

    [Required]
    public string Data { get; set; } = String.Empty;

    /// <summary>
    /// Id's of assets that the current asset is linked to
    /// </summary>
    public IEnumerable<int> Links { get; set; } = Enumerable.Empty<int>();

    /// <summary>
    /// Key value pair containing property name and regex for its value validation
    /// </summary>
    public Dictionary<string, string> Validations { get; set; } = new Dictionary<string, string>();
}
