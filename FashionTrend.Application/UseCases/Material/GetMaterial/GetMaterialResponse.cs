using System;

public class GetMaterialResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string Color { get; set; }
    public DateTimeOffset? DateCreated { get; set; }
}