using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Lesson1_DAL.Models
{
    public class LibraryBooks : BaseEntity
    {
        [ForeignKey("Revision")]
        public Guid RevisionId { get; set; }
        public BookRevision BookRevision { get; set; }
        [ForeignKey("Library")]
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
