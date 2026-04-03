using static FirstWebApplication.SD;

namespace FirstWebApplication.Models
{
    public class ApiRequest
    {
        public ApiType apiType { get; set; } = ApiType.GET;
        public string url { get; set; }
        public object? data { get; set; }
        public string? token { get; set; }
    }
}
