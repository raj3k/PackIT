namespace PackIT.Application.Dto;

public class PackingItemDto
{
    public string Name { get; set; }
    public uint Quantity { get; set; }
    public bool IsPacked { get; set; }
}