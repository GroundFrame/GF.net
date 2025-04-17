namespace GF.SimSig.UnitTests.Gateway.Messages._ReceivedMessages
{
    public class BerthStepMessage
    {
        /// <summary>
        /// Tests the <see cref="SimSig.Gateway.Messages.BerthStepMessage"/> constructor which accepts a SimSig gateway JSON message as the parameter
        /// </summary>
        /// <param name="message">The SimSig gateway message as a <see cref="JObject"/></param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_Data))]
        public void Test_Constructor(JObject message, SimSig.Gateway.Messages.MessageType messageType, SimSig.Gateway.Messages.BerthStepMessage expectedMessage)
        {
            SimSig.Gateway.Messages.BerthStepMessage testMessage = new(message, messageType);
            Assert.Equivalent(expectedMessage, testMessage);
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_Constructor(JObject, Messages.ClockMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_Data =>
            new List<object[]>
            {
            new object[] {
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CA_MSG"":{""area_id"":""north_wales_coast"",""from"":""GN17"",""to"":""BR60"",""descr"":""2R43"",""msg_type"":""CA"",""time"":""566""}}").MessageObject,
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CA_MSG"":{""area_id"":""north_wales_coast"",""from"":""GN17"",""to"":""BR60"",""descr"":""2R43"",""msg_type"":""CA"",""time"":""566""}}").MessageType,
                new SimSig.Gateway.Messages.BerthStepMessage("north_wales_coast", "GN17", "BR60", "2R43", "CA", TimeSpan.FromSeconds(566))
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
        /// Builds the test data from the <see cref="Test_Constructor(JObject, MessageType, SimSig.Gateway.Messages.BerthStepMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_ArgumentExceptions_Data =>
            new List<object[]>
                {
                new object[] {
                    new Action(() => {var message = new SimSig.Gateway.Messages.BerthStepMessage(null!, MessageType.TrainDescriberBerthCancelMessage); }),
                    typeof(ArgumentNullException)
                },
                new object[] {
                    new Action(() => { var message = new SimSig.Gateway.Messages.BerthStepMessage(
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CA_MSG"":{""property1"":""north_wales_coast"",""property2"":""GN17"",""property3"":""BR60"",""property4"":""2R43"",""property5"":""CA"",""property6"":""566"",""property7"":""someValue""}}").MessageObject,
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CA_MSG"":{""property1"":""north_wales_coast"",""property2"":""GN17"",""property3"":""BR60"",""property4"":""2R43"",""property5"":""CA"",""property6"":""566"",""property7"":""someValue""}}").MessageType
                    ); 
                    }),
                    typeof(Common.GFException)
                },
            };
    };
}