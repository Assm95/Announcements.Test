using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Test.Application.DTO
{
    public class UserDto
    {
        public Guid Id { get; set; }

        public string Name { get; set; } = null!;

        public bool IsAdmin { get; set; }
    }
}
