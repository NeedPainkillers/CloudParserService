using CloudParserService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudParserService.Lib
{
    public class MongoDBWriter : IDBWriter
    {
        MongoDBEntryContext _context;

        public MongoDBWriter(Settings settings)
        {
            _context = new MongoDBEntryContext(settings);
        }
        public async Task Write(Entry data, DateTime time)
        {
            //var entry = new Entry() { Body = data, Timestamp = time };
            await _context.Entries.InsertOneAsync(data);
        }

        public async Task WriteAll(IEnumerable<Entry> data)
        {
            //var entries = data.Select(x => new Entry() { Body = x, Timestamp = DateTime.UtcNow, AddedOn = DateTime.UtcNow });
            await _context.Entries.InsertManyAsync(data);
        }
    }
}
