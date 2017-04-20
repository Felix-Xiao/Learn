using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace Hosting
{
    class Host
    {
        static void Main(string[] args)
        {
            ServiceHost host = new ServiceHost(typeof(Service.Service));

            host.Open();

            Console.WriteLine("Service is Ready");
            Console.WriteLine("Press Any Key to Terminate...");
            Console.ReadLine();

            host.Close();
        }
    }
}
