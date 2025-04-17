using GF.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GF.Core.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal class GFMongoAttribute : Attribute
    {
        /// <summary>
        /// Gets the name mongo collection name for he entity
        /// </summary>
        public string CollectionName { get; private set; }

        /// <summary>
        /// Gets the minimum role to create the entity
        /// </summary>
        public Role CreateRole { get; private set; }

        /// <summary>
        /// Gets the minimum role to read the entity
        /// </summary>
        public Role ReadRole { get; private set; }

        /// <summary>
        /// Gets the minimum role to update the entity
        /// </summary>
        public Role UpdateRole { get; private set; }

        /// <summary>
        /// Gets the minimum role to delete the entity
        /// </summary>
        public Role DeleteRole { get; private set; }

        /// <summary>
        /// Gets the flag which indicates whether an entity owner can edit their own record
        /// </summary>
        /// <remarks>Really used to allow users to edit their own user record</remarks>
        public bool AllowOwnerEdit { get; private set; }

        public GFMongoAttribute(string collectionName, Role createRole, Role readRole, Role updateRole, Role deleteRole, bool allowOwnerEdit = false)
        {
            this.CollectionName = collectionName;
            this.CreateRole = createRole;
            this.ReadRole = readRole;
            this.UpdateRole = updateRole;
            this.DeleteRole = deleteRole;
            this.AllowOwnerEdit = allowOwnerEdit;
        }
    }
}
