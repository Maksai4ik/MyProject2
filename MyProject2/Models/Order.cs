using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyProject2.Models
{
    public class Order
    {
        public int Id { get; set; }
        [StringLength(60, MinimumLength = 3, ErrorMessage = "Длина названия должна быть от 3 до 60 символов")]
        [Required(ErrorMessage = "Это обязательное для заполнения поле")]
        [RegularExpression(@"^[а-яА-Я0-9a-zA-Z""'\s-]*$", ErrorMessage = "Названия должно начинаться с заглавной буквы")]
        [Display(Name = "Имя фирмы")]
        public string? NameFirm { get; set; }

        [Column(TypeName = "decimal(18, 0)")]
        [Display(Name = "Номер заказа")]
        [DisplayFormat(DataFormatString = "{0:N0}", ApplyFormatInEditMode = true)]

        public decimal OrderNumber { get; set; }
        [Required(ErrorMessage = "Это обязательное для заполнения поле")]
        [Range(1, 10000, ErrorMessage = "Цена не может быть такой дорогой")]
        [DataType(DataType.Currency, ErrorMessage = "Введи число")]
        [Display(Name = "Цена")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        [Display(Name = "Дата заказа")]
        [Required(ErrorMessage = "Это обязательное для заполнения поле")]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }
       
        public int? CarID { get; set; }
        [Display(Name = "Машина")]
        public Car? Car { get; set; }
        public int? DriverID { get; set; }
        [Display(Name = "Водитель")]
        public Driver? Driver { get; set; }
    }
}
