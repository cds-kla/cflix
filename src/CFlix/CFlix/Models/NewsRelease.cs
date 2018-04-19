using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models
{
    public class NewsRelease
    {
        public int Id { get; set; }

        public string UserName { get; set; }

        public string Content { get; set; }

        public NewsReleaseType NewsReleaseType { get; set; }

        public DateTimeOffset CreationDate { get; set; }
    }

    public enum NewsReleaseType : short
    {
        Administrator = 1,
        Hacknonymousoflix = 2,
        SecurityTeam = 3,
    }
}
