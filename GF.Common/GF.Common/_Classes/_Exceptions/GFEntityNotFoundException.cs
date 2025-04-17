using System.Runtime.CompilerServices;
using GF.Common.Attributes;

namespace GF.Common
{
    public class GFEntityNotFoundException : GFException
    {
        /// <summary>
        /// Instantiates a new <see cref="GFEntityNotFoundException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        public GFEntityNotFoundException(string message, [CallerMemberName] string? callingMethod = null) : base(message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }
        }

        /// <summary>
        /// Instantiates a new <see cref="GFEntityNotFoundException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="innerException"/> argument is null</exception>
        public GFEntityNotFoundException(string message, Exception innerException, [CallerMemberName] string? callingMethod = null) : base(message, innerException)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (innerException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(innerException), callingMethod), "You must supply an inner exception");
            }
        }

        /// <summary>
        /// Instantiates a new <see cref="GFEntityNotFoundException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="translationKey"/> argument is null or an empty string</exception>
        private GFEntityNotFoundException(string message, string translationKey, object[] placeholders, [CallerMemberName] string? callingMethod = null) : base(message, translationKey, placeholders)
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

        #region Methods

        /// <summary>
        /// Builds a translated <see cref="GFEntityNotFoundException"/>
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFEntityNotFoundException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        public new static GFEntityNotFoundException Build(string message, string translationKey, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            return new GFEntityNotFoundException(message, translationKey, Array.Empty<object>());
        }

        /// <summary>
        /// Builds a translated <see cref="GFEntityNotFoundException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFEntityNotFoundException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        public new static GFEntityNotFoundException Build<T0>(string message, string translationKey, T0 arg0, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            if (arg0 == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(arg0), callingMethod), "You must supply a placeholder. It cannot be null");
            }

            return new GFEntityNotFoundException(message, translationKey, new object[] { arg0 });
        }

        /// <summary>
        /// Builds a translated <see cref="GFEntityNotFoundException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The first translation placeholder value</param>
        /// <param name="arg1">The second translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFEntityNotFoundException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg1"/> is null</exception>
        public new static GFEntityNotFoundException Build<T0, T1>(string message, string translationKey, T0 arg0, T1 arg1, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            if (arg0 == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(arg0), callingMethod), "You must supply a placeholder. It cannot be null");
            }

            if (arg1 == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(arg0), callingMethod), "You must supply a placeholder. It cannot be null");
            }

            return new GFEntityNotFoundException(message, translationKey, new object[] { arg0, arg1 });
        }

        #endregion
    }
}