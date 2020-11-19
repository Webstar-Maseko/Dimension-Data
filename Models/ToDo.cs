using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Dimension_Data.Models
{
    public partial class ToDo
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Message { get; set; }
        [Display(Name ="Starting date of event")]
        public DateTime? StartDate { get; set; }
        [Display(Name = "Finishing date of event")]
        public DateTime? FinishDate { get; set; }
        [Display(Name = "Starting time of event")]
        public TimeSpan? StartTime { get; set; }
        [Display(Name = "Finishing time of event")]
        public TimeSpan? FinishTime { get; set; }
        public string UserId { get; set; }
        public string Color { get; set; }

        public virtual AspNetUsers User { get; set; }
    }
}
