using Backend.Domain.Response;
using System.Net;
using System.Text.Json;

public class JwtSettings
{
    public string Key { get; set; }
    public string Issuer { get; set; }
    public string Audience { get; set; }
    public int ExpireMinutes { get; set; }
}