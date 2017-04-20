using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace WCFService
{
    [ServiceContract]
    public interface IContracts
    {
        [OperationContract]
        Double Add(Double x, Double y);

        [OperationContract]
        Double Subtract(Double x, Double y);

        [OperationContract]
        Double Multiply(Double x, Double y);

        [OperationContract]
        Double Divide(Double x, Double y);
    }
}
