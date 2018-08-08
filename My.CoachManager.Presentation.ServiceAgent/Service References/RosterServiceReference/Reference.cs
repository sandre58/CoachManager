﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.ServiceAgent.RosterServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="RosterServiceReference.IRosterService")]
    public interface IRosterService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/GetRosters", ReplyAction="http://tempuri.org/IRosterService/GetRostersResponse")]
        My.CoachManager.Application.Dtos.Roster.RosterDto[] GetRosters();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/GetRosters", ReplyAction="http://tempuri.org/IRosterService/GetRostersResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto[]> GetRostersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/SaveRoster", ReplyAction="http://tempuri.org/IRosterService/SaveRosterResponse")]
        My.CoachManager.Application.Dtos.Roster.RosterDto SaveRoster(My.CoachManager.Application.Dtos.Roster.RosterDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/SaveRoster", ReplyAction="http://tempuri.org/IRosterService/SaveRosterResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto> SaveRosterAsync(My.CoachManager.Application.Dtos.Roster.RosterDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/RemoveRoster", ReplyAction="http://tempuri.org/IRosterService/RemoveRosterResponse")]
        void RemoveRoster(My.CoachManager.Application.Dtos.Roster.RosterDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/RemoveRoster", ReplyAction="http://tempuri.org/IRosterService/RemoveRosterResponse")]
        System.Threading.Tasks.Task RemoveRosterAsync(My.CoachManager.Application.Dtos.Roster.RosterDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/GetRosterById", ReplyAction="http://tempuri.org/IRosterService/GetRosterByIdResponse")]
        My.CoachManager.Application.Dtos.Roster.RosterDto GetRosterById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IRosterService/GetRosterById", ReplyAction="http://tempuri.org/IRosterService/GetRosterByIdResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto> GetRosterByIdAsync(int id);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IRosterServiceChannel : My.CoachManager.Presentation.ServiceAgent.RosterServiceReference.IRosterService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class RosterServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.RosterServiceReference.IRosterService>, My.CoachManager.Presentation.ServiceAgent.RosterServiceReference.IRosterService {
        
        public RosterServiceClient() {
        }
        
        public RosterServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public RosterServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RosterServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public RosterServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public My.CoachManager.Application.Dtos.Roster.RosterDto[] GetRosters() {
            return base.Channel.GetRosters();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto[]> GetRostersAsync() {
            return base.Channel.GetRostersAsync();
        }
        
        public My.CoachManager.Application.Dtos.Roster.RosterDto SaveRoster(My.CoachManager.Application.Dtos.Roster.RosterDto dto) {
            return base.Channel.SaveRoster(dto);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto> SaveRosterAsync(My.CoachManager.Application.Dtos.Roster.RosterDto dto) {
            return base.Channel.SaveRosterAsync(dto);
        }
        
        public void RemoveRoster(My.CoachManager.Application.Dtos.Roster.RosterDto dto) {
            base.Channel.RemoveRoster(dto);
        }
        
        public System.Threading.Tasks.Task RemoveRosterAsync(My.CoachManager.Application.Dtos.Roster.RosterDto dto) {
            return base.Channel.RemoveRosterAsync(dto);
        }
        
        public My.CoachManager.Application.Dtos.Roster.RosterDto GetRosterById(int id) {
            return base.Channel.GetRosterById(id);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.Roster.RosterDto> GetRosterByIdAsync(int id) {
            return base.Channel.GetRosterByIdAsync(id);
        }
    }
}
