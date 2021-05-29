using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Entities
{
    public class REProperty
    {
        [Key]
        public int Id { get; set; }
        public string Description { get; set; }
        public AppUser Agent { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
