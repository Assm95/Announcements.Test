﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Announcements.Test.Application.DTO
{
    public class PaginationDto
    {
        public int PageNumber { get; set; } = 0;

        public int PageSize { get; set; } = 10;
    }
}
