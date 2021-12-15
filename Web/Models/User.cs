using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    public class User
    {
        public int UserId { get; set; }

        [Required(ErrorMessage = "Необходимо ввести имя")]
        [MinLength(4, ErrorMessage = "Имя должно состоять из не менее 4х символов")]
        public string UserName { get; set; }

        [Range(0, 5, ErrorMessage = "Количество не должно превышать 5ти")]
        public int NumberOfPsychic { get; set; } 

        [Range(10, 99, ErrorMessage = "Значения должны быть от 10 до 99")]
        public int DesiredValue { get; set; } = 10;
    }
}
