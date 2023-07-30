using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace NTCKamaz.Data.Clasees
{
    public class PC
    {

        [Key]
        public string? ID { get; set; }

        [Column("Имя ПК")]
        public string? pcName { get; set; }

        [Column("Дата выдачи")]
        public string? issueDate { get; set; }

        [Column("ID Пользователя")]
        public int? userID { get; set; }

        [Column("ID Отдела")]
        public int? departmentID { get; set; }

        [Column("ID Марки")]
        public int? markID { get; set; }

        [Column("SN Системного блока")]
        public string? snSystemBlock { get; set; }

        [Column("ID Сборки")]
        public int? assemblyID { get; set; }

        [Column("ID Поставщика")]
        public int? providerID { get; set; }

        [Column("ID Категории")]
        public int? categoryID { get; set; }

        [Column("ID OC")]
        public int? ocID { get; set; }

        [Column("ID Инвентарного номера")]
        public int? inventoryNumberID { get; set; }

        [Column("Скл №ПК")]
        public string? skladtNumber { get; set; }

        [Column("ID Монитора")]
        public int? monitorID { get; set; }

        [Column("SN монитора")]
        public string? monitorSN { get; set; }

        [Column("ID переферийного устройства")]
        public int? additionallDeviceID { get; set; }

        [Column("Кабинет")]
        public string? cabinet { get; set; }

        [Column("ID МОЛ")]
        public int? molID { get; set; }

        [Column("ID Проекта")]
        public int? projectID { get; set; }

        [Column("Основание")]
        public string? foundation { get; set; }

        [Column("Примечание")]
        public string? note { get; set; }
    }

}

