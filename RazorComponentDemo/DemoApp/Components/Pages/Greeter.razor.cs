using Microsoft.AspNetCore.Components;

namespace DemoApp.Components.Pages
{
    using Services;

    public class GreeterBase : ComponentBase
    {
        [Inject]
        private CounterService Counter {get; set;}

        protected string OutputMessage;
        protected string NameInput;

        protected void UpdateOutputMessage()
        {
            int count = Counter.GetNextCount(NameInput);
            OutputMessage = $"Your count is {count}";        
        } 
    }
}