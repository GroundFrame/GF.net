namespace GF.SimSig.UnitTests.Gateway.Messages._ReceivedMessages
{
    public class ClockMessage
    {
        /// <summary>
        /// Tests the <see cref="SimSig.Gateway.Messages.ClockMessage"/> constructor which accepts a SimSig gateway JSON message as the parameter
        /// </summary>
        /// <param name="message">The SimSig gateway message as a <see cref="JObject"/></param>
        [Theory]
        [MemberData(nameof(Build_Test_Constructor_Data))]
        public void Test_Constructor(JObject message, SimSig.Gateway.Messages.ClockMessage expectedMessage)
        {
            SimSig.Gateway.Messages.ClockMessage testMessage = new(message);
            Assert.Equivalent(expectedMessage, testMessage);
        }

        /// <summary>
        /// Builds the test data from the <see cref="Test_Constructor(JObject, Messages.ClockMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_Data =>
            new List<object[]>
            {
            new object[] {
                new SimSig.Gateway.Messages.ReceivedMessage(@"{""clock_msg"":{""area_id"":""aston"",""clock"":2,""interval"":500,""paused"":true}}").MessageObject,
                new SimSig.Gateway.Messages.ClockMessage("aston", TimeSpan.FromSeconds(2), 500, true)
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
        /// Builds the test data from the <see cref="Test_Constructor(JObject, Messages.ClockMessage)"/> method 
        /// </summary>
        public static IEnumerable<object[]> Build_Test_Constructor_ArgumentExceptions_Data =>
            new List<object[]>
                {
                new object[] {
                    new Action(() => {var message = new SimSig.Gateway.Messages.ClockMessage(null!); }),
                    typeof(ArgumentNullException)
                },
                new object[] {
                    new Action(() => {var message = new SimSig.Gateway.Messages.ClockMessage(
                        new SimSig.Gateway.Messages.ReceivedMessage(
                            @"{""clock_msg"":{""property1"":""aston"",""property2"":2,""property3"":500,""property4"":true,""property5"":""someString""}}").MessageObject
                        );
                    }),
                    typeof(Common.GFException)
                },
            };
    };
}