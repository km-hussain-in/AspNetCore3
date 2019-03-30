using Microsoft.AspNetCore.Components;

namespace DemoApp.Components.Pages
{
    using Services;

    public class GreeterBase : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        protected string Visitor = "Visitor";
        protected string Message;
        protected string NameInput;

        protected void UpdateOutput()
        {
            int count = Counter.GetNextCount(NameInput);
            Visitor = NameInput;
            Message = $"Your count is {count}";        
        } 
    }
}