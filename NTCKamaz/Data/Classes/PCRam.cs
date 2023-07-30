using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCRam
    {
        [Key]
        public int ID { get; set; }

        [Column("ОЗУ")]
        public string? RAM { get; set; }
    }
}