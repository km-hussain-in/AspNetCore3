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

        [Parameter]
        public int Seed {get; set;}

        protected string VisitorName = "World";
        protected string TokenMessage;
    
        protected override void OnInit()
        {
            Counter.Increment += (name, count) => 
            {
                TokenMessage = $"Generated token {name}#{count + Seed}";
                Invoke(() => StateHasChanged());  
            };
        }

        protected void GenerateToken()
        {
            Counter.GetNextCount(VisitorName);
            Script.InvokeAsync<bool>("homePage.updateElement", "statusOutput", System.DateTime.Now);  
        } 
    }
}
