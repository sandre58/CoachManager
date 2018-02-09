﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SeasonServiceReference.ISeasonService")]
    public interface ISeasonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/GetList", ReplyAction="http://tempuri.org/ISeasonService/GetListResponse")]
        My.CoachManager.Application.Dtos.Administration.SeasonDto[] GetList();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/GetList", ReplyAction="http://tempuri.org/ISeasonService/GetListResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto[]> GetListAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/GetById", ReplyAction="http://tempuri.org/ISeasonService/GetByIdResponse")]
        My.CoachManager.Application.Dtos.Administration.SeasonDto GetById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/GetById", ReplyAction="http://tempuri.org/ISeasonService/GetByIdResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto> GetByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/CreateOrUpdate", ReplyAction="http://tempuri.org/ISeasonService/CreateOrUpdateResponse")]
        My.CoachManager.Application.Dtos.Administration.SeasonDto CreateOrUpdate(My.CoachManager.Application.Dtos.Administration.SeasonDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/CreateOrUpdate", ReplyAction="http://tempuri.org/ISeasonService/CreateOrUpdateResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto> CreateOrUpdateAsync(My.CoachManager.Application.Dtos.Administration.SeasonDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/Remove", ReplyAction="http://tempuri.org/ISeasonService/RemoveResponse")]
        void Remove(My.CoachManager.Application.Dtos.Administration.SeasonDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/Remove", ReplyAction="http://tempuri.org/ISeasonService/RemoveResponse")]
        System.Threading.Tasks.Task RemoveAsync(My.CoachManager.Application.Dtos.Administration.SeasonDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/UpdateOrders", ReplyAction="http://tempuri.org/ISeasonService/UpdateOrdersResponse")]
        void UpdateOrders(System.Collections.Generic.Dictionary<int, int> entities);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISeasonService/UpdateOrders", ReplyAction="http://tempuri.org/ISeasonService/UpdateOrdersResponse")]
        System.Threading.Tasks.Task UpdateOrdersAsync(System.Collections.Generic.Dictionary<int, int> entities);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISeasonServiceChannel : My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference.ISeasonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SeasonServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference.ISeasonService>, My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference.ISeasonService {
        
        public SeasonServiceClient() {
        }
        
        public SeasonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SeasonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeasonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SeasonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public My.CoachManager.Application.Dtos.Administration.SeasonDto[] GetList() {
            return base.Channel.GetList();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto[]> GetListAsync() {
            return base.Channel.GetListAsync();
        }
        
        public My.CoachManager.Application.Dtos.Administration.SeasonDto GetById(int id) {
            return base.Channel.GetById(id);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto> GetByIdAsync(int id) {
            return base.Channel.GetByIdAsync(id);
        }
        
        public My.CoachManager.Application.Dtos.Administration.SeasonDto CreateOrUpdate(My.CoachManager.Application.Dtos.Administration.SeasonDto dto) {
            return base.Channel.CreateOrUpdate(dto);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Administration.SeasonDto> CreateOrUpdateAsync(My.CoachManager.Application.Dtos.Administration.SeasonDto dto) {
            return base.Channel.CreateOrUpdateAsync(dto);
        }
        
        public void Remove(My.CoachManager.Application.Dtos.Administration.SeasonDto dto) {
            base.Channel.Remove(dto);
        }
        
        public System.Threading.Tasks.Task RemoveAsync(My.CoachManager.Application.Dtos.Administration.SeasonDto dto) {
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
