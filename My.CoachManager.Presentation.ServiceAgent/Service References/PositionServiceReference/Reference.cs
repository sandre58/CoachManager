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
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetList", ReplyAction="http://tempuri.org/IPositionService/GetListResponse")]
        My.CoachManager.Application.Dtos.Administration.PositionDto[] GetList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetList", ReplyAction="http://tempuri.org/IPositionService/GetListResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto[]> GetListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetById", ReplyAction="http://tempuri.org/IPositionService/GetByIdResponse")]
        My.CoachManager.Application.Dtos.Administration.PositionDto GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/GetById", ReplyAction="http://tempuri.org/IPositionService/GetByIdResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto> GetByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/CreateOrUpdate", ReplyAction="http://tempuri.org/IPositionService/CreateOrUpdateResponse")]
        My.CoachManager.Application.Dtos.Administration.PositionDto CreateOrUpdate(My.CoachManager.Application.Dtos.Administration.PositionDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/CreateOrUpdate", ReplyAction="http://tempuri.org/IPositionService/CreateOrUpdateResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto> CreateOrUpdateAsync(My.CoachManager.Application.Dtos.Administration.PositionDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/Remove", ReplyAction="http://tempuri.org/IPositionService/RemoveResponse")]
        void Remove(My.CoachManager.Application.Dtos.Administration.PositionDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/Remove", ReplyAction="http://tempuri.org/IPositionService/RemoveResponse")]
        System.Threading.Tasks.Task RemoveAsync(My.CoachManager.Application.Dtos.Administration.PositionDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/UpdateOrders", ReplyAction="http://tempuri.org/IPositionService/UpdateOrdersResponse")]
        void UpdateOrders(System.Collections.Generic.Dictionary<int, int> entities);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPositionService/UpdateOrders", ReplyAction="http://tempuri.org/IPositionService/UpdateOrdersResponse")]
        System.Threading.Tasks.Task UpdateOrdersAsync(System.Collections.Generic.Dictionary<int, int> entities);
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
        
        public My.CoachManager.Application.Dtos.Administration.PositionDto[] GetList() {
            return base.Channel.GetList();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto[]> GetListAsync() {
            return base.Channel.GetListAsync();
        }
        
        public My.CoachManager.Application.Dtos.Administration.PositionDto GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto> GetByIdAsync(int id) {
            return base.Channel.GetByIdAsync(id);
        }
        
        public My.CoachManager.Application.Dtos.Administration.PositionDto CreateOrUpdate(My.CoachManager.Application.Dtos.Administration.PositionDto dto) {
            return base.Channel.CreateOrUpdate(dto);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.PositionDto> CreateOrUpdateAsync(My.CoachManager.Application.Dtos.Administration.PositionDto dto) {
            return base.Channel.CreateOrUpdateAsync(dto);
        }
        
        public void Remove(My.CoachManager.Application.Dtos.Administration.PositionDto dto) {
            base.Channel.Remove(dto);
        }
        
        public System.Threading.Tasks.Task RemoveAsync(My.CoachManager.Application.Dtos.Administration.PositionDto dto) {
            return base.Channel.RemoveAsync(dto);
        }
        
        public void UpdateOrders(System.Collections.Generic.Dictionary<int, int> entities) {
            base.Channel.UpdateOrders(entities);
        }
        
        public System.Threading.Tasks.Task UpdateOrdersAsync(System.Collections.Generic.Dictionary<int, int> entities) {
            return base.Channel.UpdateOrdersAsync(entities);
        }
    }
}
