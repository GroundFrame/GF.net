using Newtonsoft.Json;

namespace GF.SimSig
{
    /// <summary>
    /// Struct representing a berth configuration which sits inside a parent <see cref="ISimSigPanelItem"/> implemented object
    /// </summary>
    /// <remarks>
    /// The default constructor
    /// </remarks>
    /// <param name="xOffSet">The x axis (horizontal) offset</param>
    /// <param name="yOffSet">The y axis (vertical) offset</param>
    /// <remarks>Use the <paramref name="xOffSet"/> and <paramref name="yOffSet"/> arguments to offset the berth text relative to the parent panel item</remarks>
    public struct BerthConfig(int xOffSet, int yOffSet)
    {
        /// <summary>
        /// Gets or sets the X offset
        /// </summary>
        [JsonProperty("xOffSet")]
        public int XOffSet { get; set; } = xOffSet;

        /// <summary>
        /// Gets or sets the Y offset
        /// </summary>
        [JsonProperty("yOffSet")]
        public int YOffSet { get; set; } = yOffSet;

        /// <summary>
        /// Gets the berth text
        /// </summary>
        [JsonProperty("text")]
        public string? Text { get; private set; }

        /// <summary>
        /// Sets the text of the berth
        /// </summary>
        /// <param name="text">The berth text</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="text"/> argument is null or an empty string</exception>
        /// <remarks>Use the <see cref="ClearText"/> method to clear berth text</remarks>
        public void SetText(string text)
        {
            if (string.IsNullOrEmpty(text))
            {
                throw new ArgumentNullException(nameof(text), $"The {nameof(text)} is null or an empty string.");
            }

            this.Text = text;
        }

        /// <summary>
        /// Clears the berth text
        /// </summary>
        public void ClearText()
        {
            this.Text = null;
        }
    }
}
