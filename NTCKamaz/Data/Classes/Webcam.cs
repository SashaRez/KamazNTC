using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class Webcam
    {
        [Key]
        public int ID { get; set; }

        [Column("Веб-камера")]
        public string? webcam { get; set; }
    }
}