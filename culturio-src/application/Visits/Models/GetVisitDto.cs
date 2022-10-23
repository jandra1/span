﻿using System;
namespace Culturio.Application.Visits.Models
{
    public class GetVisitDto
    {
        public string? SortOrder { get; set; }
        public string? SearchValue { get; set; }
        public int? Page { get; set; }
        public int? PageSize { get; set; }
        public int? UserId { get; set; }
    }
}


