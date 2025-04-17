namespace GF.SimSig
{
    /// <summary>
    /// A non track circuited section of track
    /// </summary>
    public class SignalHead : PanelItemBase, ISimSigPanelItem
    {
        #region Fields

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the orientation of the track circuit
        /// </summary>
        [JsonProperty("orientation")]
        public SimSigOrientation Orientation { get; private set; }

        /// <summary>
        /// Gets or sets the signal head configuation
        /// </summary>
        [JsonProperty("signalHeadConfig")]
        public SimSigSignalHeadOption SignalHeadConfig { get; private set; }


        #endregion Properties

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="simSigID">The SimSig ID of the signal head.</param>
        /// <param name="xOffSet">The horizontal offset of where the signal head should be placed. 0 is the left-hand side of the screen.</param>
        /// <param name="yOffSet">The vertical offset of where the signal head should be placed. 0 is the left-hand side of the screen.</param>
        /// <param name="orientation">The orientation of the signal head.</param>
        /// <param name="signalHeadConfig">Sets the configuration flags of the signal.</param>
        /// <param name="isFringe">Indicates whether the signal is a fringe signal on the panel</param>
        public SignalHead(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, SimSigSignalHeadOption signalHeadConfig, bool isFringe) : base (simSigID, xOffSet, yOffSet, isFringe)
        {
            this.Orientation = orientation;
            this.SignalHeadConfig = signalHeadConfig;
            this.BuildConfig();
        }

        /// <summary>
        /// Builds the configutation
        /// </summary>
        /// <exception cref="NotImplementedException">Thrown if the <see cref="SignalHead.Orientation"/> has not been implemented in the method.</exception>
        public void BuildConfig()
        {
            base.ClearConfig();

            switch (this.Orientation)
            {
                case SimSigOrientation.East:
                    this.BuildEast();
                    break;
                case SimSigOrientation.West:
                    this.BuildWest();
                    break;
                default:
                    throw new NotImplementedException($"The {nameof(this.Orientation)} argument {Enum.GetName(typeof(SimSigOrientation), this.Orientation)} has not been implemented in the constructor");
            }
        }

        /// <summary>
        /// Builds a wast facing signal
        /// </summary>
        private void BuildWest()
        {
            int x = 0;

            this.BuildSignalPost();
            x--;

            //is main aspect with call on / shunt signal on post.
            if (this.GetIsMainAspectWithCallOn())
            {
                base.AddConfig(new SimSigFontTokenConfig(this.GetIsReversed() ? "0x73" : "0x72", this.GetSignalHeadColour(true), x, 0));
                x--;
            }

            base.AddConfig(new SimSigFontTokenConfig(this.GetSignalHeadFontKey(), this.GetSignalHeadColour(), x, 0, this.GetIsFlashing()));
            x--;

            if (
                (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainAspect) == SimSigSignalHeadOption.DoubleMainAspect ||
                (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainWithCallOn) == SimSigSignalHeadOption.DoubleMainWithCallOn
            )
            {
                base.AddConfig(new SimSigFontTokenConfig(this.GetSignalHeadFontKey(), this.GetSignalHeadColour(), x, 0, this.GetIsFlashing()));
            }
        }

        /// <summary>
        /// Builds an east facing signal
        /// </summary>
        private void BuildEast()
        {
            int x = 0;

            this.BuildSignalPost();
            x++;

            //is main aspect with call on / shunt signal on post.
            if (this.GetIsMainAspectWithCallOn())
            {
                base.AddConfig(new SimSigFontTokenConfig(this.GetIsReversed() ? "0x73" : "0x72", this.GetSignalHeadColour(true), x, 0));
                x++;
            }

            base.AddConfig(new SimSigFontTokenConfig(this.GetSignalHeadFontKey(), this.GetSignalHeadColour(), x, 0, this.GetIsFlashing()));
            x++;

            if (
                (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainAspect) == SimSigSignalHeadOption.DoubleMainAspect ||
                (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainWithCallOn) == SimSigSignalHeadOption.DoubleMainWithCallOn
            )
            {
                base.AddConfig(new SimSigFontTokenConfig(this.GetSignalHeadFontKey(), this.GetSignalHeadColour(), x, 0, this.GetIsFlashing()));
            }
        }

        /// <summary>
        /// Builds the signal post
        /// </summary>
        private void BuildSignalPost()
        {
            base.AddConfig(new SimSigFontTokenConfig(this.GetSignalPostFontKey(), this.GetSignalPostColour(), 0, 0));
        }

        /// <summary>
        /// Calculates whether the signal should be reversed.
        /// </summary>
        /// <returns><see cref="bool"/>. True if the signal should be revsered otherwise false.</returns>
        private bool GetIsReversed()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.IsReversed) == SimSigSignalHeadOption.IsReversed;
        }

        /// <summary>
        /// Calculates whether the signal head is a main aspect signal with a call on / subsidary signal
        /// </summary>
        /// <returns><see cref="bool"/>. True if the signal head has a call on / subsidary signal otherwise false.</returns>
        private bool GetIsMainAspectWithCallOn()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.SingleMainWithCallOn) == SimSigSignalHeadOption.SingleMainWithCallOn || (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainWithCallOn) == SimSigSignalHeadOption.DoubleMainWithCallOn;
        }

        /// <summary>
        /// Calculates whether the signal has a route set
        /// </summary>
        /// <returns><see cref="bool"/>. True if the signal has a route set otherwise false.</returns>
        private bool GetHasRouteSet()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.HasRouteSet) == SimSigSignalHeadOption.HasRouteSet;
        }
        
        /// <summary>
        /// Gets the signal post colour
        /// </summary>
        /// <returns>The <see cref="SimSigColour"/> for the signal post</returns>
        private SimSigColour GetSignalPostColour()
        {
            return this.IsFringe ? SimSigColour.Grey : this.GetHasRouteSet() ? SimSigColour.White : SimSigColour.Grey;
        }
        
        /// <summary>
        /// Calcualtes whether the signal is an auto signal
        /// </summary>
        /// <returns><see cref="Boolean"/>. True if it's an auto signal otherwise false.</returns>
        private bool GetIsAutoSignal()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.IsAutoSignal) == SimSigSignalHeadOption.IsAutoSignal;
        }

        /// <summary>
        /// Determines the correct signal head font key
        /// </summary>
        /// <returns><see cref="string"/> containing the signal head font key</returns>
        private string GetSignalHeadFontKey()
        {
            //main or double aspect. No need to take into account orientation as it's round :-)
            if (this.GetIsMainAspect())
            {
                return "0x71";
            }
            else
            {
                if ((this.SignalHeadConfig & SimSigSignalHeadOption.ShuntAspect) == SimSigSignalHeadOption.ShuntAspect)
                {
                    switch (this.Orientation)
                    {
                        case SimSigOrientation.East:
                            return "0x72";
                        case SimSigOrientation.West:
                            return "0x73";
                    }

                }

                return "0x93";
            }
        }

        /// <summary>
        /// Calculates whether the signal head has a main aspect signal
        /// </summary>
        /// <returns><see cref="Boolean"/>. If true the signal has main aspect otherwise signal is shunt signal only</returns>
        private bool GetIsMainAspect()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainAspect) == SimSigSignalHeadOption.DoubleMainAspect ||
                (this.SignalHeadConfig & SimSigSignalHeadOption.SingleMainAspect) == SimSigSignalHeadOption.SingleMainAspect ||
                (this.SignalHeadConfig & SimSigSignalHeadOption.DoubleMainWithCallOn) == SimSigSignalHeadOption.DoubleMainWithCallOn ||
                (this.SignalHeadConfig & SimSigSignalHeadOption.SingleMainWithCallOn) == SimSigSignalHeadOption.SingleMainWithCallOn;
        }

        /// <summary>
        /// Calculates whether the signal head a shunt signal only
        /// </summary>
        /// <returns><see cref="Boolean"/>. If true the signal is shint aspect only otherwise signal has a main aspect</returns>
        private bool GetIsShuntAspectOnly()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.ShuntAspect) == SimSigSignalHeadOption.ShuntAspect;
        }

        /// <summary>
        /// Calculates the signal post font key
        /// </summary>
        /// <returns><see cref="string"/> containing the signal post font key</returns>
        private string GetSignalPostFontKey()
        {
            //does the head have a main aspect?
            if (this.GetIsMainAspect())
            {
                //is signal reversed?
                if (this.GetIsReversed())
                {
                    switch (this.Orientation)
                    {
                        case SimSigOrientation.East:
                            return this.GetIsAutoSignal() ? "0x83" : "0x83";
                        case SimSigOrientation.West:
                            return this.GetIsAutoSignal() ? "0x81" : "0x81";
                    }
                }
                else
                {
                    switch (this.Orientation)
                    {
                        case SimSigOrientation.East:
                            return this.GetIsAutoSignal() ? "0x90" : "0x80";
                        case SimSigOrientation.West:
                            return this.GetIsAutoSignal() ? "0x93" : "0x83";
                    }
                }
            }

            if (this.GetIsShuntAspectOnly())
            {
                //if the shunt signal is reversed
                if (this.GetIsReversed())
                {
                    switch (this.Orientation)
                    {
                        case SimSigOrientation.East:
                            return "0x82";
                        case SimSigOrientation.West:
                            return "0x81";
                    }
                }
                else
                {
                    switch (this.Orientation)
                    {
                        case SimSigOrientation.East:
                            return "0x80";
                        case SimSigOrientation.West:
                            return "0x83";
                    }
                }
            }

            return "0x90";
        }

        /// <summary>
        /// Calculates whether the signal head should be flashing
        /// </summary>
        /// <returns><see cref="bool"/>True if the head should be flashing otherwsise false.</returns>
        private bool GetIsFlashing()
        {
            return (this.SignalHeadConfig & SimSigSignalHeadOption.Flashing) == SimSigSignalHeadOption.Flashing;
        }

        /// <summary>
        /// Gets the colour of the Signal Head from the <see cref="SignalHeadConfig"/> property.
        /// </summary>
        /// <param name="isCallOnSignal">Flag to indicate whether the signal is a call on / shunt signal on post</param>
        /// <returns>The <see cref="SimSigColour"/> for the Signal Head</returns>
        private SimSigColour GetSignalHeadColour(bool isCallOnSignal = false)
        {
            if (this.IsFringe)
            {
                return SimSigColour.Grey;
            }

            if (isCallOnSignal)
            {
                if ((this.SignalHeadConfig & SimSigSignalHeadOption.ShuntOff) == SimSigSignalHeadOption.ShuntOff)
                {
                    //signal is off so return white
                    return SimSigColour.White;
                }
                else
                {
                    //return grey
                    return SimSigColour.Grey;
                }
            }

            //main aspect
            if ((this.SignalHeadConfig & SimSigSignalHeadOption.Red) == SimSigSignalHeadOption.Red)
            {
                return SimSigColour.Red;
            }

            if ((this.SignalHeadConfig & SimSigSignalHeadOption.Yellow) == SimSigSignalHeadOption.Yellow)
            {
                return SimSigColour.Yellow;
            }

            if ((this.SignalHeadConfig & SimSigSignalHeadOption.Green) == SimSigSignalHeadOption.Green)
            {
                return SimSigColour.Green;
            }

            return SimSigColour.Grey;
        }
    }
}
