namespace IP.RabbitMQ.RabbitMQSettings
{
    public class RabbitMQSettings<Request, Response>
        where Request : BaseDomains.BaseRequest
        where Response : BaseDomains.BaseResponse
    {
        public List<Request> Requests { get; set; }
        public string HostName { get; set; }
        public string QueueName { get; set; }
        public string RoutingKey { get; set; }
        public string Exchange { get; set; }
        public bool Exclusive { get; set; }
        public bool AutoDelete { get; set; }
        public Action<List<Response>> Callback { get; set; }
    }
}
