using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Fibon.Api.Repository
{
    public interface IRepository
    {
        void Insert(int number, int result);

        int? Get(int number);
    }

    public class Repository : IRepository
    {
        private readonly ConcurrentDictionary<int, int> _storage = new ConcurrentDictionary<int, int>();

        public void Insert(int number, int result)
        {
            _storage[number] = result;
        }

        public int? Get(int number)
        {
            return _storage.TryGetValue(number, out int result) ? result : (int?)null;
        }
    }
}