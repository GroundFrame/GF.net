namespace GF.SimSig.UnitTests.Gateway.Messages._ReceivedMessages
{
    public class BerthCancelMessage
    {
        /// <summary>
        /// Tests the <see cref="SimSig.Gateway.Messages.BerthCancelMessage"/> constructor which accepts a SimSig gateway JSON message as the parameter
        /// </summary>
        /// <param name="message">The SimSig gateway message as a <see cref="JObject"/></param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_Data))]
        public void Test_Constructor(JObject message, SimSig.Gateway.Messages.MessageType messageType, SimSig.Gateway.Messages.BerthCancelMessage expectedMessage)
        {
            SimSig.Gateway.Messages.BerthCancelMessage testMessage = new(message, messageType);
            Assert.Equivalent(expectedMessage, testMessage);
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_Constructor(JObject, Messages.BerthCancelMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_Data =>
            new List<object[]>
            {
            new object[] {
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CB_MSG"":{""area_id"":""north_wales_coast"",""from"":""BR60"",""descr"":""2R43"",""msg_type"":""CB"",""time"":""668""}}").MessageObject,
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""CB_MSG"":{""area_id"":""north_wales_coast"",""from"":""BR60"",""descr"":""2R43"",""msg_type"":""CB"",""time"":""668""}}").MessageType,
                new SimSig.Gateway.Messages.BerthCancelMessage("north_wales_coast", "BR60", string.Empty, "2R43", "CB", TimeSpan.FromSeconds(668))
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
        /// Builds the test data from the <see cref="Test_Constructor(JObject, MessageType, SimSig.Gateway.Messages.BerthCancelMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_ArgumentExceptions_Data =>
            new List<object[]>
                {
                new object[] {
                    new Action(() => {var message = new SimSig.Gateway.Messages.BerthCancelMessage(null!, MessageType.TrainDescriberBerthCancelMessage); }),
                    typeof(ArgumentNullException)
                },
                new object[] {
                    new Action(() => { var message = new SimSig.Gateway.Messages.BerthCancelMessage(
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CB_MSG"":{""property1"":""north_wales_coast"",""property2"":""BR60"",""property3"":""2R43"",""property4"":""CB"",""property5"":""668"",""property6"":""someValue""}}").MessageObject,
                        new SimSig.Gateway.Messages.ReceivedMessage(@"{""CB_MSG"":{""property1"":""north_wales_coast"",""property2"":""BR60"",""property3"":""2R43"",""property4"":""CB"",""property5"":""668"",""property6"":""someValue""}}").MessageType
                    ); 
                    }),
                    typeof(Common.GFException)
                },
            };
    };
}