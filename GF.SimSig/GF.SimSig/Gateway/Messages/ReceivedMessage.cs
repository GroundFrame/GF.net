using GF.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO.Pipes;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Messages
{
    public class ReceivedMessage
    {
        public string JSONMessage { get; private set; }

        public JObject MessageObject { get; private set; }

        public MessageType MessageType { get; private set; }

        public IReceivedMessage Message { get; private set; }

        public ReceivedMessage(string jsonMessage, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(jsonMessage))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(jsonMessage), callingMethod), "You must a SimSig Gateway JSON message");
            }

            this.JSONMessage = jsonMessage;
            this.MessageObject = JObject.Parse(jsonMessage);
            this.Message = this.ParseMessage();
        }

        /// <summary>
        /// Parses the message to set the <see cref="this.MessageType"/> property and build the <see cref="this.Message"/> object
        /// </summary>
        /// <exception cref="GFException">Thrown if the <see cref="this.MessageObject"/> property is null</exception>
        private IReceivedMessage ParseMessage()
        {
            if (MessageObject == null)
            {
                throw GFException.Build("The MessageObject property cannot be null", "GF.SimSig.Gateway.Messages.MessageObject.Null.Error");
            }

            //parse the message type
            this.ParseMessageType();

            //build the message type and return
            return this.MessageType switch
            {
                MessageType.ClockMessage => new ClockMessage(this.MessageObject),
                MessageType.TrainDescriberBerthStepMessage => new BerthStepMessage(this.MessageObject, this.MessageType),
                MessageType.TrainDescriberBerthCancelMessage => new BerthCancelMessage(this.MessageObject, this.MessageType),
                MessageType.TrainDescriberBerthInterposeMessage => new BerthInterposeMessage(this.MessageObject, this.MessageType),
                _ => throw new NotImplementedException($"The Message Type {Enum.GetName(typeof(MessageType), this.MessageType)} does not have an IMessage class implemented."),
            }; ;
        }

        /// <summary>
        /// Parses the message object to determine the <see cref="ReceivedMessage.MessageType"/>
        /// </summary>
        /// <exception cref="GFException">Thrown if the <see cref="this.MessageObject"/> property is null</exception>
        private void ParseMessageType()
        {
            if (MessageObject == null)
            {
                throw GFException.Build("The MessageObject property cannot be null", "GF.SimSig.Gateway.Messages.MessageObject.Null.Error");
            }

            this.MessageType = Messages.GetMessageType(this.MessageObject.Properties().ToList()[0].Name);
        }
    }
}
