using AutoMapper;
using SolveX.Business.Assets.API.Dtos;
using SolveX.Business.Assets.API.Services;
using SolveX.Business.Assets.Domain.DomainServices;
using SolveX.Framework.Utilities.Common;

namespace SolveX.Business.Assets.ApplicationServices.Services;
public class AssetService : IAssetService
{
    private readonly IAssetDomainService _assetDomainService;
    private readonly IMapper _mapper;

    public AssetService(IAssetDomainService assetService, IMapper mapper)
    {
        _assetDomainService = assetService;
        _mapper = mapper;
    }

    public async Task<int> Create(int id, string title, string data, IEnumerable<int> links, Dictionary<string, string> validations)
    {
        return await _assetDomainService.Create(id, title, data, links, validations);
    }

    public async Task<ExcelAssetDto> Export(int id)
    {
        AssetDto asset = _mapper.Map<AssetDto>(await _assetDomainService.Get(id));

        return new()
        {
            Title = asset.Title,
            Content = ExportUtilities.ExportExcel(new
            {
                Title = asset.Title,
                Id = asset.Id.ToString(),
                Data = asset.Data
            })
        };
    }

    public async Task<AssetDto> Get(int id)
    {
        return _mapper.Map<AssetDto>(await _assetDomainService.Get(id));
    }

    public async Task<AssetDto> Get(string title)
    {
        return _mapper.Map<AssetDto>(await _assetDomainService.Get(title));
    }

    public async Task<AssetDto> Get(string atribute, string attributeValue)
    {
        return _mapper.Map<AssetDto>(await _assetDomainService.Get(atribute, attributeValue));
    }

    public async Task<IEnumerable<AssetDto>> GetLinkedAssets(int assetId)
    {
        return _mapper.Map<IEnumerable<AssetDto>>(await _assetDomainService.GetLinkedAssets(assetId));
    }
}
