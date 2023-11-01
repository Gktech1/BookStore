using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace User.Domain.Entities
{
    public class User : EntityBase
    {
        public string? UserId { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Gender { get; set; }
        public string? PhoneNumber { get; set; }
        public int? Role { get; set; }
        public bool? IsBlocked { get; set; }
        public bool? Visibility { get; set; }
    }
}
