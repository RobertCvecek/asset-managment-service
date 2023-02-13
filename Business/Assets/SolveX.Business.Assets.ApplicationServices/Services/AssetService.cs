using AutoMapper;
using SolveX.Business.Assets.API.Dtos;
using SolveX.Business.Assets.API.Services;
using SolveX.Business.Assets.Domain.DomainServices;

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

    public async Task<int> Create(int id, string title, string data, IEnumerable<int> links)
    {
        return await _assetDomainService.Create(id, title, data, links);
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
