using CloudParserService.Model;
using MongoDB.Driver;

namespace CloudParserService.Lib
{
    public class MongoDBEntryContext
    {
        private readonly IMongoDatabase _database = null;

        public MongoDBEntryContext(Settings settings)
        {
            var client = new MongoClient(settings.connstr);
            if (client != null)
                _database = client.GetDatabase(settings.conndb);
        }

        public IMongoCollection<Entry> Entries
        {
            get
            {
                return _database.GetCollection<Entry>("Entry");
            }
        }
    }
}
