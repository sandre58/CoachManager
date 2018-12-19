﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.ServiceAgent.PersonServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="PersonServiceReference.IPersonService")]
    public interface IPersonService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetCountries", ReplyAction="http://tempuri.org/IPersonService/GetCountriesResponse")]
        My.CoachManager.Application.Dtos.CountryDto[] GetCountries();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetCountries", ReplyAction="http://tempuri.org/IPersonService/GetCountriesResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.CountryDto[]> GetCountriesAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPlayers", ReplyAction="http://tempuri.org/IPersonService/GetPlayersResponse")]
        My.CoachManager.Application.Dtos.PlayerDto[] GetPlayers();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPlayers", ReplyAction="http://tempuri.org/IPersonService/GetPlayersResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PlayerDto[]> GetPlayersAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPlayerById", ReplyAction="http://tempuri.org/IPersonService/GetPlayerByIdResponse")]
        My.CoachManager.Application.Dtos.PlayerDto GetPlayerById(int playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetPlayerById", ReplyAction="http://tempuri.org/IPersonService/GetPlayerByIdResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PlayerDto> GetPlayerByIdAsync(int playerId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/SavePlayer", ReplyAction="http://tempuri.org/IPersonService/SavePlayerResponse")]
        int SavePlayer(My.CoachManager.Application.Dtos.PlayerDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/SavePlayer", ReplyAction="http://tempuri.org/IPersonService/SavePlayerResponse")]
        System.Threading.Tasks.Task<int> SavePlayerAsync(My.CoachManager.Application.Dtos.PlayerDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/RemovePlayer", ReplyAction="http://tempuri.org/IPersonService/RemovePlayerResponse")]
        void RemovePlayer(My.CoachManager.Application.Dtos.PlayerDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/RemovePlayer", ReplyAction="http://tempuri.org/IPersonService/RemovePlayerResponse")]
        System.Threading.Tasks.Task RemovePlayerAsync(My.CoachManager.Application.Dtos.PlayerDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetCategoryFromBirthdate", ReplyAction="http://tempuri.org/IPersonService/GetCategoryFromBirthdateResponse")]
        My.CoachManager.Application.Dtos.CategoryDto GetCategoryFromBirthdate(System.DateTime date);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IPersonService/GetCategoryFromBirthdate", ReplyAction="http://tempuri.org/IPersonService/GetCategoryFromBirthdateResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.CategoryDto> GetCategoryFromBirthdateAsync(System.DateTime date);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IPersonServiceChannel : My.CoachManager.Presentation.ServiceAgent.PersonServiceReference.IPersonService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PersonServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.PersonServiceReference.IPersonService>, My.CoachManager.Presentation.ServiceAgent.PersonServiceReference.IPersonService {
        
        public PersonServiceClient() {
        }
        
        public PersonServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PersonServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public My.CoachManager.Application.Dtos.CountryDto[] GetCountries() {
            return base.Channel.GetCountries();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.CountryDto[]> GetCountriesAsync() {
            return base.Channel.GetCountriesAsync();
        }
        
        public My.CoachManager.Application.Dtos.PlayerDto[] GetPlayers() {
            return base.Channel.GetPlayers();
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PlayerDto[]> GetPlayersAsync() {
            return base.Channel.GetPlayersAsync();
        }
        
        public My.CoachManager.Application.Dtos.PlayerDto GetPlayerById(int playerId) {
            return base.Channel.GetPlayerById(playerId);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.PlayerDto> GetPlayerByIdAsync(int playerId) {
            return base.Channel.GetPlayerByIdAsync(playerId);
        }
        
        public int SavePlayer(My.CoachManager.Application.Dtos.PlayerDto dto) {
            return base.Channel.SavePlayer(dto);
        }
        
        public System.Threading.Tasks.Task<int> SavePlayerAsync(My.CoachManager.Application.Dtos.PlayerDto dto) {
            return base.Channel.SavePlayerAsync(dto);
        }
        
        public void RemovePlayer(My.CoachManager.Application.Dtos.PlayerDto dto) {
            base.Channel.RemovePlayer(dto);
        }
        
        public System.Threading.Tasks.Task RemovePlayerAsync(My.CoachManager.Application.Dtos.PlayerDto dto) {
            return base.Channel.RemovePlayerAsync(dto);
        }
        
        public My.CoachManager.Application.Dtos.CategoryDto GetCategoryFromBirthdate(System.DateTime date) {
            return base.Channel.GetCategoryFromBirthdate(date);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.CategoryDto> GetCategoryFromBirthdateAsync(System.DateTime date) {
            return base.Channel.GetCategoryFromBirthdateAsync(date);
        }
    }
}
