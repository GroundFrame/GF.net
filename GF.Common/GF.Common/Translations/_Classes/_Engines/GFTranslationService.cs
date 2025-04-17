using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GF.Common.Translations._Interfaces;

namespace GF.Common.Translations.Engines
{
    public class GFTranslationService : IGFTranslatorEngine
    {
        public int Version { get; private set; }

        public Dictionary<GFTranslationEngineSettingType, object> Settings { get; private set; }

        public GFTranslationService(int version, Dictionary<GFTranslationEngineSettingType, object> settings)
        {
            this.Version = version;
            this.Settings = settings;
        }
    }
}
