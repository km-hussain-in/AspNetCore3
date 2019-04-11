using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DemoApp.Components.Pages
{
    using Services;

    public class VisitorsBase : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        protected IEnumerable<CounterEntry> Greeted;
    
        protected override void OnInit()
        {
            Greeted = Counter.Entries;
            Counter.Increment += (s, e) => Invoke(StateHasChanged);
        }
    }
}
