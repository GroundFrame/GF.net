using GF.Common;
using GF.Common.Translations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace GF.Common
{
    public class GFUser
    {
        private string _key = default!;
        private string _firstName = default!;
        private string _lastName = default!;
        private string _email = default!;
        private string _username = default!;

        /// <summary>
        /// Gets or sets the user's key
        /// </summary>
        public string Key { get { return this._key; } set { if (string.IsNullOrEmpty(value)) { throw GFException.Build("The user's key cannot be null", "GFUser.Property.Null", nameof(Key)); } else { this._key = value; } } }

        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
        public string FirstName { get { return this._firstName; } set { if (string.IsNullOrEmpty(value)) { throw GFException.Build("The user's first name cannot be null", "GFUser.Property.Null", nameof(FirstName)); } else { this._firstName = value; } } }

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
        public string LastName { get { return this._lastName; } set { if (string.IsNullOrEmpty(value)) { throw GFException.Build("The user's last name cannot be null", "GFUser.Property.Null", nameof(LastName)); } else { this._lastName = value; } } }

        /// <summary>
        /// Gets or sets the user's email address
        /// </summary>
        public string Email { get { return this._email; } set { if (!value.IsValidEmail()) { throw GFException.Build("The email address must be a valid email address", "Generic.EmailValid"); } else { this._email = value; } } }

        /// <summary>
        /// Gets or sets the user's user name
        /// </summary>
        public string Username { get { return this._username; } set { if (string.IsNullOrEmpty(value)) { throw GFException.Build("The user's username cannot be null", "GFUser.Property.Null", nameof(Username)); } else { this._username = value; } } }

        /// <summary>
        /// Gets the users role
        /// </summary>
        public Role Role { get; set; }
    }

    public class GFUserEdit
    {
        private string _firstName = default!;
        private string _lastName = default!;

        /// <summary>
        /// Gets or sets the user's first name
        /// </summary>
        public string FirstName { get { return _firstName; } set { if (string.IsNullOrEmpty(value)) { GFException.Build("The user's first name cannot be null", "GFUser.Property.Null", nameof(FirstName)); } else { _firstName = value; } } }

        /// <summary>
        /// Gets or sets the user's last name
        /// </summary>
        public string LastName { get { return _lastName; } set { if (string.IsNullOrEmpty(value)) { GFException.Build("The user's last name cannot be null", "GFUser.Property.Null", nameof(LastName)); } else { _lastName = value; } } }
    }

    public class GFUserNew : GFUserEdit
    {
        private string _email = default!;
        private string _username = default!;

        /// <summary>
        /// Gets or sets the user's email address
        /// </summary>
        public string Email { get { return _email; } set { if (!value.IsValidEmail()) { GFException.Build("The email address must be a valid email address", "Generic.EmailValid"); } else { _email = value; } } }

        /// <summary>
        /// Gets or sets the user's user name
        /// </summary>
        public string Username { get { return _username; } set { if (string.IsNullOrEmpty(value)) { GFException.Build("The user's username cannot be null", "GFUser.Property.Null", nameof(Username)); } else { _username = value; } } }
    }
}
