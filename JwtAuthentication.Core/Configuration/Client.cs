﻿namespace JwtAuthentication.Core.Configuration;

public class Client
{
    public string Id { get; set; }
    public string? Secret { get; set; }
    public List<string> Audience { get; set; } = new();
}