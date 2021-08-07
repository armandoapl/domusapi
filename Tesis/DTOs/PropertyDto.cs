using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesis.DTOs
{
    public class PropertyDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public int AppUserId { get; set; }
        public string Address { get; set; }
        public DateTime CreatedAt { get; set; }
        public string PhotoUrl { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
    }
}
