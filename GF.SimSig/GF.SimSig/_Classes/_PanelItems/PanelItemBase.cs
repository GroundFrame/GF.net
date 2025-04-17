namespace GF.SimSig
{
    public abstract class PanelItemBase
    {
        #region Fields

        private readonly List<SimSigFontTokenConfig> _config; //stores the font configuration

        #endregion Fields

        public string SimSigID { get; private set; }

        /// <summary>
        /// The collection of tokens that inherit
        /// </summary>
        public IEnumerable<SimSigFontTokenConfig> Config { get { return this._config; } }

        /// <summary>
        /// Gets the horizontal offset of where the returned token should be placed on the screen. (0 is the left-hand side of the screen)
        /// </summary>
        public ushort XOffSet { get; }

        /// <summary>
        /// Gets the vertical offset of where the returned token should be placed on the screen. (0 is the top of the screen)
        /// </summary>
        public ushort YOffSet { get; }

        /// <summary>
        /// Gets or sets the colour of the panel item
        /// </summary>
        public SimSigColour Colour { get; set; }

        /// <summary>
        /// Gets or sets a flag to indicate whether the panel item is a fringe item on the panel
        /// </summary>
        public bool IsFringe { get; set; }

        /// <summary>
        /// Connstructs a <see cref="PanelItemBase"/>
        /// </summary>
        /// <param name="simSigID">The SimSig Id for the panel item. For example the track circuit ID or Signal number</param>
        /// <param name="xOffSet">The horizontal offset of where the item should be placed in the panel</param>
        /// <param name="yOffSet">The vertical offset of where the item should be planed in the panel</param>
        /// <param name="colour">The default <see cref="SimSigColour"/></param>
        /// <param name="isFringe">Indicates whether the item appears as a fringe panel item</param>
        public PanelItemBase(string simSigID, ushort xOffSet, ushort yOffSet, SimSigColour colour, bool isFringe = false)
        {
            this.SimSigID = simSigID;
            this.XOffSet = xOffSet;
            this.YOffSet = yOffSet;
            this._config = [];
            this.Colour = colour;
            this.IsFringe = isFringe;
        }

        /// <summary>
        /// Connstructs a <see cref="PanelItemBase"/> with a default colour of <see cref="SimSigColour.Grey"/>
        /// </summary>
        /// <param name="simSigID">The SimSig Id for the panel item. For example the track circuit ID or Signal number</param>
        /// <param name="xOffSet">The horizontal offset of where the item should be placed in the panel</param>
        /// <param name="yOffSet">The vertical offset of where the item should be planed in the panel</param>
        /// <param name="isFringe">Indicates whether the item appears as a fringe panel item</param>
        public PanelItemBase(string simSigID, ushort xOffSet, ushort yOffSet, bool isFringe = false) : this(simSigID, xOffSet, yOffSet, SimSigColour.Grey, isFringe) { }

        public void AddConfig(SimSigFontTokenConfig config)
        {
            this._config.Add(config);
        }

        public void ClearConfig()
        {
            this._config.Clear();
        }

        public string GenerateSVG(ISimSigFont font, SimSigStyle simsSigStyle, List<string> usedFontKeys)
        {
            return font.BuildSVG(this.Config, simsSigStyle, usedFontKeys, this.XOffSet, this.YOffSet);
        }
    }
}
