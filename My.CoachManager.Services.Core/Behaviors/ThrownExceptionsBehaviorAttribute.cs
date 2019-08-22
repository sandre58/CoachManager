//------------------------------------------------------------------------------
// <copyright file="ThrownExceptionsBehaviorAttribute.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Linq;
using System.ServiceModel.Channels;
using System.ServiceModel.Description;
using System.ServiceModel.Dispatcher;

using My.CoachManager.Services.Core.ErrorHandlers;
using My.CoachManager.Services.Core.MessageInspectors;

namespace My.CoachManager.Services.Core.Behaviors
{
    /// <summary>
    /// Services to manage throw exception behavior.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class ThrownExceptionsBehaviorAttribute : Attribute, IServiceBehavior, IEndpointBehavior, IContractBehavior
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ThrownExceptionsBehaviorAttribute" /> class.
        /// </summary>
        /// <param name="enabled">Enabled value.</param>
        public ThrownExceptionsBehaviorAttribute(bool enabled)
        {
            Enabled = enabled;
        }

        /// <summary>
        /// Gets a value indicating whether the value is enabled.
        /// </summary>
        public bool Enabled { get; private set; }

        #region ServiceBehavior members

        /// <summary>
        /// Add binding parameters.
        /// </summary>
        /// <param name="serviceDescription">Service description.</param>
        /// <param name="serviceHostBase">Service host base.</param>
        /// <param name="endpoints">End points.</param>
        /// <param name="bindingParameters">Binding parameters.</param>
        public void AddBindingParameters(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase, System.Collections.ObjectModel.Collection<ServiceEndpoint> endpoints, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Apply dispatch behavior.
        /// </summary>
        /// <param name="serviceDescription">Service description.</param>
        /// <param name="serviceHostBase">Service host base.</param>
        public void ApplyDispatchBehavior(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
            Trace.TraceInformation("Applying dispatch ExceptionMarshallingBehavior to service {0}", serviceDescription.ServiceType.FullName);
            foreach (var channelDispatcherBase in serviceHostBase.ChannelDispatchers)
            {
                var dispatcher = (ChannelDispatcher)channelDispatcherBase;
                ApplyDispatchBehavior(dispatcher);
            }
        }

        /// <summary>
        /// Validate method.
        /// </summary>
        /// <param name="serviceDescription">Service description.</param>
        /// <param name="serviceHostBase">Service host base.</param>
        public void Validate(ServiceDescription serviceDescription, System.ServiceModel.ServiceHostBase serviceHostBase)
        {
        }

        #endregion ServiceBehavior members

        #region EndpointBehavior members

        /// <summary>
        /// Add binding parameters.
        /// </summary>
        /// <param name="endpoint">End point.</param>
        /// <param name="bindingParameters">Binding parameters.</param>
        public void AddBindingParameters(ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Apply client behavior.
        /// </summary>
        /// <param name="endpoint">End point.</param>
        /// <param name="clientRuntime">Client runtime.</param>
        public void ApplyClientBehavior(ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Trace.TraceInformation("Applying client ExceptionMarshallingBehavior to endpoint {0}", endpoint.Address);
            ApplyClientBehavior(clientRuntime);
        }

        /// <summary>
        /// Apply dispatch behavior.
        /// </summary>
        /// <param name="endpoint">End point.</param>
        /// <param name="endpointDispatcher">Client runtime.</param>
        public void ApplyDispatchBehavior(ServiceEndpoint endpoint, EndpointDispatcher endpointDispatcher)
        {
            Trace.TraceInformation("Applying dispatch ExceptionMarshallingBehavior to endpoint {0}", endpoint.Address);
            ApplyDispatchBehavior(endpointDispatcher.ChannelDispatcher);
        }

        /// <summary>
        /// Validate method.
        /// </summary>
        /// <param name="endpoint">End point.</param>
        public void Validate(ServiceEndpoint endpoint)
        {
        }

        #endregion EndpointBehavior members

        #region ContractBehavior members

        /// <summary>
        /// Add binding parameters.
        /// </summary>
        /// <param name="contractDescription">Contract description.</param>
        /// <param name="endpoint">End point.</param>
        /// <param name="bindingParameters">Binding parameters.</param>
        public void AddBindingParameters(ContractDescription contractDescription, ServiceEndpoint endpoint, BindingParameterCollection bindingParameters)
        {
        }

        /// <summary>
        /// Apply client behavior.
        /// </summary>
        /// <param name="contractDescription">Contract description.</param>
        /// <param name="endpoint">End point.</param>
        /// <param name="clientRuntime">Client runtime.</param>
        public void ApplyClientBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, ClientRuntime clientRuntime)
        {
            Trace.TraceInformation("Applying client ExceptionMarshallingBehavior to contract {0}", contractDescription.ContractType);
            ApplyClientBehavior(clientRuntime);
        }

        /// <summary>
        /// Apply dispatcher behavior.
        /// </summary>
        /// <param name="contractDescription">Contract description.</param>
        /// <param name="endpoint">End point.</param>
        /// <param name="dispatchRuntime">Dispatch runtime.</param>
        public void ApplyDispatchBehavior(ContractDescription contractDescription, ServiceEndpoint endpoint, DispatchRuntime dispatchRuntime)
        {
            Trace.TraceInformation("Applying dispatch ExceptionMarshallingBehavior to contract {0}", contractDescription.ContractType.FullName);
            ApplyDispatchBehavior(dispatchRuntime.ChannelDispatcher);
        }

        /// <summary>
        /// Validate method.
        /// </summary>
        /// <param name="contractDescription">Contract description.</param>
        /// <param name="endpoint">End point.</param>
        public void Validate(ContractDescription contractDescription, ServiceEndpoint endpoint)
        {
        }

        #endregion ContractBehavior members

        #region --- Private methods ---

        /// <summary>
        /// Apply client behavior.
        /// </summary>
        /// <param name="clientRuntime">Runtime client.</param>
        private void ApplyClientBehavior(ClientRuntime clientRuntime)
        {
            if (!Enabled)
            {
                return;
            }

            // don't add a message inspector if it already exists
            if (clientRuntime.MessageInspectors.OfType<ThrownExceptionsClientMessageInspector>().Any())
            {
                return;
            }

            clientRuntime.MessageInspectors.Add(new ThrownExceptionsClientMessageInspector());
        }

        /// <summary>
        /// Apply dispatch behavior.
        /// </summary>
        /// <param name="channelDispatcher">Channel dispatcher.</param>
        private void ApplyDispatchBehavior(ChannelDispatcher channelDispatcher)
        {
            if (!Enabled)
            {
                return;
            }

            // Don't add an error handler if it already exists
            if (channelDispatcher.ErrorHandlers.OfType<ThrownExceptionsErrorHandler>().Any())
            {
                return;
            }

            channelDispatcher.ErrorHandlers.Clear();
            channelDispatcher.ErrorHandlers.Add(new ThrownExceptionsErrorHandler());
        }

        #endregion --- Private methods ---
    }
}
