using GF.Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GF.SimSig.UnitTests")]
namespace GF.SimSig.Gateway.Messages
{
    public class ClockMessage : IReceivedMessage
    {
        /// <summary>
        /// Gets the simulation id. Note the format is the .sim file name
        /// </summary>
        public string AreaId { get; private set; }

        /// <summary>
        /// Gets the clock time
        /// </summary>
        public TimeSpan Time { get; private set; }

        /// <summary>
        /// Gets the speed of the simulator
        /// </summary>
        public int Interval { get; private set; }

        /// <summary>
        /// Gets whether the simulator is paused
        /// </summary>
        public bool Paused { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="ClockMessage"/> object from the supplied <see cref="JObject"/>
        /// </summary>
        /// <param name="messageObject">The source <see cref="JObject"/>. Usually parsed from a <see cref="ReceivedMessage"/> object</param>
        /// <param name="callingMethod">The name of the calling method. Do not pass.</param>
        /// <exception cref="ArgumentNullException">Thown if the messageObject is null</exception>
        /// <exception cref="GFException">Throw if the messageObject does not contain one of the expected properties</exception>
        public ClockMessage(JObject messageObject, [CallerMemberName] string? callingMethod = null)
        {
            //validate paramaters
            if (messageObject == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(messageObject), callingMethod), "You must supply a message object");
            }

            this.AreaId = messageObject.GetValue("clock_msg")?.Value<string>("area_id") ?? throw GFException.Build<string, string>($"The {nameof(ClockMessage)} area_id property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(ClockMessage), "area_id");
            this.Time = TimeSpan.FromSeconds(messageObject.GetValue("clock_msg")?.Value<double>("clock") ?? throw GFException.Build<string, string>($"The {nameof(ClockMessage)} clock property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(ClockMessage), "clock"));
            this.Interval = messageObject.GetValue("clock_msg")?.Value<int>("interval") ?? throw GFException.Build<string, string>($"The {nameof(ClockMessage)} interval property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(ClockMessage), "interval");
            this.Paused = messageObject.GetValue("clock_msg")?.Value<bool>("paused") ?? throw GFException.Build<string, string>($"The {nameof(ClockMessage)} interval property cannot be found in the supplied message object", "GF.SimSig.Gateway.Messages.MissingProperty.Error", nameof(ClockMessage), "paused");
        }

        /// <summary>
        /// Instantiates a new <see cref="ClockMessage"/>. Should only be used for testing
        /// </summary>
        /// <param name="areaId">The simulation area</param>
        /// <param name="time">The clock time</param>
        /// <param name="interval">The interval</param>
        /// <param name="paused">Indicates whether the clock is paused</param>
        internal ClockMessage(string areaId, TimeSpan time, int interval, bool paused, [CallerMemberName] string? callingMethod = null)
        {
            //validate paramaters
            if (string.IsNullOrEmpty(areaId))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(areaId), callingMethod), "You must supply an area Id");
            }

            if (time == default)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(time), callingMethod), "You must supply the clock time");
            }

            AreaId = areaId;
            Time = time;
            Interval = interval;
            Paused = paused;
        }
    }
}
