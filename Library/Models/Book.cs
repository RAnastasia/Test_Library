using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class Book
    {
        public int? BookId { get; set; }
        public string BookName { get; set; }
        public string AuthorName { get; set; }
        public int Quantity { get; set; }
        public string Availability { get; set; }


    }

    }
