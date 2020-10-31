using CloudParserService.Model;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CloudParserService.Lib
{
    public interface IDBWriter
    {
        public Task Write(Entry data, DateTime time);

        public Task WriteAll(IEnumerable<Entry> data);
    }
}
