﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WikiSystem.Services.DTOs.CollectionDocument
{
    public class GetCollectionDocumentResponse
    {
        List<CollectionDocumentInfo> CollectionDocuments { get; set; }
        public int TotalCount { get; set; }
    }
}
