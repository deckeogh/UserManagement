using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace UserManagement.Models
{
    public class UserDetails
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        [Key
        , Display(Name = "User Id")
        , DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }
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
