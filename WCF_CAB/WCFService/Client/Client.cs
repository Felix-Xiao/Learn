using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client
{
    class Client
    {
        static void Main(string[] args)
        {
            MyService.ContractsClient client = new MyService.ContractsClient();

            Double x = Double.Parse(Console.ReadLine());
            Double y = Double.Parse(Console.ReadLine());
            Console.WriteLine(client.Add(x, y).ToString());
            Console.ReadLine();

            client.Close(); 
        }
    }
}
