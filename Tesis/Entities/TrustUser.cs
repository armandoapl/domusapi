using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tesis.Entities
{
    public class TrustUser
    {
        public AppUser SourceUser { get; set; }
        public int SourceUserId { get; set; }
        public AppUser TrustedUser { get; set; }
        public int TrustedUserId { get; set; }


    }
}
