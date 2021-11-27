namespace mongocrud.Settings
{
    public class MongoDbSettings
    {
        public string Host { get; set; }
        public int Port { get; set; }

        public string ConnectionString
        {
            get
            {
                //for local b usage please use this connection string
                //return $"mongodb://{Host}:{Port}";


                // for cloud db usage please use this connection string
                return "mongodb+srv://koraytug:Welcome1@cluster0.ey43w.mongodb.net/TESTAPP?retryWrites=true&w=majority";
            }
        }
    }
}
