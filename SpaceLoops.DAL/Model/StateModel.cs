using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceLoops.DAL.Model
{
    public class StateModel
    {
        public Guid Id { get; set; }
        public string? StateName { get; set; }
        public Guid CountryId { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime? CreatedOn { get; set; }
        public DateTime? UpdatedOn { get; set; }
        public virtual string CountryName { get; set; }
    }
}
