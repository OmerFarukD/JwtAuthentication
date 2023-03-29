namespace JwtAuthentication.Core.Dtos;

public class ClientLoginDto
{
    public int ClientId { get; set; }
    public string? ClientSecret { get; set; }
}