namespace GF.SimSig.UnitTests.Gateway.Messages._ReceivedMessages
{
    public class BerthInterposeMessage
    {
        /// <summary>
        /// Tests the <see cref="SimSig.Gateway.Messages.BerthInterposeMessage"/> constructor which accepts a SimSig gateway JSON message as the parameter
        /// </summary>
        /// <param name="message">The SimSig gateway message as a <see cref="JObject"/></param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_Data))]
        public void Test_Constructor(JObject message, SimSig.Gateway.Messages.MessageType messageType, SimSig.Gateway.Messages.BerthInterposeMessage expectedMessage)
        {
            SimSig.Gateway.Messages.BerthInterposeMessage testMessage = new(message, messageType);
            Assert.Equivalent(expectedMessage, testMessage);
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_Constructor(JObject, Messages.BerthInterposeMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_Data =>
            new List<object[]>
            {
            new object[] {
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CC_MSG"":{""area_id"":""north_wales_coast"",""to"":""GN17"",""descr"":""2R43"",""msg_type"":""CC"",""time"":""456""}}").MessageObject,
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CC_MSG"":{""area_id"":""north_wales_coast"",""to"":""GN17"",""descr"":""2R43"",""msg_type"":""CC"",""time"":""456""}}").MessageType,
                new SimSig.Gateway.Messages.BerthInterposeMessage("north_wales_coast", string.Empty, "GN17", "2R43", "CC", TimeSpan.FromSeconds(456))
            },
        };

        /// <summary>
        /// Tests the constructor throw the appropriate exceptions if the passed arguments are invalid
        /// </summary>
        /// <param name="contructorAction">The action which initialises the constructor</param>
        /// <param name="exceptedException">The exception that should be be thrown by the constructor</param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_ArgumentExceptions_Data))]
        public void Test_Constructor_ArgumentExceptions(Action contructorAction, Type exceptedException)
        {
            Assert.Throws(exceptedException, contructorAction);
        }
        /// <summary>
        /// Builds the test data from the <see cref="Test_Constructor(JObject, MessageType, SimSig.Gateway.Messages.BerthInterposeMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_ArgumentExceptions_Data =>
            new List<object[]>
                {
                new object[] {
                    new Action(() => {var message = new SimSig.Gateway.Messages.BerthInterposeMessage(null!, MessageType.TrainDescriberBerthInterposeMessage); }),
                    typeof(ArgumentNullException)
                },
                new object[] {
                    new Action(() => { var message = new SimSig.Gateway.Messages.BerthInterposeMessage(
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CC_MSG"":{""property1"":""north_wales_coast"",""property2"":""GN17"",""property3"":""2R43"",""property4"":""CC"",""property5"":""456"",""property76"":""someValue""}}").MessageObject,
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CC_MSG"":{""property1"":""north_wales_coast"",""property2"":""GN17"",""property3"":""2R43"",""property4"":""CC"",""property5"":""456"",""property76"":""someValue""}}").MessageType
                    ); 
                    }),
                    typeof(Common.GFException)
                },
            };
    };
}