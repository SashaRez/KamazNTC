using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class MOLView
    {
     
        public int ID { get; set; }

        [DisplayName("ФИО")]
        public string? UserName { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }

    }
}