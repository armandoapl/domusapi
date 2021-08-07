using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Tesis.Entities
{
    public class REProperty
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AppUserId { get; set; }
        public string Address { get; set; }
        public AppUser Agent { get; set; }
        public DateTime CreatedAt  { get; set; } = DateTime.Now;
        public DateTime LastUpdated { get; set; }
        public ICollection<Photo> Photos { get; set; }
    }
}
