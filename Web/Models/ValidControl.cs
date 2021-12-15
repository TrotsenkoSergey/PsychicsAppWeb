using System;
using System.ComponentModel.DataAnnotations;

namespace Web.Models
{
    /// <summary>
    /// Модель для проверки соответствия вводимой пользователем информации-допустимой.
    /// </summary>
    public class ValidControl
    {
        [Range(2, 5, ErrorMessage = "Количество не может быть ниже 2х и выше 5ти")]
        public int NumberOfPsychic { get; set; } = 2;

        [Range(10, 99, ErrorMessage = "Значение должно быть от 10 до 99")]
        public int DesiredValue { get; set; } = 10;
    }
}
