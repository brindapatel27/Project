using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace importantTopics.Model
{
    public partial class Contain
    {
        [Column("TopicID")]
        public int TopicId { get; set; }
        [Column("KeywordID")]
        public int KeywordId { get; set; }

        [ForeignKey("KeywordId")]
        [InverseProperty("Contain")]
        public virtual Keywords Keyword { get; set; }
        [ForeignKey("TopicId")]
        [InverseProperty("Contain")]
        public virtual Topics Topic { get; set; }
    }
}
