using System;
using System.Collections.Generic;
using System.Text;

namespace GF.Rails.Network.SandT
{
    public class Token
    {
        private readonly List<TemporalId<SignalBlockId>> _signalBlockIds;

        public TemporalId<TokenId> Id { get; }

        public string Key { get; }

        public string Name { get; }

        public IEnumerable<TemporalId<SignalBlockId>> SignalBlocks { get { return this._signalBlockIds; } }

        public Token(DateTime startDate, DateTime? endDate, string key, string name)
        {
            this._signalBlockIds = new List<TemporalId<SignalBlockId>>();
            this.Id = new TemporalId<TokenId>(new TokenId(Guid.NewGuid()), startDate, endDate);
            this.Key = key;
            this.Name = name;
        }

        public void AddSignalBlock(SignalBlockHistory signalBlockHistory)
        {
            if (!signalBlockHistory.Id.TemporalPeriod.Overlaps(this.Id.TemporalPeriod))
            {
                throw new ArgumentException("The supplied signal block history must overlap the temporal period of this token.");
            }

            int index = this._signalBlockIds.FindIndex(sbi => sbi.Equals(signalBlockHistory.Id));

            if (index != -1)
            {
                this._signalBlockIds[index] = signalBlockHistory.Id;
                return;
            }

            this._signalBlockIds.Add(signalBlockHistory.Id);
        }
    }
}
