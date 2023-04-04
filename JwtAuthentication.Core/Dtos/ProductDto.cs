namespace JwtAuthentication.Core.Dtos;

public class ProductDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? UserId { get; set; }
}