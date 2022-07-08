using System;
using System.Collections.Generic;
using System.Text;

namespace Lesson1_DAL.Models
{
    public class LibraryBooks : BaseEntity
    {
        public Guid RevisionId { get; set; }
        public BookRevision BookRevision { get; set; }
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
