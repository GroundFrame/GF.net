namespace GF.SimSig
{
    public interface ISimSigPanelItem
    {
        public string SimSigID { get; }

        public IEnumerable<SimSigFontTokenConfig> Config { get; }

        /// <summary>
        /// Gets the horizontal offset of where the returned token should be placed on the screen. (0 is the left-hand side of the screen)
        /// </summary>
        public ushort XOffSet { get; }

        /// <summary>
        /// Gets the vertical offset of where the returned token should be placed on the screen. (0 is the top of the screen)
        /// </summary>
        public ushort YOffSet { get; }

        public string GenerateSVG(ISimSigFont font, SimSigStyle styleClass, List<string> usedFontKeys);
    }
}
