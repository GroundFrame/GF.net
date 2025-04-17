using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace GF.Common.Translations
{
    public class GFTranslation
    {
        /// <summary>
        /// Gets the language of the translation. Either ISO 2 letter language name (see <see cref="https://en.wikipedia.org/wiki/List_of_ISO_639-2_codes"/>)
        /// </summary>
        public string Language { get; private set; }

        /// <summary>
        /// Gets the translation
        /// </summary>
        public string Translation { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="GFTranslation"/> from the supplied language and translation
        /// </summary>
        /// <param name="language">The language of the translation</param>
        /// <param name="translation">The translation</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="language"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translation"/> argument is null or an empty string</exception>
        public GFTranslation(string language, string translation, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(language))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(language), callingMethod), "You must supply a language for the translation");
            }

            if (string.IsNullOrEmpty(translation))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translation), callingMethod), "You must supply a translation");
            }

            Language = CultureInfo.GetCultureInfo(language).TwoLetterISOLanguageName;
            Translation = translation;
        }

        /// <summary>
        /// Instantiates a new <see cref="GFTranslation"/> from the supplied language and translation
        /// </summary>
        /// <param name="lcid">The lcid of the translation</param>
        /// <param name="translation">The translation</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentException">Thrown if the <paramref name="lcid"/> argument less than 1</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translation"/> argument is null or an empty string</exception>
        public GFTranslation(int lcid, string translation, [CallerMemberName] string? callingMethod = null)
        {
            if (lcid <= 0)
            {
                throw new ArgumentException("You must supply a valid lcid", GFException.BuildArgumentName(nameof(lcid), callingMethod));
            }

            if (string.IsNullOrEmpty(translation))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translation), callingMethod), "You must supply a translation");
            }

            Language = CultureInfo.GetCultureInfo(lcid).TwoLetterISOLanguageName;
            Translation = translation;
        }

        /// <summary>
        /// Returns the translation from the supplied <see cref="List{GFTranslation}"/> for the supplied <see cref="CultureInfo"/>
        /// </summary>
        /// <param name="translations">The list of translations from which to return the translation</param>
        /// <param name="culture">The culture of the translation to return</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>The translated string. If the translation can't be made then null is returned</returns>
        /// <remarks>If the translation cannot be found for the supplied culture then the parent culture is tried</remarks>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translations"/> argument is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="culture"/> argument is null</exception>
        public static string? GetTranslation(List<GFTranslation> translations, CultureInfo culture, [CallerMemberName] string? callingMethod = null)
        {
            if (translations == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translations), callingMethod), "You must supply a list of translations");
            }

            if (culture == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(culture), callingMethod), "You must supply a culture");
            }

            if (translations.Exists(t => t.Language == culture.TwoLetterISOLanguageName))
            {
                return translations.First(t => t.Language == culture.TwoLetterISOLanguageName).Translation;
            }

            if (translations.Exists(t => t.Language == culture.Parent.TwoLetterISOLanguageName))
            {
                return translations.First(t => t.Language == culture.Parent.TwoLetterISOLanguageName).Translation;
            }

            return null;
        }

        /// <summary>
        /// Returns the translation from the supplied <see cref="List{GFTranslation}"/> for the current thread culture
        /// </summary>
        /// <param name="translations">The list of translations from which to return the translation</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>The translated string. If the translation can't be made then null is returned</returns>
        /// <remarks>If the translation cannot be found for the current thread culture then the parent culture is tried</remarks>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translations"/> argument is null</exception>
        public static string? GetTranslation(List<GFTranslation> translations, [CallerMemberName] string? callingMethod = null)
        {
            if (translations == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translations), callingMethod), "You must supply a list of translations");
            }

            return GetTranslation(translations, Thread.CurrentThread.CurrentCulture);
        }
    }
}
