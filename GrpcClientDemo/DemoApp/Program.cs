using System;
using Grpc.Net.Client;

namespace DemoApp
{

    class Program
    {
        static void Main(string[] args)
        {
            string itm = args[0].ToLower();
            int qty = int.Parse(args[1]);

		 	var channel = GrpcChannel.ForAddress("https://localhost:5001/");
			var client = new ShopKeeper.ShopKeeperClient(channel);

			var info = client.GetItemInfo(new ItemInfoRequest{Name = itm});
            if(qty <= info.CurrentStock)
            {
                float dis = client.GetBulkDiscount(new BulkDiscountRequest{Quantity = qty}).Rate;
                double amt = qty * info.UnitPrice * (1 - dis / 100); 
                Console.WriteLine($"Total Payment: {amt:0.00}");
            }
            else
                Console.WriteLine("Not available!");
        }
    }
}
