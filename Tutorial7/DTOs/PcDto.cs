namespace Tutorial7.DTOs;

public class PcListItemDto
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class PcComponentDto
{
    public string Code { get; set; } = null!;
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public int Amount { get; set; }
    public string ManufacturerName { get; set; } = null!;
    public string ComponentTypeName { get; set; } = null!;
}

public class PcCreateDto
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}

public class PcUpdateDto
{
    public string Name { get; set; } = null!;
    public double Weight { get; set; }
    public int Warranty { get; set; }
    public DateTime CreatedAt { get; set; }
    public int Stock { get; set; }
}
