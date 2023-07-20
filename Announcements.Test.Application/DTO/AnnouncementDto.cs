using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Test.Application.DTO
{
    public class AnnouncementDto
    {
        public Guid Id { get; set; }

        public int Number { get; set; }

        public UserDto User { get; set; } = null!;

        public string Text { get; set; } = null!;

        public FileDto? Image { get; set; }

        public int Rating { get; set; }

        public DateTime CreatedAt { get; set; }

        public DateOnly ExpirationDate { get; set; }
    }
}
