using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace MyProject2.Models
{
    public class Car
    {
        public int Id { get; set; }
        [Required]
        [Display(Name = "Название машины")]
        public string? NameCar { get; set; }
        public ICollection<Order> Orders { get; set; }
        public Car()
        {
             Orders = new List<Order>();
        }

        
    }
}
