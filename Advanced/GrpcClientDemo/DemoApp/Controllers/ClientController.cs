using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Grpc.Core;

namespace DemoApp.Controllers
{
	using Models;
	
	[ApiController]
	[Route("shop/client")]
	public class ClientController : ControllerBase
	{
		private readonly ShopKeeper.ShopKeeperClient _client;

		public ClientController(ShopKeeper.ShopKeeperClient client)
		{
			_client = client;
		}

		[HttpGet]
		public async Task<IEnumerable<ItemInfoRequest>> GetItems()
		{
			var items = new List<ItemInfoRequest>();
			var result = _client.GetItemNames(new Google.Protobuf.WellKnownTypes.Empty());
			
			await foreach(var item in result.ResponseStream.ReadAllAsync())	
				items.Add(item);

			return items;
		}

		[HttpPost]
		public async Task<ActionResult<Purchase>> ProcessOrder(Purchase input)
		{
			var info = await _client.GetItemInfoAsync(new ItemInfoRequest{Name = input.Item});
			if(input.Quantity <= info.CurrentStock)
			{
                input.Payment = 1.06 * input.Quantity * info.UnitPrice; 
				return Ok(input);
			}
			
			return NotFound();
		}

		
	}
	
}

