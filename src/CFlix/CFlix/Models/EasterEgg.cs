using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models
{
    public class EasterEgg
    {
        public EasterEgg()
        {
        }

        public EasterEgg(string title, EasterEggType easterEggType, string description, string hash)
        {
            EasterEggType = easterEggType;
            Title = title;
            Description = description;
            Hash = hash;
        }

        public int Id { get; set; }

        [MaxLength(200)]
        public string Title { get; set; }

        public string Description { get; set; }

        [MaxLength(64)]
        public string Hash { get; set; }

        public EasterEggType EasterEggType { get; set; }

        public bool IsAvailable { get; set; }
    }

    public enum EasterEggType : short
    {
        None = 0,
        Challenge = 1,
        Bonus = 2
    }
}
