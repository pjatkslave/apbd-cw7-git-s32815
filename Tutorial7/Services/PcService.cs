using Microsoft.EntityFrameworkCore;
using Tutorial7.Data;
using Tutorial7.DTOs;
using Tutorial7.Models;

namespace Tutorial7.Services;

public class PcService : IPcService
{
    private readonly AppDbContext _db;

    public PcService(AppDbContext db)
    {
        _db = db;
    }

    public async Task<List<PcListItemDto>> GetAllAsync()
    {
        return await _db.PCs
            .Select(pc => new PcListItemDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            })
            .ToListAsync();
    }

    public async Task<List<PcComponentDto>?> GetComponentsAsync(int id)
    {
        var exists = await _db.PCs.AnyAsync(x => x.Id == id);
        if (!exists) return null;

        return await _db.PCComponents
            .Where(x => x.PCId == id)
            .Select(x => new PcComponentDto
            {
                Code = x.Component.Code,
                Name = x.Component.Name,
                Description = x.Component.Description,
                Amount = x.Amount,
                ManufacturerName = x.Component.Manufacturer.FullName,
                ComponentTypeName = x.Component.ComponentType.Name
            })
            .ToListAsync();
    }

    public async Task<PcListItemDto> CreateAsync(PcCreateDto dto)
    {
        var pc = new PC
        {
            Name = dto.Name,
            Weight = dto.Weight,
            Warranty = dto.Warranty,
            CreatedAt = dto.CreatedAt,
            Stock = dto.Stock
        };

        _db.PCs.Add(pc);
        await _db.SaveChangesAsync();

        return new PcListItemDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<PcListItemDto?> UpdateAsync(int id, PcUpdateDto dto)
    {
        var pc = await _db.PCs.FindAsync(id);
        if (pc == null) return null;

        pc.Name = dto.Name;
        pc.Weight = dto.Weight;
        pc.Warranty = dto.Warranty;
        pc.CreatedAt = dto.CreatedAt;
        pc.Stock = dto.Stock;

        await _db.SaveChangesAsync();

        return new PcListItemDto
        {
            Id = pc.Id,
            Name = pc.Name,
            Weight = pc.Weight,
            Warranty = pc.Warranty,
            CreatedAt = pc.CreatedAt,
            Stock = pc.Stock
        };
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var pc = await _db.PCs.Include(x => x.PCComponents).FirstOrDefaultAsync(x => x.Id == id);
        if (pc == null) return false;

        _db.PCs.Remove(pc);
        await _db.SaveChangesAsync();
        return true;
    }
}
