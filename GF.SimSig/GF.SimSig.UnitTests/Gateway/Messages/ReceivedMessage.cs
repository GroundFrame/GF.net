namespace GF.SimSig.UnitTests.Gateway.Messages
{
    public class ReceivedMessage
    {
        /// <summary>
        /// Tests the <see cref="SimSig.Gateway.Messages.ReceivedMessage"/> constructor which accepts a SimSig gateway JSON message as the parameter
        /// </summary>
        /// <param name="simsigGatewayMessage">The SimSig gateway JSON message</param>
        /// <param name="expectedMessageType">The expected <see cref="MessageType"/> of the message</param>
        /// <param name="expectedMessage">The expected <see cref="IReceivedMessage"/> object which the <see cref="SimSig.Gateway.Messages.ReceivedMessage.Message"/> property returns</param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_Data))]
        public void Test_Constructor(string simsigGatewayMessage, MessageType expectedMessageType, IReceivedMessage expectedMessage)
        {
            SimSig.Gateway.Messages.ReceivedMessage testMessage = new(simsigGatewayMessage);
            Assert.Equal(simsigGatewayMessage, testMessage.JSONMessage);
            Assert.NotNull(testMessage.MessageObject);
            Assert.Equal(expectedMessageType, testMessage.MessageType);
            Assert.NotNull(testMessage.Message);
            var actualMessageType = testMessage.Message.GetType();
            var messageType = expectedMessage.GetType();
            Assert.Equal(messageType, actualMessageType);
        }

        /// <summary>
        /// Builds the data for the <see cref="Test_Constructor(string, MessageType, IReceivedMessage)"/> test.
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_Data =>
            new List<object[]>
            {
                new object[] {
                    @"{""clock_msg"":{""area_id"":""aston"",""clock"":2,""interval"":500,""paused"":true}}",
                    MessageType.ClockMessage,
                    new SimSig.Gateway.Messages.ClockMessage("aston", TimeSpan.FromSeconds(2), 500, true)
                },
                new object[] {
                    @"{""CA_MSG"":{""area_id"":""north_wales_coast"",""from"":""GN17"",""to"":""BR60"",""descr"":""2R43"",""msg_type"":""CA"",""time"":""566""}}",
                    MessageType.TrainDescriberBerthStepMessage,
                    new SimSig.Gateway.Messages.BerthStepMessage("north_wales_coast", "GN17", "BT60", "2R43", "CA", TimeSpan.FromSeconds(566))
                },
                new object[] {
                    @"{""CB_MSG"":{""area_id"":""north_wales_coast"",""from"":""BR60"",""descr"":""2R43"",""msg_type"":""CB"",""time"":""668""}}",
                    MessageType.TrainDescriberBerthCancelMessage,
                    new SimSig.Gateway.Messages.BerthCancelMessage("north_wales_coast", "BR60", string.Empty, "2R43", "CB", TimeSpan.FromSeconds(668))
                },
                new object[] {
                    @"{""CC_MSG"":{""area_id"":""north_wales_coast"",""to"":""GN17"",""descr"":""2R43"",""msg_type"":""CC"",""time"":""456""}}",
                    MessageType.TrainDescriberBerthInterposeMessage,
                    new SimSig.Gateway.Messages.BerthInterposeMessage("north_wales_coast", string.Empty, "GN17", "2R43", "CC", TimeSpan.FromSeconds(456))
                },
            };
    }
}