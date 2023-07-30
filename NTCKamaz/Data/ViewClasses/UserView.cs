using System.ComponentModel;

namespace NTCKamaz.Data.ViewClasses
{
    public class UserView
    {
        public int ID { get; set; }

        [DisplayName("Табельный номер")]
        public string? TableNumber { get; set; }

        [DisplayName("ФИО")]
        public string? UserName { get; set; }

        [DisplayName("Кабинет")]
        public string? Cabinet { get; set; }

        [DisplayName("Должность")]
        public string? Position { get; set; }

        [DisplayName("Отдел(сокр.)")]
        public string? Department { get; set; }

        [DisplayName("Учётная запись")]
        public string? Account { get; set; }

        [DisplayName("ID ПК")]
        public string? pcID { get; set; }


    }
}
