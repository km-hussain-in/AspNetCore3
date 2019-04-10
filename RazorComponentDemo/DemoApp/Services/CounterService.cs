using System;
using System.Linq;
using System.Collections.Generic;

namespace DemoApp.Services
{
    public class CounterEntry
    {
        public string Name {get;}

        public int Count {get;}

        public CounterEntry(KeyValuePair<string, int> pair)
        {
            Name = pair.Key;
            Count = pair.Value;
        }
    }

    public class CounterService
    {
        private IDictionary<string, int> counters = new Dictionary<string, int>();

        public event EventHandler Increment;

        public virtual int GetNextCount(string name)
        {
            lock(counters)
            {
                int count;
                counters.TryGetValue(name, out count);
                counters[name] = ++count;
                Increment?.Invoke(this, EventArgs.Empty);
                return count;
            }
        }

        public IEnumerator<CounterEntry> GetEnumerator()
        {
            return counters.Select(k => new CounterEntry(k)).GetEnumerator();
        }
    }
}