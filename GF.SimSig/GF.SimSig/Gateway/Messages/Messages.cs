using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Messages
{
    internal static class Messages
    {
        internal static MessageType GetMessageType(string messageTypeName)
        {
            switch (messageTypeName.ToLower())
            {
                case "clock_msg":
                    return MessageType.ClockMessage;
                case "ca_msg":
                    return MessageType.TrainDescriberBerthStepMessage;
                case "cb_msg":
                    return MessageType.TrainDescriberBerthCancelMessage;
                case "cc_msg":
                    return MessageType.TrainDescriberBerthInterposeMessage;
                default:
                    throw new NotImplementedException($"The message type {messageTypeName} has not have a {nameof(MessageType)} implemented.");
            }
        }
    }
}
