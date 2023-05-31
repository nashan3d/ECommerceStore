namespace ECommerceStore.API.Middleware
{
    public class ErrorDetail
    {
        public ErrorDetail()
        {
        }

        public int StatusCode { get; set; }
        public object Message { get; set; }
    }
}