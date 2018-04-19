using CFlix.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Extensions
{
    public static class EnumExtension
    {
        public static string ToCSSType(this NewsReleaseType type)
        {
            switch (type)
            {
                case NewsReleaseType.Administrator:
                    return "positive";
                case NewsReleaseType.Hacknonymousoflix:
                    return "violet";
                case NewsReleaseType.SecurityTeam:
                    return "black";
                default:
                    return "";
            }
        }

        public static string ToCSSIcon(this NewsReleaseType type)
        {
            switch (type)
            {
                case NewsReleaseType.Administrator:
                    return "announcement";
                case NewsReleaseType.Hacknonymousoflix:
                    return "spy";
                case NewsReleaseType.SecurityTeam:
                    return "doctor";
                default:
                    return "";
            }
        }
    }
}
