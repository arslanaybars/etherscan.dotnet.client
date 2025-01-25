namespace EtherScan.Dotnet.Client.Models.Base;

public class Response<T>
{
    public required string Status { get; set; }
    public bool IsSuccess => Status == "1" && Message == "OK";
    public required string Message { get; set; }
    public T? Result { get; set; }
    
    
    public Response<TResponse> CreateFailedResponse<TResponse>(string message)
    {
        return new Response<TResponse>
        {
            Status = "0",
            Message = message,
            Result = default
        };
    }
}