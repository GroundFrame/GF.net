using FluentValidation;
using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.SimSig
{
    internal static class Ext_FluentValidation
    {
        /// <summary>
        /// Converts a <see cref="ValidationException"/> to a <see cref="GFException"/>
        /// </summary>
        /// <param name="validationException">The <see cref="ValidationException"/> to convert</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="validationException"/> is null</exception>
        public static GFException ToGFException(this ValidationException validationException, [CallerMemberName] string? callingMethod = null)
        {
            if (validationException == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(validationException), callingMethod), "You must supply a valiation exception to convert");
            }

            //string for holding the errors
            string errors = string.Empty;

            //loop around each error and add to the errors string
            foreach (var error in validationException.Errors)
            {
                errors += error.ToString() + Environment.NewLine;
            }
            return new GFException(errors);
        }
    }
}
