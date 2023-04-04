using System.Text.Json.Serialization;

namespace SharedLibrary.Dtos;

public class Response<T> where T : class
{
    public T? Data { get; set; }
    public int StatusCode { get; set; }
    public ErrorDto? Error { get; set; }
    
    [JsonIgnore]
    public bool IsSuccessful { get; set; }

    public static Response<T> Success(T data, int statusCode) => new Response<T> {Data = data,StatusCode = statusCode,IsSuccessful = true};
    public static Response<T> Success(int statusCode) => new Response<T> {Data =default ,StatusCode = statusCode,IsSuccessful = true};
    public static Response<T> Fail(ErrorDto errorDto, int statusCode) => new Response<T> {Error = errorDto,StatusCode = statusCode,IsSuccessful = false};

    public static Response<T> Fail(string errorMessage, int statusCode, bool isShow)
    {
        var dto = new ErrorDto(errorMessage,isShow);
        return new Response<T> { Error = dto,StatusCode = statusCode, IsSuccessful = false};
    }
}