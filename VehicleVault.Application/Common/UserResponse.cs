using System.Net;
using System.Text.Json.Serialization;

namespace VehicleVault.Application.Common;

public class UserResponse<T>
{
    [JsonPropertyOrder(1)]
    public int? TotalCount { get; set; }
    
    [JsonPropertyOrder(2)]
    public HttpStatusCode  HttpStatusCode  { get; set; }
    
    [JsonPropertyOrder(3)]
    public string Message { get; set; }
    
    [JsonPropertyOrder(5)]
    public DateTime Timestamp { get; set; }
    
    [JsonPropertyOrder(99)] // Ensures `data` appears last
    public T? Data { get; set; }

    public UserResponse(int? totalCount, HttpStatusCode httpStatusCode, string message, T? data)
    {
        TotalCount = totalCount;
        HttpStatusCode = httpStatusCode;
        Message = message;
        Timestamp = DateTime.UtcNow;
        Data = data;
    }
}