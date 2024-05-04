using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DATA.ViewModels
{
    public class BorrowdBooks
    {
        [Key]
        public int BorrowID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string status { get; set; }
    }
}
