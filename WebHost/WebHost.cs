using System;
using System.IO;
using System.Web;
using System.Web.Hosting;
//using System.Web.Routing;

//[assembly: PreApplicationStartMethod(typeof(WebHost.RouteInitializer), "Initialize")]

namespace WebHost
{
    public class WebHost : MarshalByRefObject
    {
        public void ProcessRequest(string page, string query, StreamWriter output)
        {
            HttpRuntime.ProcessRequest(new SimpleWorkerRequest(page, query, output));
        }

        public override object InitializeLifetimeService()
        {
            // This tells the CLR not to surreptitiously 
            // destroy this object -- it's a singleton
            // and will live for the life of the appdomain
            return null;
        }
    }
}
