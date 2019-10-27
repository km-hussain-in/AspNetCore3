using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if(args.Length < 2)
                CreateHostBuilder(args).Build().Run();
            else
                RunConsoleClient(args);
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void RunConsoleClient(string[] args)
        {
            string itm = args[0].ToLower();
            int qty = int.Parse(args[1]);

		 	var channel = Grpc.Net.Client.GrpcChannel.ForAddress("https://localhost:5001/");
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
