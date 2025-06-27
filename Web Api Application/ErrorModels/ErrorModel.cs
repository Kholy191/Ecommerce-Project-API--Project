namespace Web_Api_Application.ErrorModels
{
    public class ErrorModel
    {
        public int statusCode { get; set; }
        public string message { get; set; } = string.Empty;
    }
}
