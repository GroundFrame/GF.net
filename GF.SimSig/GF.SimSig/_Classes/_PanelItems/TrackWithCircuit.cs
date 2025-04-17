namespace GF.SimSig
{
    /// <summary>
    /// A non track circuited section of track
    /// </summary>
    public class TrackWithCircuit : PanelItemBase, ISimSigPanelItem, ISimSigTrackCircuit
    {
        #region Fields

        private readonly List<BerthConfig> _berthConfigs;
        private bool _isOccupied = false;
        private bool _hasRouteSet = false;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets the orientation of the track circuit
        /// </summary>
        public SimSigOrientation Orientation { get; private set; }

        /// <summary>
        /// Gets the length of the track circuit
        /// </summary>
        public int Length { get; private set; }

        /// <summary>
        /// Gets or sets the flag which indicates whether the track circuit break should be show
        /// </summary>
        public bool ShowTrackCircuitBreak { get; set; }

        /// <summary>
        /// Gets or sets the flag to indiate whether the track circuit is occupied
        /// </summary>
        public bool IsOccupied { get { return _isOccupied; } set { this._isOccupied = value; this.BuildColour(); } }

        /// <summary>
        /// Gets or sets the flag to indiate whether the track circuit has a route set
        /// </summary>
        public bool HasRouteSet { get { return _hasRouteSet; } set { this._hasRouteSet = value; this.BuildColour(); } }


        /// <summary>
        /// Gets or sets the track circuit options
        /// </summary>
        public SimSigTrackCircuitOption Options { get; set; }

        /// <summary>
        /// Gets the track circuit id
        /// </summary>
        public string TrackCircuitID { get { return this.SimSigID; } }

        #endregion Properties

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="simSigID">The SimSig ID of the track circuit</param>
        /// <param name="xOffSet">The horizontal offset of where the track circuit should be placed. 0 is the left-hand side of the screen.</param>
        /// <param name="yOffSet">The vertical offset of where the track circuit should be placed. 0 is the left-hand side of the screen.</param>
        /// <param name="orientation">The orientation of the track circuit.</param>
        /// <param name="length">The length in co-ordinate space.</param>
        /// <param name="options">The track circuit options.</param>
        /// <param name="showTrackCircuitBreak">Flag to indicate whether the track cirucit breaks should be shown</param>
        /// <param name="isOccupied">Flag to indicate whether the track circuit is occupied.</param>
        /// <param name="hasRouteSet">Flag to indicate whether a route has been set through this track circuit.</param>
        /// <param name="berthConfigs">A collection containing the available berths within the track circuit</param>
        public TrackWithCircuit(string simSigID, ushort xOffSet, ushort yOffSet, SimSigOrientation orientation, int length, SimSigTrackCircuitOption options, bool showTrackCircuitBreak, bool isOccupied, bool hasRouteSet, IEnumerable<BerthConfig>? berthConfigs = null) : base (simSigID, xOffSet, yOffSet)
        {
            this._berthConfigs = berthConfigs == null ? [] : berthConfigs.ToList();
            this.Orientation = orientation;
            this.Length = length;
            this.Options = options;
            this.ShowTrackCircuitBreak = showTrackCircuitBreak;
            this.IsOccupied = isOccupied;
            this.HasRouteSet = hasRouteSet;
            this.BuildConfig();
        }

        /// <summary>
        /// Sets the text of the berth at the supplied index
        /// </summary>
        /// <param name="berthIndex">The index of the berth</param>
        /// <param name="text">The text. Must not exceed 4 characters</param>
        public void SetBerth(int berthIndex, string text)
        {
            if (this._berthConfigs.Count == 0 || berthIndex > this._berthConfigs.Count - 1) { return; }

            var newBerthConfig = this._berthConfigs[berthIndex];
            newBerthConfig.SetText(text);
            this._berthConfigs[berthIndex] = newBerthConfig;
            this.BuildConfig();
        }

        /// <summary>
        /// Clears the text of the berth at the supplied index
        /// </summary>
        /// <param name="berthIndex">The index of the berth</param>
        public void ClearBerth(int berthIndex)
        {
            if (this._berthConfigs.Count == 0 || berthIndex > this._berthConfigs.Count - 1) { return; }

            var newBerthConfig = this._berthConfigs[berthIndex];
            newBerthConfig.ClearText();
            this._berthConfigs[berthIndex] = newBerthConfig;
            this.BuildConfig();
        }

        /// <summary>
        /// Builds the colour of the track circuit
        /// </summary>
        private void BuildColour()
        {
            this.Colour = IsOccupied ? SimSigColour.Red : HasRouteSet ? SimSigColour.White : SimSigColour.Grey;
        }

        /// <summary>
        /// Builds the track circuit configuration
        /// </summary>
        /// <exception cref="NotImplementedException"></exception>
        public void BuildConfig()
        {
            string key = string.Empty;
            base.ClearConfig();

            switch (this.Orientation)
            {            
                case SimSigOrientation.West:
                    this.BuildWest();
                    break;
                case SimSigOrientation.SouthEast:
                    this.BuildSouthEast();
                    break;
                case SimSigOrientation.SouthWest:
                    this.BuildSouthWest();
                    break;
                case SimSigOrientation.East:
                    this.BuildEast();
                    break;
                case SimSigOrientation.NorthWest:
                    this.BuildNorthWest();
                    break;
                case SimSigOrientation.NorthEast:
                    this.BuildNorthEast();
                    break;
                default:
                    throw new NotImplementedException($"The {nameof(this.Orientation)} argument {Enum.GetName(typeof(SimSigOrientation), this.Orientation)} has not been implemented in the constructor");
            }

            foreach (var berthConfig in this._berthConfigs.Where(bc => !String.IsNullOrEmpty(bc.Text)))
            {
                base.AddConfig(new SimSigFontTokenConfig(String.Concat(Enumerable.Repeat("0x61", berthConfig.Text!.Length)), SimSigColour.Black, berthConfig.XOffSet, berthConfig.YOffSet));
                base.AddConfig(new SimSigFontTokenConfig(SimSigFont.ConvertTextToHex(berthConfig.Text!), SimSigColour.Cyan, berthConfig.XOffSet, berthConfig.YOffSet));
            }
        }

        /// <summary>
        /// Builds the west facing track circuit
        /// </summary>
        private void BuildWest()
        {
            string key = string.Empty;

            for (int i = 0; i < this.Length; i++)
            {
                //if first token
                if (i == 0)
                {
                    if (this.Length > 0)
                    {
                        key += ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x62" : ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5B" : "0x61";
                        //key += ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && this.ShowTrackCircuitBreak ? "0x63" : "0x61";
                    }

                    if (this.Length == 1)
                    {
                        key += ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && this.ShowTrackCircuitBreak ? "0x63" : ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x62" : ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5D" : "0x61";
                    }

                    continue;
                };

                //if intermediate tokens
                if (i < this.Length - 1)
                {
                    key += "0x61";
                    continue;
                }

                //if last token
                if (i == this.Length - 1)
                {
                    key += ((this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x62" : ((this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5B" : "0x61";
                    continue;
                }
            }

            base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, 0, 0));
        }

        /// <summary>
        /// Builds the east facing track circuit
        /// </summary>
        private void BuildEast()
        {
            string key = string.Empty;

            for (int i = 0; i < this.Length; i++)
            {
                //if first token
                if (i == 0)
                {
                    if (this.Length > 1)
                    {
                        key += ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x63" : ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5B" : "0x61";
                    }
                    
                    if (this.Length == 1)
                    {
                        key += ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x63" : ((this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5B" : ((this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd) && this.ShowTrackCircuitBreak ? "0x62" : "0x61";
                    }
                    continue;
                };

                //if intermediate tokens
                if (i < this.Length - 1)
                {
                    key += "0x61";
                    continue;
                }

                //if last token
                if (i == this.Length - 1)
                {
                    key += ((this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) != SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x62" : ((this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd) && ((this.Options & SimSigTrackCircuitOption.IsOverlap) == SimSigTrackCircuitOption.IsOverlap) && this.ShowTrackCircuitBreak ? "0x5D" : "0x61";
                    continue;
                }
            }

            base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, 0, 0));
        }

        /// <summary>
        /// Builds the south east facing track circuit
        /// </summary>
        private void BuildSouthEast()
        {
            string key;
            int i = 0; //keeps track of the length iterator
            int x = 0; //keeps track of the x offset increment inside the loop
            int y = 0; //keeps track of the y offset increment inside the loop

            while (i < this.Length)
            {
                //first character
                if (i == 0)
                {
                    key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak && i == this.Length - 1 ? "0x6C" : "0x68";
                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, i, i));
                    i++;
                    x += 2;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //odd numbered iterator 
                if (i % 1 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6D" : "0x69";
                    }
                    else
                    {
                        key = "0x69";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, (x / 2), y));
                    i++;
                    y++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //even numbered iterator 
                if (i % 2 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6C" : "0x68";
                    }
                    else
                    {
                        key = "0x68";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, (x / 2), y));
                    i++;
                    x += 2;

                    if (i == this.Length)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Builds the south west facing track circuit
        /// </summary>
        private void BuildSouthWest()
        {
            string key;
            int i = 0; //keeps track of the length iterator
            int x = 0; //keeps track of the x offset increment inside the loop
            int y = 0; //keeps track of the y offset increment inside the loop

            while (i < this.Length)
            {
                //first character
                if (i == 0)
                {
                    key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak && i == this.Length - 1 ? "0x6F" : "0x6B";
                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, i, i));
                    i++;
                    x+=2;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //odd numbered iterator 
                if (i % 1 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6E" : "0x6A";
                    }
                    else
                    {
                        key = "0x6A";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, -(x / 2), y));
                    i++;
                    y++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //even numbered iterator 
                if (i % 2 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6F" : "0x6B";
                    }
                    else
                    {
                        key = "0x6B";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, -(x / 2), y));
                    i++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }
            }
        }


        /// <summary>
        /// Builds the north west facing track circuit
        /// </summary>
        private void BuildNorthWest()
        {
            string key;
            int i = 0; //keeps track of the length iterator
            int x = 0; //keeps track of the x offset increment inside the loop
            int y = 0; //keeps track of the y offset increment inside the loop

            while (i < this.Length)
            {
                //first character
                if (i == 0)
                {
                    key = (this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart & this.ShowTrackCircuitBreak && i == this.Length - 1 ? "0x6D" : "0x69";
                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, i, i));
                    i++;
                    x += 2;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //odd numbered iterator 
                if (i % 1 == 0)
                {
                    //on a northwest facing track circuit it cannot have a break on an odd iteration
                    key = "0x68";

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, -(x / 2), -y));
                    i++;
                    y++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //even numbered iterator 
                if (i % 2 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6D" : "0x69";
                    }
                    else
                    {
                        key = "0x69";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, -(x / 2), -y));
                    i++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Builds the north east facing track circuit
        /// </summary>
        private void BuildNorthEast()
        {
            string key;
            int i = 0; //keeps track of the length iterator
            int x = 0; //keeps track of the x offset increment inside the loop
            int y = 0; //keeps track of the y offset increment inside the loop

            while (i < this.Length)
            {
                //first character
                if (i == 0)
                {
                    key = (this.Options & SimSigTrackCircuitOption.BreakAtStart) == SimSigTrackCircuitOption.BreakAtStart & this.ShowTrackCircuitBreak && i == this.Length - 1 ? "0x6E" : "0x6A";
                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, i, i));
                    i++;
                    x += 2;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //odd numbered iterator 
                if (i % 1 == 0)
                {
                    //on a east facing track circuit it cannot have a break on an odd iteration
                    key = "0x6B";

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, (x / 2), -y));
                    i++;
                    y++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }

                //even numbered iterator 
                if (i % 2 == 0)
                {
                    //are we on the final itereration?
                    if (i == this.Length - 1)
                    {
                        key = (this.Options & SimSigTrackCircuitOption.BreakAtEnd) == SimSigTrackCircuitOption.BreakAtEnd & this.ShowTrackCircuitBreak ? "0x6E" : "0x6A";
                    }
                    else
                    {
                        key = "0x6A";
                    }

                    base.AddConfig(new SimSigFontTokenConfig(key, this.Colour, (x / 2), -y));
                    i++;
                    x++;

                    if (i == this.Length)
                    {
                        break;
                    }
                }
            }
        }
    }
}
