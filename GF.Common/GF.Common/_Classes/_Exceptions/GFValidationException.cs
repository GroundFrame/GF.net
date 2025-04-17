using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common
{
    public class GFValidationException : GFException
    {
        /// <summary>
        /// Instantiates a new <see cref="GFValidationException"/> 
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="innerException">The inner exception</param>
        /// <param name="translationKey">The translation key</param>
        /// <param name="placeholders">The translation placeholders</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="message"/> argument is null or an empty string</exception>
        /// <exception cref="ArgumentNullException">Throw if the <paramref name="translationKey"/> argument is null or an empty string</exception>
        private GFValidationException(string message, Exception innerException, string translationKey, object[] placeholders, [CallerMemberName] string? callingMethod = null) : base(message, innerException, translationKey, placeholders) { }

        public static GFValidationException BuildValidationExeception<T>(Exception validationException)
        {
            return new GFValidationException("The {ObjectValidated} object has failed validation.", validationException, "Common.FailedValidation.Error", new object[] { typeof(T).Name });
        }
    }
}
