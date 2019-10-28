using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;

namespace DemoApp.Services
{

    public class ShopKeeperService : ShopKeeper.ShopKeeperBase
    {		
        static string[] items = { "cpu", "ram", "hdd", "motherboard", "keyboard", "mouse", "monitor" };

        public override Task<ItemInfoReply> GetItemInfo(ItemInfoRequest request, ServerCallContext context)
        {
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

		public override async Task GetItemNames(Google.Protobuf.WellKnownTypes.Empty _, IServerStreamWriter<ItemInfoRequest> response, ServerCallContext context)
		{
			foreach(string item in items)
				await response.WriteAsync(new ItemInfoRequest{Name = item});
		}

		public override Task<BulkDiscountReply> GetBulkDiscount(BulkDiscountRequest request, ServerCallContext context)
		{
			return Task.FromResult(new BulkDiscountReply());
		}
    }
}

