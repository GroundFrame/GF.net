using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway
{
    /// <summary>
    /// Enum containing the available topics available within the gateway
    /// </summary>
    public enum GatewayTopic
    {
        /// <summary>
        /// Provides information on TD steps, and the state of the signalling equipment. It is also used to send certain requests to the simulation. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#td___all___sig___area_messages_recieved_from_the_server"/>
        /// </summary>
        Signalling = 1,
        /// <summary>
        /// Provides information on train movement within the simulation. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#train___mvt___all___toc_messages_recieved_from_the_server"/>
        /// </summary>
        TrainMovements = 2,
        /// <summary>
        /// Provides heartbeat messages at a minimum of every simulated minute. It is also used to send certain requests to the simulation. See <see cref="https://www.simsig.co.uk/Wiki/Show?page=usertrack:interface_gateway#simsig_messages_recieved_from_the_server"/>
        /// </summary>
        HeartBeat = 3
    }
}
