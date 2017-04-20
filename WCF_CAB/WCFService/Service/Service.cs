using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WCFService;

namespace Service
{
    public class Service:IContracts
    {
        public Double Add(Double x, Double y)
        {
            return x + y;
        }

        public Double Subtract(Double x, Double y)
        {
            return x - y;
        }

        public Double Multiply(Double x, Double y)
        {
            return x * y;
        }

        public Double Divide(Double x, Double y)
        {
            return x / y;
        }
    }
}
