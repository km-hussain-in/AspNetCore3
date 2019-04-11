using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoApp.Components.Pages
{
    using Services;

    public class GreeterBase : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        [Inject]
        private IJSRuntime Script {get; set;}

        protected string VisitorName;

        protected string GreetMessage;
    
        protected void CountAndGreet()
        {
            int count = Counter.GetNextCount(VisitorName);
            GreetMessage = $"Number of greetings is {count}";
            Script.InvokeAsync<bool>("hostPage.updateElement", "headerOutput", $"Hello {VisitorName}");                 
        } 
    }
}
