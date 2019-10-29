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
using Grpc.Core;

namespace DemoApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
			#if INSECURE
			//disable transport level security
			AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
			#endif
            if(args.Length == 1)
                RunConsoleClient(args);
            else
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static void RunConsoleClient(string[] args)
        {
		 	var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5000/");
			var client = new ShopKeeper.ShopKeeperClient(channel);
            var info = client.GetItemInfo(new ItemInfoRequest{Name = args[0]});
            
            Console.WriteLine($"Stock: {info.CurrentStock}");
        }
    }
}

