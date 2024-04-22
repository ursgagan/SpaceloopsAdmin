using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Entities
{
    public class Country
    {
        [Key]
        public Guid Id { get; set; }
        public string? CountryName { get; set; }
        public DateTime? CreatedDate { get; set; }
        public DateTime? UpdatedDate { get; set; } 
        public bool? IsDeleted { get; set; }
    }
}