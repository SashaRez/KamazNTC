using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class Chief
    {
        public int ID { get; set; }

        [Column("ФИО")]
        public string? Name { get; set; }
    }
}