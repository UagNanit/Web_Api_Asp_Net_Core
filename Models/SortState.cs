using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_2.Models
{
    public enum SortState
    {
        TitleAsc,    // по имени по возрастанию
        TitleDesc,   // по имени по убыванию
        RatingAsc, // по рейтингу по возрастанию
        RatingDesc,    // по рейтингу по убыванию
    }
}
