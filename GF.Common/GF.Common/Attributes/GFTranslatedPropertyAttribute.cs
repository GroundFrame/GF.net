using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Property)]
    internal class GFTranslatedPropertyAttribute : Attribute
    {
        /// <summary>
        /// Gets the name of the property which contains the placeholder values for the translation of this property
        /// </summary>
        public string PlaceholderProperty { get; private set; }

        /// <summary>
        /// Gets the name of the property which cotnains the translation key for the translation of this property
        /// </summary>
        public string KeyProperty { get; private set; }

        public GFTranslatedPropertyAttribute(string keyProperty, string placeholderProperty)
        {
            this.PlaceholderProperty = placeholderProperty;
            this.KeyProperty = keyProperty;
        }
    }
}
