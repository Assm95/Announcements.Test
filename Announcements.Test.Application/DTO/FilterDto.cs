using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Test.Application.DTO
{
    public class FilterDto
    {
        public int? Number { get; set; }

        public int? Rating { get; set; }

        public Guid? UserId { get; set; }

        public FilterDateDto? CreatedAt { get; set; }

        public FilterDateDto? ExpirationDate { get; set; }
    }
}
