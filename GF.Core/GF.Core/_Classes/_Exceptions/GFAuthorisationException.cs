using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using GF.Common;

namespace GF.Core
{
    internal static class GFAuthorisationException
    {
        /// <summary>
        /// Builds a translated <see cref="GFAuthorisationException"/>
        /// </summary>
        /// <typeparam name="TGFEntity">The type of entity the user is trying to perform a CRUD action against</typeparam>
        /// <param name="user">The user who has insufficient rights</param>
        /// <param name="callingMethod">The calling method name. Do not pass</param>
        /// <returns>A translated <see cref="GFAuthorisationException"/></returns>
        /// <exception cref="ArgumentNullException">Thrown if the <paramref name="user"/> is null</exception>
        internal static Common.GFAuthorisationException Build<TGFEntity>(GFUserEntity user, [CallerMemberName] string? callingMethod = null)
        {
            if (user == null)
            {
                throw new ArgumentNullException(GFException.BuildArgumentName(nameof(user), callingMethod), "You must supply a user for the exception");
            }

            return new Common.GFAuthorisationException($"The user {user.Username} (Id: {user.Id}) does not have permission to perform this action on the {typeof(TGFEntity).Name}.", "Entity.InsufficientPermission", new object[] { user.Id, typeof(TGFEntity).Name });
        }
    }
}
