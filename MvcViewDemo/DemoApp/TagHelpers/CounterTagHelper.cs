using System.Collections.Concurrent;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace DemoApp.TagHelpers
{
	[HtmlTargetElement("span", Attributes = "app-count-for")]
	public class CounterTagHelper : TagHelper
	{
		private static ConcurrentDictionary<string, int> counters = new ConcurrentDictionary<string, int>();
		
		public string AppCountFor {get; set;}
		
		public override void Process(TagHelperContext context, TagHelperOutput output)
		{
			int count;
			counters.TryGetValue(AppCountFor, out count);
			counters[AppCountFor] = ++count;
			output.Attributes.RemoveAll("app-count-for");
			output.Content.SetContent(count.ToString());
		}
	}
	
}

