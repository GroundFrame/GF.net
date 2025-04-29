namespace GF.Rails.UnitTests
{
    public class SignalState
    {
        /// <summary>
        /// Tests the <see cref="GF.Rails.Network.SandT.SignalState.SetCurrentState(ISignalHead, SignalStateOptions)"/> method throws and <see cref="ArgumentException"/> if an invalid state is passed compared to the possible states of the signal head
        /// </summary>
        [Fact]
        public void SetCurrentState_ArgumentException()
        {
            var testSignalHead = new GF.Rails.Network.SandT.ColourLightMainAspect(ColourLightBulbType.MASIncandescent, SignalStateOptions.Red | SignalStateOptions.Green, SignalStateOptions.Green, SignalStateOptions.Red, false);
            var testSignalState = new GF.Rails.Network.SandT.SignalState();

            //check an exception is thrown if the state is not valid for the signal head possble stated
            Assert.Throws<ArgumentException>(() => testSignalState.SetCurrentState(testSignalHead, SignalStateOptions.SingleYellow));
        }

        /// <summary>
        /// Tests the <see cref="GF.Rails.Network.SandT.SignalState.SetCurrentState(ISignalHead, SignalStateOptions)"/> method
        /// </summary>
        [Theory]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.Green, SignalStateOptions.Green, SignalStateOptions.Green, true)]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.Green, SignalStateOptions.Green, SignalStateOptions.Red, false)]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.SingleYellow | SignalStateOptions.Green, SignalStateOptions.Green | SignalStateOptions.SingleYellow, SignalStateOptions.Green, true)]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.SingleYellow | SignalStateOptions.Green, SignalStateOptions.Green | SignalStateOptions.SingleYellow, SignalStateOptions.SingleYellow, true)]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.SingleYellow | SignalStateOptions.Green, SignalStateOptions.Green | SignalStateOptions.SingleYellow, SignalStateOptions.Red, false)]
        [InlineData(SignalStateOptions.Red | SignalStateOptions.SingleYellow | SignalStateOptions.Green | SignalStateOptions.IsFlashing, SignalStateOptions.Green | SignalStateOptions.SingleYellow, SignalStateOptions.SingleYellow | SignalStateOptions.IsFlashing, true)]
        public void SetCurrentState(SignalStateOptions possibleStates, SignalStateOptions offState, SignalStateOptions newState, bool expectedIsOff)
        {
            var testSignalHead = new GF.Rails.Network.SandT.ColourLightMainAspect(ColourLightBulbType.MASIncandescent, possibleStates, offState, SignalStateOptions.Red, false);
            var testSignalState = new GF.Rails.Network.SandT.SignalState();
            testSignalState.SetCurrentState(testSignalHead, newState);
            Assert.Equal(expectedIsOff, testSignalState.IsOff);
            Assert.Equal(newState, testSignalState.CurrentState);
        }

        /// <summary>
        /// Tests the <see cref="GF.Rails.Network.SandT.SignalState.BuildSignalState(ISignalHead, SignalStateOptions)"/> method
        /// </summary>
        [Fact]
        public void BuildSignalState()
        {
            var testSignalHead = new GF.Rails.Network.SandT.ColourLightMainAspect(ColourLightBulbType.MASIncandescent, SignalStateOptions.Red | SignalStateOptions.Green, SignalStateOptions.Green, SignalStateOptions.Red, false);
            var testSignalState = GF.Rails.Network.SandT.SignalState.BuildSignalState(testSignalHead, SignalStateOptions.Green);
            testSignalState.SetCurrentState(testSignalHead, SignalStateOptions.Green);
            Assert.True(testSignalState.IsOff);

            testSignalState = GF.Rails.Network.SandT.SignalState.BuildSignalState(testSignalHead, SignalStateOptions.Red);
            testSignalState.SetCurrentState(testSignalHead, SignalStateOptions.Red);
            Assert.False(testSignalState.IsOff);


            //check an exception is thrown if the state is not valid for the signal head possble stated
            Assert.Throws<ArgumentException>(() => GF.Rails.Network.SandT.SignalState.BuildSignalState(testSignalHead, SignalStateOptions.SingleYellow));
        }
    }
}