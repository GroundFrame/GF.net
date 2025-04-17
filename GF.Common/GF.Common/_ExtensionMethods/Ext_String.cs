using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Mail;
using System.Text.RegularExpressions;

namespace GF.Common
{
    public static partial class ExtMethods
    {
        /// <summary>
        /// Validates whether the string is a valid email address
        /// </summary>
        /// <param name="email">The email value to check</param>
        /// <returns>True if the string is a valid email address otherwise false</returns>
        public static bool IsValidEmail(this string email)
        {
            if (string.IsNullOrEmpty(email)) return false;

            if (string.IsNullOrWhiteSpace(email)) return false;

            if (!MailAddress.TryCreate(email, out var mailAddress))
                return false;

            // and if you want to be more strict:
            var hostParts = mailAddress.Host.Split('.');
            if (hostParts.Length == 1)
                return false; // no dot.
            if (hostParts.Any(p => p == string.Empty))
                return false; // double dot.
            if (hostParts[^1].Length < 2)
                return false; // TLD only one letter.

            if (mailAddress.User.Contains(' '))
                return false;
            if (mailAddress.User.Split('.').Any(p => p == string.Empty))
                return false; // double dot or dot at end of user part.

            return true;
        }

        /// <summary>
        /// Splits a string at every n characters into an <see cref="IEnumerable{string}"/>
        /// </summary>
        /// <param name="s">The source string</param>
        /// <param name="characters">The number of characters</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the source string is null or an empty character</exception>
        /// <exception cref="ArgumentException">Thrown if the number of characters is to split is less then or equal to zerio</exception>
        public static IEnumerable<String> SplitInParts(this string s, int characters)
        {
            if (string.IsNullOrEmpty(s))
                throw new ArgumentNullException(nameof(s), $"The source string cannot be null or an empty string.");
            if (characters <= 0)
                throw new ArgumentException("The characters argument must be greater than zero.", nameof(characters));

            for (var i = 0; i < s.Length; i += characters)
                yield return s.Substring(i, Math.Min(characters, s.Length - i));
        }
    }
}
