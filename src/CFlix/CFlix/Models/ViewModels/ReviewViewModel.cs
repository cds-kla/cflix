using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class ReviewViewModel
    {
        //[Required]
        //public int MediaId { get; set; }

        [Required]
        [MinLength(5)]
        [MaxLength(4096)]
        public string Content { get; set; }
    }
}
