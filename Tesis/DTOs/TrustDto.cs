using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesis.DTOs
{
    public class TrustDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public int Age { get; set; }
        public string knownAs { get; set; }
        public string PhotoUrl { get; set; }
        public string city { get; set; }
    }
}
