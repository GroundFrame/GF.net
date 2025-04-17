using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    public class FreeTextPanelItem : PanelItemBase, ISimSigPanelItem
    {
        public FreeTextPanelItem(string simSigID, ushort xOffSet, ushort yOffSet, IEnumerable<SimSigFontTokenConfig> configs) : base (simSigID, xOffSet, yOffSet)
        {
            foreach (var item in configs)
            {
                base.AddConfig(item);
            }
        }
    }
}
