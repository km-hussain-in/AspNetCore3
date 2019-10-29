using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Grpc.Core;

namespace DemoApp.Services
{

    public class ShopKeeperService : ShopKeeper.ShopKeeperBase
    {		
        private XElement shop = XElement.Load("shop.xml");

        public override Task<ItemInfoReply> GetItemInfo(ItemInfoRequest request, ServerCallContext context)
        {
            var selection = from i in shop.Elements("item") 
                            where (string)i.Attribute("name") == request.Name
                            select new ItemInfoReply
                            {
                                CurrentStock = (int)i.Element("stock"),
                                UnitPrice = (double)i.Element("price")
                            };
            var info = selection.FirstOrDefault() ?? new ItemInfoReply();  

            return Task.FromResult(info);
        }

		public override async Task GetItemNames(Google.Protobuf.WellKnownTypes.Empty _, IServerStreamWriter<ItemInfoRequest> response, ServerCallContext context)
		{
            var selection = from i in shop.Elements("item") 
                            select new ItemInfoRequest{Name = (string)i.Attribute("name")}; 
			foreach(var entry in selection)
				await response.WriteAsync(entry);
		}

    }
}

