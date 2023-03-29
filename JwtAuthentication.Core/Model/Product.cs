namespace JwtAuthentication.Core.Model;

public class Product
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public double Price { get; set; }
    public string? UserId { get; set; }
}