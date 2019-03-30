using System;
using System.Collections.Generic;

namespace DemoApp.Services
{
    public class CounterService
    {
        private IDictionary<string, int> counters = new Dictionary<string, int>();

        public event Action<string, int> Increment;

        public virtual int GetNextCount(string name)
        {
            lock(counters)
            {
                int count;
                counters.TryGetValue(name, out count);
                counters[name] = ++count;
                Increment?.Invoke(name, count);
                return count;
            }
        }
    }
}