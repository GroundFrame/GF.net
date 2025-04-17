using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common
{
    /// <summary>
    /// Enum listing the available user roles
    /// </summary>
    public enum Role
    {
        /// <summary>
        /// This is the default permission granted to a user. This has no permissions
        /// </summary>
        Default = 100,
        /// <summary>
        /// A standard GF player
        /// </summary>
        Player = 200,
        /// <summary>
        /// A GF player who also has permissions to edit a timetable
        /// </summary>
        WTTEditor = 300,
        /// <summary>
        /// A GF player who also has permissions to edit a network
        /// </summary>
        NetworkEditor = 400,
        /// <summary>
        /// A sys admin user
        /// </summary>
        SysAdmin = 500
    }
}
