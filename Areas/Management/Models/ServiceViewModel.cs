using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Portfolio.Areas.Management.Models
{
    public class ServiceViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} title is requierd.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public string Description { get; set; }

        //relations
        [ForeignKey(nameof(Image))]
        public int ImageId { get; set; }

        [Display(Name = "Image")]
        public ImageViewModel Image { get; set; }

    }
    public class CreateServiceViewModel
    {
        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} title is requierd.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public string Description { get; set; }

        //relations
        [Display(Name = "Image")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public IFormFile ImageFile { get; set; }
    }
    public class EditServiceViewModel
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "Title")]
        [Required(ErrorMessage = "{0} title is requierd.")]
        public string Title { get; set; }

        [Display(Name = "Description")]
        [Required(ErrorMessage = "{0} is requierd.")]
        public string Description { get; set; }

        //relations
        public int ImageId { get; set; }

        [Display(Name = "Image")]
        public IFormFile? ImageFile { get; set; }
    }
}