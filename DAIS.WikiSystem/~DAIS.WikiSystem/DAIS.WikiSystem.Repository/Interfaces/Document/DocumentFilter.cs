﻿using DAIS.WikiSystem.Models.Enums;
using System.Data.SqlTypes;

namespace DAIS.WikiSystem.Repository.Interfaces.Document
{
    public class DocumentFilter
    {
        public bool? IsDeleted { get; set; }
        public SqlString? Title { get; set; }
        public SqlInt32? CreatorId { get; set; }
        public SqlInt32? CategoryId { get; set; }
        public AccessLevel? AccessLevel { get; set; }
    }
}
