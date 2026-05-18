using Tutorial7.DTOs;

namespace Tutorial7.Services;

public interface IPcService
{
    Task<List<PcListItemDto>> GetAllAsync();
    Task<List<PcComponentDto>?> GetComponentsAsync(int id);
    Task<PcListItemDto> CreateAsync(PcCreateDto dto);
    Task<PcListItemDto?> UpdateAsync(int id, PcUpdateDto dto);
    Task<bool> DeleteAsync(int id);
}
