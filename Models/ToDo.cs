using System;
using System.Collections.Generic;

namespace Dimension_Data.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? FinishDate { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
