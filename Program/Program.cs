using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Web.Hosting;

namespace ConsoleApplication6
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Initializing ASP.NET runtime...");
            var host = (WebHost.WebHost)ApplicationHost.CreateApplicationHost(
                typeof(WebHost.WebHost),
                "/",
                Directory.GetCurrentDirectory());
            Console.WriteLine("Done.");

            Console.WriteLine("Initializing HTTP listener...");
            var server = new HttpListener();
			server.Prefixes.Add("http://127.0.0.1:12345/");
            server.Start();
            Console.WriteLine("Done.");

            var query = "";
            while (query != "q=1")
            {
                var context = server.GetContext();

                using (var response = context.Response)
                {
                    using (var writer = new StreamWriter(response.OutputStream))
                    {
                        var page = context.Request.Url.LocalPath.Substring(1);
                        if (String.IsNullOrWhiteSpace(page))
                            page = "Default.cshtml";

                        query = context.Request.Url.Query.Replace("?", "");

                        host.ProcessRequest(page, query, writer);
                        Console.WriteLine("[{0}][{1}]", page, query);
                    }
                }
            }

            if (Debugger.IsAttached)
            {
                Console.WriteLine("\nPress <any key> to continue.");
                Console.ReadKey(true);
            }
        }
    }
}
