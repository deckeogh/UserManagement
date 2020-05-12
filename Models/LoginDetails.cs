using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class LoginDetails
    {
        /// <summary>
        /// Gets or sets the email.
        /// </summary>
        /// <value>
        /// The email.
        /// </value>
        [MinLength(3)
        , MaxLength(100)
        , Display(Name = "Email Address")
        , Required()]
        public string Email { get; set; }
        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        /// <value>
        /// The password.
        /// </value>
        [MinLength(8)
        , MaxLength(50)
        , Required()]
        public string Password { get; set; }
    }
}
