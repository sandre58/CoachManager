﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.ServiceAgent.PositionServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PositionServiceReference.IPositionService")]
    public interface IPositionService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetPositions", ReplyAction="http://tempuri.org/IPositionService/GetPositionsResponse")]
        My.CoachManager.Application.Dtos.PositionDto[] GetPositions();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetPositions", ReplyAction="http://tempuri.org/IPositionService/GetPositionsResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PositionDto[]> GetPositionsAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPositionServiceChannel : My.CoachManager.Presentation.ServiceAgent.PositionServiceReference.IPositionService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PositionServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.PositionServiceReference.IPositionService>, My.CoachManager.Presentation.ServiceAgent.PositionServiceReference.IPositionService {
        
        public PositionServiceClient() {
        }
        
        public PositionServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PositionServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PositionServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PositionServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public My.CoachManager.Application.Dtos.PositionDto[] GetPositions() {
            return base.Channel.GetPositions();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PositionDto[]> GetPositionsAsync() {
            return base.Channel.GetPositionsAsync();
        }
    }
}