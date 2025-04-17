using GF.Common;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Messages
{
    public abstract class BerthMessage
    {
        /// <summary>
        /// Gets the simulation id. Note the format is the .sim file name
        /// </summary>
        public string AreaId { get; private set; }

        /// <summary>
        /// Gets the ID of the from berth
        /// </summary>
        public string From { get; private set; }

        /// <summary>
        /// Gets the ID of the to berth
        /// </summary>
        public string To { get; private set; }

        /// <summary>
        /// Gets the berth description
        /// </summary>
        public string Description { get; private set; }

        /// <summary>
        /// Gets the berth message type
        /// </summary>
        public string BerthMessageType { get; private set; }

        /// <summary>
        /// Gets the time the message was sent
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="ClockMessage"/> object from the supplied <see cref="JObject"/>
        /// </summary>
        /// <param name="messageObject">The source <see cref="JObject"/>. Usually parsed from a <see cref="ReceivedMessage"/> object</param>
        /// <param name="callingMethod">The name of the calling method. Do not pass.</param>
        /// <exception cref="ArgumentNullException">Thown if the messageObject is null</exception>
        /// <exception cref="GFException">Throw if the messageObject does not contain one of the expected properties</exception>
        public BerthMessage(JObject messageObject, MessageType berthMessageType, [CallerMemberName] string? callingMethod = null)
        {
            //validate paramaters
            if (messageObject == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(messageObject), callingMethod), "You must supply a message object");
            }

            //get the berth message type code
            string berthMessageTypeCode = berthMessageType.GetMessageCode();

            this.AreaId = messageObject.GetValue(berthMessageTypeCode)?.Value<string>("area_id") ?? throw GFException.Build<string, string>($"The {nameof(BerthMessage)} area_id property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(BerthMessage), "area_id");
            this.From = messageObject.GetValue(berthMessageTypeCode)?.Value<string>("from") ?? string.Empty;
            this.To = messageObject.GetValue(berthMessageTypeCode)?.Value<string>("to") ?? string.Empty;
            this.Description = messageObject.GetValue(berthMessageTypeCode)?.Value<string>("descr") ?? throw GFException.Build<string, string>($"The {nameof(BerthMessage)} descr property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(BerthMessage), "descr");
            this.Time = TimeSpan.FromSeconds(messageObject.GetValue(berthMessageTypeCode)?.Value<double>("time") ?? throw GFException.Build<string, string>($"The {nameof(BerthMessage)} time property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(BerthMessage), "time"));
            this.BerthMessageType = messageObject.GetValue(berthMessageTypeCode)?.Value<string>("msg_type") ?? string.Empty; ;

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="areaId">The simulation area</param>
        /// <param name="from">The from berth Id</param>
        /// <param name="to">The to berth Id</param>
        /// <param name="description">The berth description</param>
        /// <param name="berthMessageType">The berth message type</param>
        /// <param name="time">The time the message was sent</param>
        /// <param name="callingMethod">The calling method name. Do not pass.</param>
        /// <exception cref="ArgumentNullException">Throw if the areaId argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the description argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the berthMessageType argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the time argument is the default value</exception>
        /// <exception cref="ArgumentException">Throw if the from and the to arguments are both null</exception>
        public BerthMessage(string areaId, string from, string to, string description, string berthMessageType, TimeSpan time, [CallerMemberName] string? callingMethod = null)
        {
            //validate paramaters
            if (string.IsNullOrEmpty(areaId))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(areaId), callingMethod), "You must supply an area Id");
            }

            if (time == default)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(time), callingMethod), "You must supply the time");
            }

            if (string.IsNullOrEmpty(description))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(description), callingMethod), "You must supply the berth description");
            }

            if (string.IsNullOrEmpty(berthMessageType))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(areaId), callingMethod), "You must supply a berth message type");
            }

            if (string.IsNullOrEmpty(from) && string.IsNullOrEmpty(to))
            {
                throw new ArgumentException("Either or both the from and to arguments must be supplied", GFException.BuildArgumentName(nameof(areaId), callingMethod));
            }

            AreaId = areaId;
            From = from;
            To = to;
            Description = description;
            BerthMessageType = berthMessageType;
            Time = time;
        }
    }
}
