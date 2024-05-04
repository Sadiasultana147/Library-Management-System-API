using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LMS_DATA.ViewModels
{
    public class BorrowdBooksViewModel
    {
        public int BorrowID { get; set; }
        public int MemberID { get; set; }
        public int BookID { get; set; }
        public DateTime BorrowDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public string status { get; set; }
        public string MemberName { get; set; }
        public string BookName { get; set; }
    }
}
