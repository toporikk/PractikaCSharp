using System;
using System.ComponentModel.DataAnnotations;

namespace PetTracker.Models
{
    public class Pet
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Введите имя питомца")]
        [Display(Name = "Имя питомца")]
        public string Name { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите тип животного")]
        [Display(Name = "Тип животного")]
        public string Type { get; set; } = string.Empty;

        [Required(ErrorMessage = "Укажите дату рождения")]
        [Display(Name = "Дата рождения")]
        [DataType(DataType.Date)]
        public DateTime Birthday { get; set; }
    }
}