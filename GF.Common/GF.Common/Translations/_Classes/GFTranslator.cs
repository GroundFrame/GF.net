using GF.Common.Attributes;
using GF.Common.Translations._Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common.Translations
{
    public static class GFTranslator
    {
        /// <summary>
        /// Adds translations to any properties which have been decorated with the <see cref="GFTranslatedPropertyAttribute"/> attribute
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectToTranslate"></param>
        /// <param name="callingMethod"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static T Translate<T>(T objectToTranslate, IGFTranslatorEngine translatorEngine, [CallerMemberName] string? callingMethod = null)
        {
            if (objectToTranslate == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(objectToTranslate), callingMethod), "You must supply an object to translate");
            }

            if (translatorEngine == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translatorEngine), callingMethod), "You must supply an translator engine");
            }

            return objectToTranslate;
        }
    }
}
