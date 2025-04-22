using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Rails.Network
{
    public sealed class SAndC
    {
        #region Fields

        private static readonly Lazy<SAndC> lazy = new Lazy<SAndC>(() => new SAndC());
        private readonly List<ISignalBox> _signalBoxes = new List<ISignalBox>();

        #endregion Fields

        /// <summary>
        /// Gets the S and C instance
        /// </summary>
        public static SAndC Instance { get { return lazy.Value; } }

        public IEnumerable<ISignalBox> SignalBoxes { get { return this._signalBoxes; } }

        #region Constructors

        private SAndC() { }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Adds or updates a signal box
        /// </summary>
        /// <param name="signalBox">The <see cref="ISignalBox"/> to add or update</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="signalBox"/> argument is null</exception>
        public void AddOrUpddateSignalBox(ISignalBox signalBox)
        {
            if (signalBox == null) throw new ArgumentNullException(nameof(signalBox), "You must supply the signal box to add or update");

            int existingIndex = this._signalBoxes.FindIndex(sb => sb.Id.Equals(signalBox.Id));

            if (existingIndex == -1)
            {
                this._signalBoxes.Add(signalBox);
                return;
            }

            this._signalBoxes[existingIndex] = signalBox;
        }

        #endregion Methods
    }
}
