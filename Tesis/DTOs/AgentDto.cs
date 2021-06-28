using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tesis.Entities;

namespace Tesis.DTOs
{
    public class AgentDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string PhotoUrl { get; set; } // to be set as the main photo
        public string LegalId { get; set; }// cedula, string
        public int age { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreatedAt { get; set; } 
        public DateTime LastActive { get; set; } 
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<PhotoDto> Photos { get; set; }
        public ICollection<PropertyDto> Properties { get; set; }
    }
}
