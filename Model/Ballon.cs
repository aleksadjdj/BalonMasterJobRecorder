using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Table("Balloons")]
    public class Ballon
    {
        [Key]
        public int Id                   { get; set; }

        public string Date              { get; set; }
        public string Store             { get; set; }
        public string Dimension         { get; set; }
        public string Color             { get; set; }
        public string Description       { get; set; }

        public DateTime QueryInputDate  { get; set; }
    }
}
