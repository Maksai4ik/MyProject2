using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyProject2.Models
{
    public class Driver
    {
        public int Id { get; set; }
        [RegularExpression(@"^[А-Я]+[а-яА-Я0-9""'\s-]*$", ErrorMessage = "Названия должно начинаться с заглавной буквы и без небуквенных символов")]
        [Required(ErrorMessage = "Это обязательное для заполнения поле")]
        [StringLength(30, ErrorMessage = "Длина названия должна быть до 30 символов")]
        [Display(Name = "Имя водителя")]
        public string? DiverName { get; set; }
        [Required]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Номер телефона")]
        [StringLength(11, MinimumLength = 11, ErrorMessage = "Длина названия должна быть до 11 символов")]
        public string? PhoneNumber {  get; set; }
        [Display(Name = "Рейтинг таксиста")]
        [Range(1, 5, ErrorMessage = "Введи рейтинг от 1 до 5")]
        [Required(ErrorMessage = "Это обязательное для заполнения поле")]
        public string? Rating { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Driver()
        {
            Orders = new List<Order>();
        }
    }
}
