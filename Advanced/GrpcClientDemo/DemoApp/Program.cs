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
            if(args.Length > 0 && args[0] == "items")
                RunConsoleClient(args).Wait();
            else
                CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        private static async Task RunConsoleClient(string[] args)
        {
		 	var channel = Grpc.Net.Client.GrpcChannel.ForAddress("http://localhost:5000/");
			var client = new ShopKeeper.ShopKeeperClient(channel);
			var result = client.GetItemNames(new Google.Protobuf.WellKnownTypes.Empty());
			await foreach(var item in result.ResponseStream.ReadAllAsync())
				Console.WriteLine(item.Name);
        }
    }
}

