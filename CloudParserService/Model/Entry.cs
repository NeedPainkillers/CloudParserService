using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson.Serialization.IdGenerators;
using System;
using System.Collections.Generic;

namespace CloudParserService.Model
{
    public class Entry
    {
        [BsonId(IdGenerator = typeof(StringObjectIdGenerator))]
        public string EntryId { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Price { get; set; } = string.Empty;
        public string PriceCurrency { get; set; } = string.Empty;
        public string Time { get; set; } = string.Empty;
        public string Title { get; set; } = string.Empty;
        public string Url { get; set; } = string.Empty;

        public IEnumerable<string> Images { get; set; }

        public DateTime Timestamp { get; set; } = default;

        public DateTime AddedOn { get; set; } = DateTime.UtcNow;
        //public DateTime UpdatedOn { get; set; } = DateTime.UtcNow;


    }
}
