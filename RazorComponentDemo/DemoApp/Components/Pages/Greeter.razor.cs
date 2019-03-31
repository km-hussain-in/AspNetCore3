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
        private IJSRuntime JS {get; set;}

        protected string Visitor = "World";
        protected string Message;
        protected string NameInput;
 
        protected override void OnInit()
        {
            Counter.Increment += (name, count) => 
            {
                if(name == Visitor)
                    Message = $"Your count is {count}";
                else
                    Message = $"{name}'s count is {count}";
                Invoke(() => StateHasChanged());  
            };
        }

        protected void UpdateVisitorCount()
        {
            Visitor = NameInput;
            Counter.GetNextCount(Visitor);
            JS.InvokeAsync<object>("indexStatus.update", new DotNetObjectRef(new ServerTime()));  
        } 

        public class ServerTime
        {
            [JSInvokable]
            public string GetInfo() => $"Time on server is {System.DateTime.Now}";
        }
    }
}