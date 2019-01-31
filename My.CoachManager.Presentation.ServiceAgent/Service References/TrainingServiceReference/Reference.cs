﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference {
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="TrainingServiceReference.ITrainingService")]
    public interface ITrainingService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetTrainings", ReplyAction="http://tempuri.org/ITrainingService/GetTrainingsResponse")]
        My.CoachManager.Application.Dtos.TrainingDto[] GetTrainings(int rosterId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetTrainings", ReplyAction="http://tempuri.org/ITrainingService/GetTrainingsResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto[]> GetTrainingsAsync(int rosterId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/SaveTraining", ReplyAction="http://tempuri.org/ITrainingService/SaveTrainingResponse")]
        int SaveTraining(int rosterId, My.CoachManager.Application.Dtos.TrainingDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/SaveTraining", ReplyAction="http://tempuri.org/ITrainingService/SaveTrainingResponse")]
        System.Threading.Tasks.Task<int> SaveTrainingAsync(int rosterId, My.CoachManager.Application.Dtos.TrainingDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/RemoveTraining", ReplyAction="http://tempuri.org/ITrainingService/RemoveTrainingResponse")]
        void RemoveTraining(My.CoachManager.Application.Dtos.TrainingDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/RemoveTraining", ReplyAction="http://tempuri.org/ITrainingService/RemoveTrainingResponse")]
        System.Threading.Tasks.Task RemoveTrainingAsync(My.CoachManager.Application.Dtos.TrainingDto dto);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetTrainingById", ReplyAction="http://tempuri.org/ITrainingService/GetTrainingByIdResponse")]
        My.CoachManager.Application.Dtos.TrainingDto GetTrainingById(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetTrainingById", ReplyAction="http://tempuri.org/ITrainingService/GetTrainingByIdResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto> GetTrainingByIdAsync(int id);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/AddTrainings", ReplyAction="http://tempuri.org/ITrainingService/AddTrainingsResponse")]
        My.CoachManager.Application.Dtos.TrainingDto[] AddTrainings(int rosterId, System.DateTime startDate, System.DateTime endDate, System.TimeSpan startTime, System.TimeSpan endTime, string place, System.DayOfWeek[] days);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/AddTrainings", ReplyAction="http://tempuri.org/ITrainingService/AddTrainingsResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto[]> AddTrainingsAsync(int rosterId, System.DateTime startDate, System.DateTime endDate, System.TimeSpan startTime, System.TimeSpan endTime, string place, System.DayOfWeek[] days);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/SaveTrainingAttendances", ReplyAction="http://tempuri.org/ITrainingService/SaveTrainingAttendancesResponse")]
        int SaveTrainingAttendances(int trainingId, My.CoachManager.Application.Dtos.TrainingAttendanceDto[] attendances);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/SaveTrainingAttendances", ReplyAction="http://tempuri.org/ITrainingService/SaveTrainingAttendancesResponse")]
        System.Threading.Tasks.Task<int> SaveTrainingAttendancesAsync(int trainingId, My.CoachManager.Application.Dtos.TrainingAttendanceDto[] attendances);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetPlayersForTraining", ReplyAction="http://tempuri.org/ITrainingService/GetPlayersForTrainingResponse")]
        My.CoachManager.Application.Dtos.RosterPlayerDto[] GetPlayersForTraining(int trainingId);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ITrainingService/GetPlayersForTraining", ReplyAction="http://tempuri.org/ITrainingService/GetPlayersForTrainingResponse")]
        System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.RosterPlayerDto[]> GetPlayersForTrainingAsync(int trainingId);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ITrainingServiceChannel : My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference.ITrainingService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class TrainingServiceClient : System.ServiceModel.ClientBase<My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference.ITrainingService>, My.CoachManager.Presentation.ServiceAgent.TrainingServiceReference.ITrainingService {
        
        public TrainingServiceClient() {
        }
        
        public TrainingServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public TrainingServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TrainingServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public TrainingServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public My.CoachManager.Application.Dtos.TrainingDto[] GetTrainings(int rosterId) {
            return base.Channel.GetTrainings(rosterId);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto[]> GetTrainingsAsync(int rosterId) {
            return base.Channel.GetTrainingsAsync(rosterId);
        }
        
        public int SaveTraining(int rosterId, My.CoachManager.Application.Dtos.TrainingDto dto) {
            return base.Channel.SaveTraining(rosterId, dto);
        }
        
        public System.Threading.Tasks.Task<int> SaveTrainingAsync(int rosterId, My.CoachManager.Application.Dtos.TrainingDto dto) {
            return base.Channel.SaveTrainingAsync(rosterId, dto);
        }
        
        public void RemoveTraining(My.CoachManager.Application.Dtos.TrainingDto dto) {
            base.Channel.RemoveTraining(dto);
        }
        
        public System.Threading.Tasks.Task RemoveTrainingAsync(My.CoachManager.Application.Dtos.TrainingDto dto) {
            return base.Channel.RemoveTrainingAsync(dto);
        }
        
        public My.CoachManager.Application.Dtos.TrainingDto GetTrainingById(int id) {
            return base.Channel.GetTrainingById(id);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto> GetTrainingByIdAsync(int id) {
            return base.Channel.GetTrainingByIdAsync(id);
        }
        
        public My.CoachManager.Application.Dtos.TrainingDto[] AddTrainings(int rosterId, System.DateTime startDate, System.DateTime endDate, System.TimeSpan startTime, System.TimeSpan endTime, string place, System.DayOfWeek[] days) {
            return base.Channel.AddTrainings(rosterId, startDate, endDate, startTime, endTime, place, days);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.TrainingDto[]> AddTrainingsAsync(int rosterId, System.DateTime startDate, System.DateTime endDate, System.TimeSpan startTime, System.TimeSpan endTime, string place, System.DayOfWeek[] days) {
            return base.Channel.AddTrainingsAsync(rosterId, startDate, endDate, startTime, endTime, place, days);
        }
        
        public int SaveTrainingAttendances(int trainingId, My.CoachManager.Application.Dtos.TrainingAttendanceDto[] attendances) {
            return base.Channel.SaveTrainingAttendances(trainingId, attendances);
        }
        
        public System.Threading.Tasks.Task<int> SaveTrainingAttendancesAsync(int trainingId, My.CoachManager.Application.Dtos.TrainingAttendanceDto[] attendances) {
            return base.Channel.SaveTrainingAttendancesAsync(trainingId, attendances);
        }
        
        public My.CoachManager.Application.Dtos.RosterPlayerDto[] GetPlayersForTraining(int trainingId) {
            return base.Channel.GetPlayersForTraining(trainingId);
        }
        
        public System.Threading.Tasks.Task<My.CoachManager.Application.Dtos.RosterPlayerDto[]> GetPlayersForTrainingAsync(int trainingId) {
            return base.Channel.GetPlayersForTrainingAsync(trainingId);
        }
    }
}
