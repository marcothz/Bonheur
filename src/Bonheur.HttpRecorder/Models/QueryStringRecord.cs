using System.Collections;
using Microsoft.Extensions.Primitives;

namespace Bonheur.HttpRecorder.Models
{
    public class QueryStringRecord : IEnumerable<KeyValuePair<string, StringValues>>
    {
        private readonly IEnumerable<KeyValuePair<string, StringValues>> _items;

        public QueryStringRecord(IEnumerable<KeyValuePair<string, StringValues>> items)
        {
            _items = items;
        }

        public IEnumerator<KeyValuePair<string, StringValues>> GetEnumerator()
        {
            return _items.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}