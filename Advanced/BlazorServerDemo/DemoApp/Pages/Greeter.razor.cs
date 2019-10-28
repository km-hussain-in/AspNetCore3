using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoApp.Pages
{
    using Services;

    public class GreeterComponent : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        [Inject]
        private IJSRuntime Script {get; set;}
		
        protected string VisitorName;

        protected string GreetMessage;
    	
        public async Task CountAndGreet()
        {
            int count = Counter.GetNextCount(VisitorName);
            GreetMessage = $"Number of greetings: {count}";
			string[] intervals = {"Night", "Morning", "Afternoon", "Evening"};
			int t = System.DateTime.Now.Hour / 6;
			await Script.InvokeVoidAsync("_hostFunctions.showGreeting", $"Good {intervals[t]} {VisitorName}");
        } 
    }
}
