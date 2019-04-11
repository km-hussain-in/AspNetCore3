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
    
        protected void CountAndGreet()
        {
            Counter.GetNextCount(VisitorName);
            Script.InvokeAsync<bool>("hostPage.updateElement", "headerOutput", $"Hello {VisitorName}");                 
        } 
    }
}
