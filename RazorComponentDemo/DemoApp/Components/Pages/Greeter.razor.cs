using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;

namespace DemoApp.Components.Pages
{
    using Services;

    public class GreeterBase : ComponentBase
    {
        [Inject]
        protected CounterService Counter {get; set;}

        [Inject]
        private IJSRuntime Script {get; set;}

        [Parameter]
        public string TimeFormatString {get; set;}

        protected string VisitorName;
    
        protected override void OnInit()
        {
            Counter.Increment += (s, e) => Invoke(StateHasChanged);
        }

        protected void UpdateCounter()
        {
            Counter.GetNextCount(VisitorName);
            string text = string.Format(TimeFormatString, System.DateTime.Now);
            Script.InvokeAsync<bool>("hostPage.updateElement", "statusOutput", text);                 
        } 
    }
}
