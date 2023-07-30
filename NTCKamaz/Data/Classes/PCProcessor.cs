using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCProcessor
    {
        [Key]
        public int ID { get; set; }


        [Column("Процессор")]
        public string? processorName { get; set; }
    }
}