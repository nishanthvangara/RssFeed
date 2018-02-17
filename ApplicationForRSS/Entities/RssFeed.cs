using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationForRSS.Entities
{
    public class RssFeed
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]        
        public string Source { get; set; }

        public string Header { get; set; }

        public string Description { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime CreateDateTime { get; set; }
    }
}