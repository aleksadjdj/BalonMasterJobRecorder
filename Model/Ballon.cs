using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Model
{
    [Table("BalonDatabase")]
    public class Ballon
    {
        [Key]
        [Column(Order = 0)]
        public int ID                   { get; set; }

        public string Date              { get; set; }
        public string Store             { get; set; }
        public string Dimension         { get; set; }
        public string Color             { get; set; }
        public string Description       { get; set; }

        public DateTime QueryInputDate  { get; set; }
    }
}
