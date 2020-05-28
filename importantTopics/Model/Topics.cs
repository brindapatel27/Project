using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace importantTopics.Model
{
    public partial class Topics
    {
        public Topics()
        {
            Contain = new HashSet<Contain>();
        }

        [Column("TopicID")]
        public int TopicId { get; set; }
        [Required]
        [StringLength(255)]
        public string TopicName { get; set; }

        [InverseProperty("Topic")]
        //[JsonIgnore]
        public virtual ICollection<Contain> Contain { get; set; }
    }
}
