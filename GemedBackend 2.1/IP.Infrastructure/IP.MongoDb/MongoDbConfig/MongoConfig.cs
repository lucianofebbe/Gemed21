namespace IP.MongoDb.MongoDbConfig
{
    public class MongoConfig
    {
        public string ConnectionString { get; set; } = "mongodb://Gemed2022:Gemed2022Teste@localhost:27018/?authSource=admin";
        public string Database { get; set; } = "DbCache";
        public string Collection { get; set; }
    }
}
