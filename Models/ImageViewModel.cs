using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Models
{
    public class ImageViewModel
    {
        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Title")]
        public string? Title { get; set; }

        [Display(Name = "Alt")]
        public string? Alt { get; set; }

        public string HtmlId { get; set; }
        public string HtmlClass { get; set; }
        public int? Height { get; set; }
        public int? Width { get; set; }

    }
}