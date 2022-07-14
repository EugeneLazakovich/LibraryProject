using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lesson1_DAL.Models
{
    public class LibraryBooks : BaseEntity
    {
        [ForeignKey("Revision")]
        public Guid BookRevisionId { get; set; }
        public BookRevision BookRevision { get; set; }
        [ForeignKey("Library")]
        public Guid LibraryId { get; set; }
        public Library Library { get; set; }
    }
}
