﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OfficeResourcesReservationSystem.Services.DTOs.Resource
{
    public class GetAllResourceResponse
    {
        public List<ResourceInfo> Resources { get; set; }
        public int? TotalCount { get; set; }
    }
}       
