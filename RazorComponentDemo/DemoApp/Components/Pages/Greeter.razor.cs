using Microsoft.AspNetCore.Components;

namespace DemoApp.Components.Pages
{
    using Services;

    public class GreeterBase : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        [Inject]
        private Microsoft.JSInterop.IJSRuntime JS {get; set;}

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
            JS.InvokeAsync<object>("indexStatus.update", $"Time on server is {System.DateTime.Now}");  
        } 
    }
}