//------------------------------------------------------------------------------
// <copyright file="ThrownExceptionsClientMessageInspector.cs" company="Servicarte">
// © Servicarte - Projet Expense
// </copyright>
//------------------------------------------------------------------------------

using System;
using System.IO;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;
using System.ServiceModel.Dispatcher;
using System.Xml;

namespace My.CoachManager.Services.Core.MessageInspectors
{
    /// <summary>
    /// Class to get thrown exceptions.
    /// </summary>
    public class ThrownExceptionsClientMessageInspector : IClientMessageInspector
    {
        #region ---- Implemented methods ----

        /// <summary>
        /// After receive the reply.
        /// </summary>
        /// <param name="reply">Message reply.</param>
        /// <param name="correlationState">Correlation state.</param>
        public void AfterReceiveReply(ref Message reply, object correlationState)
        {
            if (reply == null)
            {
                return;
            }

            var s = reply.ToString();
            if (s != null && (!reply.IsFault && (!Equals(reply.Version, MessageVersion.None) || !s.StartsWith("<Fault ", StringComparison.OrdinalIgnoreCase))))
            {
                return;
            }

            // Create a copy of the original reply to allow default WCF processing
            var buffer = reply.CreateBufferedCopy(Int32.MaxValue);

            // Create a copy to work with
            var copy = buffer.CreateMessage();

            // Restore the original message
            reply = buffer.CreateMessage();

            var faultDetail = ReadFaultDetail(copy);
            var exception = faultDetail as Exception;
            if (exception != null)
            {
                throw CreateNew(exception);
            }
        }

        /// <summary>
        /// Before send request.
        /// </summary>
        /// <param name="request">Message request.</param>
        /// <param name="channel">Message channel.</param>
        /// <returns>A message.</returns>
        public object BeforeSendRequest(ref Message request, System.ServiceModel.IClientChannel channel)
        {
            return null;
        }

        #endregion ---- Implemented methods ----

        #region ---- Private methods ----

        /// <summary>
        /// Read the fault detail.
        /// </summary>
        /// <param name="reply">Message reply.</param>
        /// <returns>A message.</returns>
        private static object ReadFaultDetail(Message reply)
        {
            const string detailElementName = "Detail";

            using (var reader = reply.GetReaderAtBodyContents())
            {
                // Find <soap:Detail>.
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element && reader.LocalName.Equals(detailElementName, StringComparison.OrdinalIgnoreCase))
                    {
                        break;
                    }
                }

                // Did we find it ? .
                if (reader.NodeType != XmlNodeType.Element || !reader.LocalName.Equals(detailElementName, StringComparison.OrdinalIgnoreCase))
                {
                    return null;
                }

                // Move to the contents of <soap:Detail>.
                if (!reader.Read())
                {
                    return null;
                }

                // Deserialize the fault.
                var serializer = new NetDataContractSerializer();
                try
                {
                    return serializer.ReadObject(reader);
                }
                catch (FileNotFoundException)
                {
                    // Serializer was unable to find assembly where exception is defined.
                    return null;
                }
            }
        }

        /// <summary>
        /// Create the new exception.
        /// </summary>
        /// <typeparam name="T">Type of exception.</typeparam>
        /// <param name="exception">The exception.</param>
        /// <returns>The new exception.</returns>
        private static T CreateNew<T>(T exception)
            where T : Exception, new()
        {
            return Activator.CreateInstance(exception.GetType(), exception.Message, exception) as T;
        }

        #endregion ---- Private methods ----
    }
}