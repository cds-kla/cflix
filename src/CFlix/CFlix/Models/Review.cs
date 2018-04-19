using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models
{
    public class Review
    {
        public Review()
        {
        }

        public Review(int mediaId)
        {
            this.MediaId = mediaId;
        }

        public int Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Column(TypeName = "TIMESTAMP")]
        public DateTime LastUpdated { get; set; }

        public int MediaId { get; set; }

        public Media Media { get; set; }

        public bool IsHidden { get; set; }

        [NotMapped]
        public CFlixUser User { get; set; }
    }
}
