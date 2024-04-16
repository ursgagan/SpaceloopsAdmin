using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Model
{
    public class CityModel
    {
        public Guid Id { get; set; }
        public string? CityName { get; set; }
        public Guid StateId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public string? StateName { get; set; }
            
    }
}
