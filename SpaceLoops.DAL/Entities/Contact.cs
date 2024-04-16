using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Entity
{
    public class Contact
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "FirstName is Requird")]
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? EmailId { get; set; }
        public string? Message { get; set; }
        //public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public bool? IsDeleted { get; set; }
    }
}
