using System.ComponentModel.DataAnnotations;

namespace SolveX.AssetManagment.Models.Requests;

public class CreateAssetRequest
{
    [Required]
    public int Id { get; set; }

    [Required]
    public string Title { get; set; }

    [Required]
    public string Data { get; set; }
}
