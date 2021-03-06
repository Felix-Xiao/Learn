﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SimpleCABApp.MyService {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MyService.IContracts")]
    public interface IContracts {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Add", ReplyAction="http://tempuri.org/IContracts/AddResponse")]
        double Add(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Add", ReplyAction="http://tempuri.org/IContracts/AddResponse")]
        System.Threading.Tasks.Task<double> AddAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Subtract", ReplyAction="http://tempuri.org/IContracts/SubtractResponse")]
        double Subtract(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Subtract", ReplyAction="http://tempuri.org/IContracts/SubtractResponse")]
        System.Threading.Tasks.Task<double> SubtractAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Multiply", ReplyAction="http://tempuri.org/IContracts/MultiplyResponse")]
        double Multiply(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Multiply", ReplyAction="http://tempuri.org/IContracts/MultiplyResponse")]
        System.Threading.Tasks.Task<double> MultiplyAsync(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Divide", ReplyAction="http://tempuri.org/IContracts/DivideResponse")]
        double Divide(double x, double y);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IContracts/Divide", ReplyAction="http://tempuri.org/IContracts/DivideResponse")]
        System.Threading.Tasks.Task<double> DivideAsync(double x, double y);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IContractsChannel : SimpleCABApp.MyService.IContracts, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class ContractsClient : System.ServiceModel.ClientBase<SimpleCABApp.MyService.IContracts>, SimpleCABApp.MyService.IContracts {
        
        public ContractsClient() {
        }
        
        public ContractsClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public ContractsClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContractsClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public ContractsClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public double Add(double x, double y) {
            return base.Channel.Add(x, y);
        }
        
        public System.Threading.Tasks.Task<double> AddAsync(double x, double y) {
            return base.Channel.AddAsync(x, y);
        }
        
        public double Subtract(double x, double y) {
            return base.Channel.Subtract(x, y);
        }
        
        public System.Threading.Tasks.Task<double> SubtractAsync(double x, double y) {
            return base.Channel.SubtractAsync(x, y);
        }
        
        public double Multiply(double x, double y) {
            return base.Channel.Multiply(x, y);
        }
        
        public System.Threading.Tasks.Task<double> MultiplyAsync(double x, double y) {
            return base.Channel.MultiplyAsync(x, y);
        }
        
        public double Divide(double x, double y) {
            return base.Channel.Divide(x, y);
        }
        
        public System.Threading.Tasks.Task<double> DivideAsync(double x, double y) {
            return base.Channel.DivideAsync(x, y);
        }
    }
}
