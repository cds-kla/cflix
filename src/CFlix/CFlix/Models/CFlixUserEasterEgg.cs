using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models
{
    public class CFlixUserEasterEgg
    {
        public string CFlixUserId { get; set; }

        public CFlixUser CFlixUser { get; set; }

        public int EasterEggId { get; set; }

        public EasterEgg EasterEgg { get; set; }

        //[Column(TypeName = "timestamp")]
        public DateTimeOffset CreationDate { get; set; }

        [Range(0,5)]
        public short Rate { get; set; }
    }
}
