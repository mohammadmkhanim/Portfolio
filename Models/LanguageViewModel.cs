using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Portfolio.Models
{
    public class LanguageViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        public string Title { get; set; }

        [Display(Name = "Level")]
        public string Level { get; set; }

        //relations
        [Display(Name = "Image")]
        public ImageViewModel Image { get; set; }
    }
}