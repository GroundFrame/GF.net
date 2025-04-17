using GF.SimSig.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.Gateway
{    public static class Ext_GatewayTopic
    {
        /// <summary>
        /// Returns the topic destination for the supplied <see cref="GatewayTopic"/>
        /// </summary>
        /// <param name="gatewayTopic">The <see cref="GatewayTopic"/> to return the destination for</param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException">Thrown if the supplied <see cref="GatewayTopic"/> has not been implemented</exception>
        public static string Destination(this GatewayTopic gatewayTopic)
        {
            switch (gatewayTopic)
            {
                case GatewayTopic.Signalling:
                    return "TD_ALL_SIG_AREA";
                case GatewayTopic.TrainMovements:
                    return "TRAIN_MVT_ALL_TOC";
                case GatewayTopic.HeartBeat:
                    return "SimSig";
                default:
                    throw new NotImplementedException($"The GatewayTopic {Enum.GetName(typeof(GatewayTopic), gatewayTopic)} has not have a Destination implemented.");
            }
        }
    }
}
