using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace DemoApp.Pages
{
    using Services;

    public class GreetingsComponent : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        protected IEnumerable<CounterEventArgs> Greeted;
    
        protected override void OnInitialized()
        {
            Greeted = Counter.Entries;
            Counter.Increment += CounterIncrement;
        }

		private async void CounterIncrement(object sender, System.EventArgs e)
		{
			await InvokeAsync(StateHasChanged);
		}
    }
}

