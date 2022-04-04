using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2.Models
{
    public class BooksIndexViewModel
    {
        public IEnumerable<Book> Books { get; set; }
        public PageViewModel PageViewModel { get; set; }
    }
}
