namespace RoyalHotel.DTO
{
    public class ApiResponse<TData>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public int statusCode { get; set; }
        public object? errorMessage { get; set; } = string.Empty;
        public TData? Data { get; set; }
        public DateOnly TimeStamp { get; set; } = DateOnly.FromDateTime(DateTime.Now);

        public static ApiResponse<TData> Create(bool Success, string Message, int statusCode, object? errorMessage = null, TData? data = default)
        {
            return new ApiResponse<TData>
            {
                Success = Success,
                Message = Message,
                statusCode = statusCode,
                errorMessage = errorMessage,
                Data = data
            };
        }

        public static ApiResponse<TData> Ok
            (TData data, string message) => Create(true, message,200,null,data);
        public static ApiResponse<TData> CreatedAt
            (TData data, string message) => Create(true, message, 201,null, data);
        public static ApiResponse<TData> NoContent
            (string message = "Operation Completed Successfully") => Create(true, message, 204);
        public static ApiResponse<TData> NotFound
            (string message = "Resource Not Found") => Create(false, message, 404);
        public static ApiResponse<TData> BadRequest
            (string message,object? errorMessage = null) => Create(false, message, 400, errorMessage);
        public static ApiResponse<TData> Conflict
            (string message) => Create(false, message, 409);

        public static ApiResponse<TData> Error
            (string message, object? errorMessage = null) => Create(false, message, 500, errorMessage);

    }
}
