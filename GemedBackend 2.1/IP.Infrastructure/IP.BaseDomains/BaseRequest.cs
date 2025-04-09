namespace IP.BaseDomains
{
    public class BaseRequest
    {
        public int Id { get; set; }
        public string ConnectionString { get; set; }
        public string ProviderName { get; set; }
        public string Message { get; set; }
    }
}
