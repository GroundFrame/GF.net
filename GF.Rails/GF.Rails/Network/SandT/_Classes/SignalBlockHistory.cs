using GF.Common;
using GF.Graph;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GF.Rails.Network.SandT
{
    public class SignalBlockHistory : ITemporalHistory<SignalBlockId>
    {
        #region Fields

        private readonly SignalBlockId _signalBlockId;

        #endregion Fields

        /// <summary>
        /// Gets the signal block Id
        /// </summary>
        public TemporalId<SignalBlockId> Id { get; private set; }

        /// <summary>
        /// Gets the flag which indicates whether the track box has control over a train.
        /// </summary>
        public bool CanControlTrain { get; }

        /// <summary>
        /// Gets the traction power that can enter this signal block
        /// </summary>
        public Power Power { get; }

        /// <summary>
        /// Gets the route availability of the signal block
        /// </summary>
        public byte RA { get; }

        /// <summary>
        /// Gets the average speed across the signal block
        /// </summary>
        public Speed AverageSpeed { get; }

        /// <summary>
        /// Gets the average gradient over the length of the length of the signal block
        /// </summary>
        public ReciprocalGradient AverageGradient { get; }

        /// <summary>
        /// Gets the signal box panel / workstation which controls this signal block
        /// </summary>
        public ISignalBoxPanelOrWorkstation SignalBoxPanelOrWorkstation { get; }

        /// <summary>
        /// Gets the signal box which controls this signal block
        /// </summary>
        public ISignalBox SignalBox { get { return this.SignalBoxPanelOrWorkstation.SignalBox; } }

        /// <summary>
        /// The default constructor
        /// </summary>
        /// <param name="startDate">The signal block start date</param>
        /// <param name="endDate">The signal block end date (pass null if still current)</param>
        /// <param name="panelOrWorkstation">The signal box panel or workstation which controls this signal block</param>
        /// <param name="power">The power types allowed on the track circuit</param>
        /// <param name="ra">The route availability</param>
        /// <param name="averageSpeed">The average speed across the signal block</param>
        /// <param name="averageGradient">The average gradient across the signal block</param>
        /// <param name="gradientDirection">The direction the <paramref name="averageGradient"/> applies to</param>
        /// <param name="canControlTrain">A flag to indicate whether the signal block has can control a train (ie is signalled)</param>
        public SignalBlockHistory(SignalBlockId signalBlockId, DateTime startDate, DateTime? endDate, ISignalBoxPanelOrWorkstation panelOrWorkstation, Power power, byte ra, Speed averageSpeed, Gradient averageGradient, DirectionType gradientDirection, bool canControlTrain = false)
        {
            this._signalBlockId = signalBlockId;
            this.SignalBoxPanelOrWorkstation = panelOrWorkstation;
            this.Id = new TemporalId<SignalBlockId>(signalBlockId, startDate, endDate);
            this.CanControlTrain = canControlTrain;
            this.Power = power;
            this.RA = ra;
            this.AverageSpeed = averageSpeed;
            this.AverageGradient = new ReciprocalGradient(averageGradient, gradientDirection);
        }

        /// <summary>
        /// Updates the Id
        /// </summary>
        /// <param name="newId">The new <see cref="TemporalId{SignalBlockId}"/></param>
        public void UpdateId(TemporalId<SignalBlockId> newId)
        {
            if (!newId.Id.Equals(this._signalBlockId))
            {
                throw new ArgumentException($"The new Id must have the same {nameof(SignalBlockId)} as the existing Id.");
            }

            this.Id = newId;
        }
    }
}
