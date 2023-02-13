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
    [AllowAnonymous]
    public async Task<IActionResult> Create([FromBody]CreateAssetRequest request)
    {
        int id = await _assetService.Create(request.Id, request.Title, request.Data);
        return Ok(id);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Search([FromRoute] int id)
    {
        AssetDto asset = await _assetService.Get(id);

        if(asset == null)
        {
            return NotFound();
        }
        return Ok(asset);
    }

    [HttpGet()]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Search([Required][FromQuery] string title)
    {
        AssetDto asset = await _assetService.Get(title);

        if (asset == null)
        {
            return NotFound();
        }
        return Ok(asset);
    }

    [HttpGet("attribute")]
    [ProducesResponseType(typeof(LoginResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.NotFound)]
    [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
    [AllowAnonymous]
    public async Task<IActionResult> Search([Required][FromQuery] string attibuteName, [Required][FromQuery] string attibuteValue)
    {
        AssetDto asset = await _assetService.Get(attibuteName, attibuteValue);

        if (asset == null)
        {
            return NotFound();
        }
        return Ok(asset);
    }
}