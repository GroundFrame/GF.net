using GF.SimSig.Gateway;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace GF.SimSig.UnitTests.Gateway._ExtensionMethods
{
    public class Ext_GatewayTopic
    {
        /// <summary>
        /// Tests that each member of <see cref="GatewayTopic"/> enum is implemented within the <see cref="SimSig.Gateway.Ext_GatewayTopic.Destination(GatewayTopic)"/> method
        /// </summary>
        /// <param name="gatewayTopic">The gateway topic</param>
        [Theory]
        [MemberData(nameof(Build_Test_Destination_Members_Implemented_Data))]
        public void Test_Destination_Members_Implemented(GatewayTopic gatewayTopic)
        {
            Assert.NotNull(gatewayTopic.Destination());
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_Destination_Members_Implemented(GatewayTopic)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Destination_Members_Implemented_Data()
        {
            foreach (var topic in Enum.GetValues(typeof(GatewayTopic)))
            {
                yield return new object[] { topic };
            }
        }
    }
}
