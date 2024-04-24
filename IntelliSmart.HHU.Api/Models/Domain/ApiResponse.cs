namespace IntelliSmart.HHU.Api.Models.Domain
{
    public class ApiResponse
    {
        public int StatusCode { get; set; }
        public string? Message { get; set; }
        public object? Data { get; set; }
    }
}