using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MVC_2.Models
{
    
    public class Author
    {
        
        public int Id { get; set; }

        [Display(Name = "FirstName")]
        public string FirstName { get; set; }
       
        [Display(Name = "SecondName")]
        public string SecondName { get; set; }

        [Display(Name = "FullName")]
        public string FullName { get { return FirstName + " " + SecondName; } }
   
        [JsonIgnore]
        public List<Book> Books { get; set; }
       
        public Author()
        {
            Books = new List<Book>();
        }
    }
}
