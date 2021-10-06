using System;
using System.Collections.Generic;

namespace Tesis.Entities
{
    public class AppUser
    {        
        public int Id { get; set; }
        public string UserName { get; set; }
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string LegalId { get; set; }// cedula, string
        public DateTime DateOfBirth { get; set; }
        public string KnownAs { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime LastActive { get; set; } = DateTime.Now;
        public string Gender { get; set; }
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<REProperty> Properties { get; set; }

        public ICollection<TrustUser> TrustedbyUsers { get; set; }
        public ICollection<TrustUser> TrustedUsers { get; set; }
    }
}
