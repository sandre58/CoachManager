//------------------------------------------------------------------------------
// <copyright file="ThrownExceptionsErrorHandler.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using CommonServiceLocator;
using My.CoachManager.CrossCutting.Logging;

namespace My.CoachManager.Services.Core.ErrorHandlers
{
    /// <summary>
    /// Handler for thrown exceptions.
    /// </summary>
    public class ThrownExceptionsErrorHandler : IErrorHandler
    {
        /// <summary>
        /// Returns a value indicating whether the error is handled.
        /// </summary>
        /// <param name="error">Exception error.</param>
        /// <returns>True or false.</returns>
        public bool HandleError(Exception error)
        {
            // Fault message is already generated or Let WCF do normal processing
            return !(error is FaultException);
        }

        /// <summary>
        /// Provide fault.
        /// </summary>
        /// <param name="error">Exception error.</param>
        /// <param name="version">Message version.</param>
        /// <param name="fault">Message fault.</param>
        public void ProvideFault(Exception error, MessageVersion version, ref Message fault)
        {
            if (error is FaultException)
            {
                return; // Let WCF do normal processing
            }

            // Generate fault message manually
            MessageFault msgFault = MessageFault.CreateFault(
                new FaultCode("Sender..."), new FaultReason(error.Message), error, new NetDataContractSerializer());

            fault = Message.CreateMessage(version, msgFault, null);

            ServiceLocator.Current.GetInstance<ILogger>().Error(error);
        }
    }
}