using System.Runtime.CompilerServices;
using GF.Common.Translations;
using GF.Common.Attributes;
using System.ComponentModel.DataAnnotations;

namespace GF.Common
{
    public class GFException : Exception
    {
        /// <summary>
        /// Gets the message
        /// </summary>
        public override string Message { get { return this.TranslateMessage(); } }

        /// <summary>
        /// Gets or sets the list of message translations
        /// </summary>
        [GFTranslatedPropertyAttribute(nameof(MessageTranslationKey), nameof(MessageTranslationPlaceholders))]
        public List<GFTranslation>? MessageTranslations { get; set; }

        /// <summary>
        /// Gets the translation key
        /// </summary>
        public string? MessageTranslationKey { get; private set; }

        /// <summary>
        /// Gets the translation placeholders
        /// </summary>
        public object[]? MessageTranslationPlaceholders { get; private set; }

        /// <summary>
        /// Instantiates a new <see cref="GFException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        public GFException(string message, [CallerMemberName] string? callingMethod = null) : base(message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }
        }

        /// <summary>
        /// Instantiates a new <see cref="GFException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="innerException"/> argument is null</exception>
        public GFException(string message, Exception innerException, [CallerMemberName] string? callingMethod = null) : base(message, innerException)
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
        /// Instantiates a new <see cref="GFException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="translationKey"/> argument is null or an empty string</exception>
        public GFException(string message, string translationKey, object[] placeholders, [CallerMemberName] string? callingMethod = null) : base(message)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            this.MessageTranslationKey = translationKey;
            this.MessageTranslationPlaceholders = placeholders;
        }

        /// <summary>
        /// Instantiates a new <see cref="GFException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="placeholders">The placeholders associated with the translation</param>
        /// <param name="innerException">The inner <see cref="Exception"/></param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="translationKey"/> argument is null or an empty string</exception>
        internal GFException(string message, Exception innerException, string translationKey, object[] placeholders, [CallerMemberName] string? callingMethod = null) : base(message, innerException)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            if (innerException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(innerException), callingMethod), "You must supply an inner exception");
            }

            this.MessageTranslationKey = translationKey;
            this.MessageTranslationPlaceholders = placeholders;
        }

        #region Methods

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        public static GFException Build(string message, string translationKey, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            return new GFException(message, translationKey, Array.Empty<object>());
        }

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <param name="innerException">The inner <see cref="Exception"/></param>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        public static GFException Build(Exception innerException, string message, string translationKey, [CallerMemberName] string? callingMethod = null)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(message), callingMethod), "You must supply a message for the exception");
            }

            if (string.IsNullOrEmpty(translationKey))
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(translationKey), callingMethod), "You must supply a translation key");
            }

            if (innerException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(innerException), callingMethod), "You must supply an inner exception");
            }

            return new GFException(message, innerException, translationKey, Array.Empty<object>());
        }

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        public static GFException Build<T0>(string message, string translationKey, T0 arg0, [CallerMemberName] string? callingMethod = null)
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

            return new GFException(message, translationKey, new object[] { arg0 });
        }

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="innerException">The inner <see cref="Exception"/></param>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        public static GFException Build<T0>(Exception innerException, string message, string translationKey, T0 arg0, [CallerMemberName] string? callingMethod = null)
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

            if (innerException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(innerException), callingMethod), "You must supply an inner exception");
            }

            return new GFException(message, innerException, translationKey, new object[] { arg0 });
        }

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The first translation placeholder value</param>
        /// <param name="arg1">The second translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg1"/> is null</exception>
        public static GFException Build<T0, T1>(string message, string translationKey, T0 arg0, T1 arg1, [CallerMemberName] string? callingMethod = null)
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

            return new GFException(message, translationKey, new object[] { arg0, arg1 });
        }

        /// <summary>
        /// Builds a translated <see cref="GFException"/>
        /// </summary>
        /// <typeparam name="T0">The placeholder type</typeparam>
        /// <param name="innerException">The inner <see cref="Exception"/></param>
        /// <param name="message">The message</param>
        /// <param name="translationKey">The message translation key</param>
        /// <param name="arg0">The first translation placeholder value</param>
        /// <param name="arg1">The second translation placeholder value</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="message"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="translationKey"/> is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg0"/> is null</exception>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="arg1"/> is null</exception>
        public static GFException Build<T0, T1>(Exception innerException, string message, string translationKey, T0 arg0, T1 arg1, [CallerMemberName] string? callingMethod = null)
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

            if (innerException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(innerException), callingMethod), "You must supply an inner exception");
            }

            return new GFException(message, innerException, translationKey, new object[] { arg0, arg1 });
        }

        /// <summary>
        /// Combines the argument name with the calling method
        /// </summary>
        /// <param name="argumentName">The argument name</param>
        /// <param name="callingMethod">The calling method name</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="argumentName"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="callingMethod"/> argument is null</exception>
        public static string BuildArgumentName(string argumentName, string? callingMethod)
        {
            if (string.IsNullOrEmpty(argumentName))
            {
                throw new ArgumentNullException(nameof(argumentName), "You must supply an argument name");
            }

            if (string.IsNullOrEmpty(callingMethod))
            {
                return argumentName;
            }
            else
            {
                return $"{argumentName} (called by {callingMethod})";
            }
        }



        /// <summary>
        /// Gets the translated message. If no translations exist or a translation can't be found then the base message is returned
        /// </summary>
        /// <returns>A <see cref="string"/> containing the translated message or the base message if a translation cannot be found</returns>
        private string TranslateMessage()
        {
            if (this.MessageTranslations == null)
            {
                return base.Message;
            }
            else
            {
                var translatedMessage = GFTranslation.GetTranslation(this.MessageTranslations);
                return string.IsNullOrEmpty(translatedMessage) ? base.Message : translatedMessage;
            }
        }

        #endregion
    }
}