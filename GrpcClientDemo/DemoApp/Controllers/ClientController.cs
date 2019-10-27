using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;

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

		[HttpPost]
		public ActionResult<Purchase> ProcessOrder(Purchase input)
		{
			var info = _client.GetItemInfo(new ItemInfoRequest{Name = input.Item});
			if(input.Quantity <= info.CurrentStock)
			{
                float dis = _client.GetBulkDiscount(new BulkDiscountRequest{Quantity = input.Quantity}).Rate;
                input.Payment = input.Quantity * info.UnitPrice * (1 - dis / 100); 
				return Ok(input);
			}
			
			return NotFound();
		}
	}
	
}

