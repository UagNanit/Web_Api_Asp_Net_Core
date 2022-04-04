using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2.Models
{
    public class Book
    {
        
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required.")]
        public string Title { get; set; }

        [Display(Name = "Год публикации")]
        [Required(ErrorMessage = "Release date is required.")]
        public int ReleaseDate { get; set; }
       
        public int AuthorId { get; set; }
        public Author Author { get; set; }

        [Required(ErrorMessage = "Publisher is required.")]
        public string Publisher { get; set; }

        [Range(2, 18, ErrorMessage = "Недопустимый возраст")]
        [Required(ErrorMessage = "Age category is required.")]
        public int AgeCategory { get; set; }

        [Required(ErrorMessage = "Rating category is required.")]
        public double Rating { get; set; }
    }
    // Microsoft.EntityFrameworkCore.Design

}
