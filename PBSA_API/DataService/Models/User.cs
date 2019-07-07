using System;
using System.Collections.Generic;

namespace DataService.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Fullname { get; set; }
        public bool? IsActive { get; set; }
        public int RoleId { get; set; }
        public string Phone { get; set; }

        public virtual Role Role { get; set; }
    }
}
