using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Portfolio.Areas.Management.Models
{
    public class LanguageViewModel
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
        public ImageViewModel Image { get; set; }
    }
    public class CreateLanguageViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} title is requierd.")]
        public string Title { get; set; }

        [Display(Name = "Level")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public string Level { get; set; }

        //relations
        [Display(Name = "Image")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public IFormFile ImageFile { get; set; }
    }
    public class EditLanguageViewModel
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
        public int ImageId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}