using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Portfolio.Data.Entities
{
    public class Language
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} title is requierd.")]
        public string Title { get; set; }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public string Level { get; set; }

        //relations
        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }

        [Display(Name = "Image")]
        public Image Image { get; set; }
    }
}