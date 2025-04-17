using GF.SimSig.Gateway;
using GF.SimSig.Gateway.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig.UnitTests.Gateway.Messages._ExtensionMethods
{
    public class Ext_MessageType
    {
        /// <summary>
        /// Tests that each member of <see cref="MessageType"/> enum is implemented within the <see cref="SimSig.Gateway.Messages.Ext_MessageType.GetMessageCode(MessageType)"/> method
        /// </summary>
        /// <param name="messageType">The message type</param>
        [Theory]
        [MemberData(nameof(Build_Test_GetMessageCode_Members_Implemented_Data))]
        public void Test_GetMessageCode_Members_Implemented(MessageType messageType)
        {
            Assert.NotNull(messageType.GetMessageCode());
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_GetMessageCode_Members_Implemented(MessageType)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_GetMessageCode_Members_Implemented_Data()
        {
            foreach (var messageType in Enum.GetValues(typeof(MessageType)))
            {
                yield return new object[] { messageType };
            }
        }
    }
}
