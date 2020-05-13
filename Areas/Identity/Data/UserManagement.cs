using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace UserManagement.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the UserManagement class
    public class UserManagement : IdentityUser
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [MinLength(3)
        , MaxLength(50)
        , Display(Name = "User Name")
        , Required()]
        public string Name { get; set; }
        /// <summary>
        /// Gets or sets the surnames.
        /// </summary>
        /// <value>
        /// The surnames.
        /// </value>
        [MinLength(3)
        , MaxLength(50)
        , Required()]
        public string Surnames { get; set; }
        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        /// <value>
        /// The country.
        /// </value>
        [MinLength(3)
        , MaxLength(50)
        , Required()]
        public string Country { get; set; }
        /// <summary>
        /// Gets or sets the phone.
        /// </summary>
        /// <value>
        /// The phone.
        /// </value>
        [MaxLength(50)
        , Display(Name = "Phone Number")
        , Required()]
        public string Phone { get; set; }
        /// <summary>
        /// Gets or sets the post code.
        /// </summary>
        /// <value>
        /// The post code.
        /// </value>
        [MinLength(3)
        , MaxLength(20)
        , Required()]
        public string PostCode { get; set; }
    }
}
