using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Library.Models
{
    public class CardFile
    {
        public int CardFileID { get; set; }
      
    public string ReaderName { get; set; }

public string Email     { get; set; }
         
        public int BookId    { get; set; }
        public DateTime? DateIn { get; set; }
        public DateTime? DateReturn{ get; set; }




    }
}