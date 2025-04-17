using GF.Common;
using Newtonsoft.Json.Linq;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("GF.SimSig.UnitTests")]
namespace GF.SimSig.Gateway.Messages
{
    public class BerthInterposeMessage : BerthMessage, IReceivedMessage
    {
        /// <summary>
        /// Instantiates a new <see cref="BerthInterposeMessage"/> object from the supplied <see cref="JObject"/>
        /// </summary>
        /// <param name="messageObject">The source <see cref="JObject"/>. Usually parsed from a <see cref="ReceivedMessage"/> object</param>
        /// <param name="messageType">The <see cref="MessageType"/></param>
        /// <param name="callingMethod">The name of the calling method. Do not pass.</param>
        /// <exception cref="ArgumentNullException">Thown if the messageObject is null</exception>
        /// <exception cref="GFException">Throw if the messageObject does not contain one of the expected properties</exception>
        public BerthInterposeMessage(JObject messageObject, MessageType messageType, [CallerMemberName] string? callingMethod = null) : base(messageObject, messageType, callingMethod) { }

        /// <summary>
        /// Instantiates a new <see cref="BerthInterposeMessage"/>. Should only be used for testing
        /// </summary>
        /// <param name="areaId">The simulation area</param>
        /// <param name="from">The from berth Id</param>
        /// <param name="to">The to berth Id</param>
        /// <param name="description">The berth description</param>
        /// <param name="berthMessageType">The berth message type</param>
        /// <param name="time">The time the message was sent</param>
        /// <param name="callingMethod">The calling method name. Do not pass.</param>
        internal BerthInterposeMessage(string areaId, string from, string to, string description, string berthMessageType, TimeSpan time, [CallerMemberName] string? callingMethod = null) : base (areaId, from, to, description, berthMessageType, time, callingMethod) { }
    }
}
