using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace aventyrliga_kontakter.Model
{
    public class Contact
    {
        [Key]
        public int ContactId { get; set; }

        [Required(ErrorMessage = "E-post måste vara ifyllt."), DataType(DataType.EmailAddress, ErrorMessage = "Du måste ange en giltig e-post adress."), StringLength(50, ErrorMessage = "E-post får var högst 50 tecken."), EmailAddress(ErrorMessage = "Du måste ange en giltig e-post adress")]
        public string EmailAdress { get; set; }

        [Required(ErrorMessage = "Förnamn måste vara ifyllt."), StringLength(50, ErrorMessage = "Förnamn får vara högst 50 tecken.")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Efternamn måste vara ifyllt."), StringLength(50, ErrorMessage = "Efternamn får vara högst 50 tecken.")]
        public string LastName { get; set; }
    }
}