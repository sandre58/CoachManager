﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using My.CoachManager.Application.Dtos.User;

namespace My.CoachManager.Presentation.ServiceAgent.UserServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="UserServiceReference.IUserService")]
    public interface IUserService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserById", ReplyAction="http://tempuri.org/IUserService/GetUserByIdResponse")]
        UserDto GetUserById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserById", ReplyAction="http://tempuri.org/IUserService/GetUserByIdResponse")]
        System.Threading.Tasks.Task<UserDto> GetUserByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByLoginAndPassword", ReplyAction="http://tempuri.org/IUserService/GetUserByLoginAndPasswordResponse")]
        UserDto GetUserByLoginAndPassword(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByLoginAndPassword", ReplyAction="http://tempuri.org/IUserService/GetUserByLoginAndPasswordResponse")]
        System.Threading.Tasks.Task<UserDto> GetUserByLoginAndPasswordAsync(string login, string password);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByLogin", ReplyAction="http://tempuri.org/IUserService/GetUserByLoginResponse")]
        UserDto GetUserByLogin(string login);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IUserService/GetUserByLogin", ReplyAction="http://tempuri.org/IUserService/GetUserByLoginResponse")]
        System.Threading.Tasks.Task<UserDto> GetUserByLoginAsync(string login);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IUserServiceChannel : My.CoachManager.Presentation.ServiceAgent.UserServiceReference.IUserService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class UserServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.UserServiceReference.IUserService>, My.CoachManager.Presentation.ServiceAgent.UserServiceReference.IUserService {
        
        public UserServiceClient() {
        }
        
        public UserServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public UserServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public UserServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public UserDto GetUserById(int id) {
            return base.Channel.GetUserById(id);
        }
        
        public System.Threading.Tasks.Task<UserDto> GetUserByIdAsync(int id) {
            return base.Channel.GetUserByIdAsync(id);
        }
        
        public UserDto GetUserByLoginAndPassword(string login, string password) {
            return base.Channel.GetUserByLoginAndPassword(login, password);
        }
        
        public System.Threading.Tasks.Task<UserDto> GetUserByLoginAndPasswordAsync(string login, string password) {
            return base.Channel.GetUserByLoginAndPasswordAsync(login, password);
        }
        
        public UserDto GetUserByLogin(string login) {
            return base.Channel.GetUserByLogin(login);
        }
        
        public System.Threading.Tasks.Task<UserDto> GetUserByLoginAsync(string login) {
            return base.Channel.GetUserByLoginAsync(login);
        }
    }
}
