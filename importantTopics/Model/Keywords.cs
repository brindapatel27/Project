using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace importantTopics.Model
{
    public partial class Keywords
    {
        public Keywords()
        {
            Contain = new HashSet<Contain>();
        }

        [Column("KeywordID")]
        public int KeywordId { get; set; }
        [Required]
        [StringLength(255)]
        public string KeywordName { get; set; }

        [InverseProperty("Keyword")]

        [JsonIgnore]
        public virtual ICollection<Contain> Contain { get; set; }
    }
}
