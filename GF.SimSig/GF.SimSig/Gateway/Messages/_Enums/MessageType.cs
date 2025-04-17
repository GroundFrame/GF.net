using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway.Messages
{
    /// <summary>
    /// Enum representing the various SimSig Gateway message types
    /// </summary>
    public enum MessageType
    {
        /// <summary>
        /// Indicates the message is a Clock Message. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#clock_message"/>
        /// </summary>
        ClockMessage = 1,
        /// <summary>
        /// Indicates the message is a Berth Step Message. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#train_describer_(td)"/>
        /// </summary>
        TrainDescriberBerthStepMessage = 2,
        /// <summary>
        /// Indicates the message is a Berth Cancel Message. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#train_describer_(td)"/>
        /// </summary>
        TrainDescriberBerthCancelMessage = 3,
        /// <summary>
        /// Indicates the message is a Berth Interpose Message. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#train_describer_(td)"/>
        /// </summary>
        TrainDescriberBerthInterposeMessage = 4
    }
}
