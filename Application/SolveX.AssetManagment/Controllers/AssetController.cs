using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using SolveX.AssetManagment.Models.Requests;
using SolveX.AssetManagment.Models.Responses;
using SolveX.Business.Assets.API.Dtos;
using SolveX.Business.Assets.API.Services;
using SolveX.Business.Users.API.Dtos;
using SolveX.Business.Users.API.Services;
using SolveX.Framework.WebAPI.Models;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Net;
using System.Reflection;
using System.Security.Claims;
using System.Text;

namespace SolveX.FlowerSpot.Controllers;

[ApiController]
[Route("asset")]
public class AssetController : ControllerBase
{
    private readonly IAssetService _assetService;

    public AssetController(IAssetService assetService)
    {
        _assetService = assetService;
    }

    [HttpPost("create")]
    [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Create([FromBody]CreateAssetRequest request)
    {
        int id = await _assetService.Create(request.Id, request.Title, request.Data, request.Links, request.Validations);
        return Ok(id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(AssetDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Search([FromRoute] int id)
    {
        AssetDto asset = await _assetService.Get(id);

        if(asset is null)
        {
            return NotFound();
        }
        return Ok(asset);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(AssetDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Search([Required][FromQuery] string title)
    {
        AssetDto asset = await _assetService.Get(title);

        if (asset is null)
        {
            return NotFound();
        }
        return Ok(asset);
    }

    [HttpGet("attribute")]
    [ProducesResponseType(typeof(AssetDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Search([Required][FromQuery] string attibuteName, [Required][FromQuery] string attibuteValue)
    {
        AssetDto asset = await _assetService.Get(attibuteName, attibuteValue);

        if (asset is null)
        {
            return NotFound();
        }
        return Ok(asset);
    }

    [HttpGet("linked/{id}")]
    [ProducesResponseType(typeof(AssetDto), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> GetLinkedAssets([FromRoute] int id)
    {
        IEnumerable<AssetDto> assets = await _assetService.GetLinkedAssets(id);

        if (assets is null || !assets.Any())
        {
            return NotFound();
        }
        return Ok(assets);
    }

    [HttpGet("export/{id}")]
    [ProducesResponseType(typeof(File), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [Authorize(Roles ="Admin")]
    public async Task<IActionResult> ExportAsset([FromRoute] int id)
    {
        ExcelAssetDto data = await _assetService.Export(id);

        return File(
            fileContents: data.Content.ToArray(),
            contentType: "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            fileDownloadName: data.Title
        );
    }
}