using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CFlix.Models
{
    public class Media
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public MediaType Type { get; set; }

        [DataType(DataType.ImageUrl)]
        public string ImageUri { get; set; }

        public List<Review> Reviews { get; set; }

        public string YouTubeId { get; set; }

        [Column(TypeName = "TIMESTAMP")]
        public DateTime ReleaseDate { get; set; }

        [NotMapped]
        public bool IsAvailable
        {
            get
            {
                return DateTime.Now >= ReleaseDate;
            }
        }
    }
}