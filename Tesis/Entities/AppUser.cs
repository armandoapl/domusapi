﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Tesis.Extensions;

namespace Tesis.Entities
{
    public class AppUser
    {
        [Key]
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
        [StringLength(420)]
        public string Introduction { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public ICollection<Photo> Photos { get; set; }
        public ICollection<REProperty> Properties { get; set; }

        public int GetAge()
        {
            return DateOfBirth.CalculateAge();
        }
    }
}