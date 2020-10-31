using CloudParserService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudParserService.Lib
{
    public class MongoDBWriter : IDBWriter
    {
        private readonly MongoDBEntryContext _context;

        public MongoDBWriter(Settings settings)
        {
            _context = new MongoDBEntryContext(settings);
        }
        public async Task Write(Entry data, DateTime time)
        {
            await _context.Entries.InsertOneAsync(data);
        }

        public async Task WriteAll(IEnumerable<Entry> data)
        {
            await _context.Entries.InsertManyAsync(data);
        }
    }
}
