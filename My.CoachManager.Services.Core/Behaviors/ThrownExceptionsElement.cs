//------------------------------------------------------------------------------
// <copyright file="ThrownExceptionsElement.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.Configuration;
using System.ServiceModel.Configuration;

namespace My.CoachManager.Services.Core.Behaviors
{
    /// <summary>
    /// Class representing a thrown exception.
    /// </summary>
    public class ThrownExceptionsElement : BehaviorExtensionElement
    {
        /// <summary>
        /// Gets or sets a value indicating whether the value is enable.
        /// </summary>
        /// <value>True or False.</value>
        [ConfigurationProperty("enabled", DefaultValue = true, IsRequired = false)]
        public bool Enabled
        {
            get { return (bool)base["enabled"]; }
            set { base["enabled"] = value; }
        }

        /// <summary>
        /// Gets the behavior type.
        /// </summary>
        /// <value>The behavior type.</value>
        public override Type BehaviorType
        {
            get { return typeof(ThrownExceptionsBehaviorAttribute); }
        }

        /// <summary>
        /// Creates behavior.
        /// </summary>
        /// <returns>The thrown exceptions behavior.</returns>
        protected override object CreateBehavior()
        {
            return new ThrownExceptionsBehaviorAttribute(Enabled);
        }
    }
}
