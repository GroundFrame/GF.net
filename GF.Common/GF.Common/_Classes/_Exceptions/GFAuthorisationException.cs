using System.Runtime.CompilerServices;

namespace GF.Common
{
    public partial class GFAuthorisationException : GFException
    {
        /// <summary>
        /// Instantiates a new <see cref="GFAuthorisationException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="translationKey"/> argument is null or an empty string</exception>
        public GFAuthorisationException(string message, string translationKey, object[] placeholders, [CallerMemberName] string? callingMethod = null) : base(message, translationKey, placeholders)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }
        }
    }
}