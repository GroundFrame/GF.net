using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Messages
{
    /// <summary>
    /// Static class contain extension methods for the <see cref="MessageType"/> enum
    /// </summary>
    public static class Ext_MessageType
    {
        /// <summary>
        /// Returns the SimSig Gateway Message Type code for the supplied <see cref="MessageType"/>
        /// </summary>
        /// <param name="messageType">The <see cref="MessageType"/> to return the code fgor</param>
        /// <returns>A <see cref="string"/> representing the SimSig Gateway Berth Message type code</returns>
        /// <exception cref="NotImplementedException">If the supplied <see cref="MessageType"/> has not been implemented</exception>
        public static string GetMessageCode(this MessageType messageType)
        {
            switch (messageType)
            {
                case MessageType.ClockMessage:
                    return "clock_msg";
                case MessageType.TrainDescriberBerthStepMessage:
                    return "CA_MSG";
                case MessageType.TrainDescriberBerthCancelMessage:
                    return "CB_MSG";
                case MessageType.TrainDescriberBerthInterposeMessage:
                    return "CC_MSG";
                default:
                    throw new NotImplementedException($"The Message type {Enum.GetName(typeof(MessageType), messageType)} has not have a message code implemented.");
            }
        }
    }
}
