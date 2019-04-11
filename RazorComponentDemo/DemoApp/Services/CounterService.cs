using System;
using System.Linq;
using System.Collections.Generic;

namespace DemoApp.Services
{
    public class CounterEventArgs : EventArgs
    {
        public string Name {get;}

        public int Count {get;}

        public CounterEventArgs(string name, int count)
        {
            Name = name;
            Count = count;
        }

        internal CounterEventArgs(KeyValuePair<string, int> pair) : this(pair.Key, pair.Value) {}
    }

    public class CounterService
    {
        private IDictionary<string, int> counters = new Dictionary<string, int>();

        public event EventHandler<CounterEventArgs> Increment;

        public virtual int GetNextCount(string name)
        {
            lock(counters)
            {
                int count;
                counters.TryGetValue(name, out count);
                counters[name] = ++count;
                Increment?.Invoke(this, new CounterEventArgs(name, count));
                return count;
            }
        }

        public IEnumerable<CounterEventArgs> Entries => counters.Select(pair => new CounterEventArgs(pair));
    }
}
