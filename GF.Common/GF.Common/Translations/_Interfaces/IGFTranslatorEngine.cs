using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common.Translations._Interfaces
{
    public interface IGFTranslatorEngine
    {
        /// <summary>
        /// Gets the version of the translator engine to use
        /// </summary>
        public int Version { get; }

        /// <summary>
        /// Gets the translator engine settings
        /// </summary>
        public Dictionary<GFTranslationEngineSettingType, object> Settings { get; }
    }
}
