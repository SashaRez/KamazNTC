using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PCHDD
    {
        [Key]
        public int? ID { get; set; }


        [Column("HDD")]
        public string? HDD { get; set; }
    }

}
