using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CFlix.Models.ViewModels
{
    public class ViewerViewModel
    {
        public string MediaId { get; set; }

        public string CurrentImage { get; set; }

        public List<string> Images { get; set; }
    }
}
