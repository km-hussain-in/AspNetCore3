using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Grpc.Core;
using Microsoft.Extensions.Logging;

namespace DemoApp.Services
{

    public class ShopKeeperService : ShopKeeper.ShopKeeperBase
    {		
        public override Task<ItemInfoReply> GetItemInfo(ItemInfoRequest request, ServerCallContext context)
        {
            string[] items = { "cpu", "ram", "hdd", "motherboard", "keyboard", "mouse", "monitor" };
            double[] prices = { 200, 30, 80, 1300, 20, 10, 100 };
            int[] stocks = { 100, 500, 90, 100, 50, 50, 20 };

            int i = Array.IndexOf(items, request.Name);
			var info = new ItemInfoReply();
            if(i >= 0)
            {
                info.UnitPrice = 1.06 * prices[i];
               	info.CurrentStock = stocks[i];
            }
    
            return Task.FromResult(info);
        }

		public override Task<BulkDiscountReply> GetBulkDiscount(BulkDiscountRequest request, ServerCallContext context)
		{
			return Task.FromResult(new BulkDiscountReply());
		}
    }
}

