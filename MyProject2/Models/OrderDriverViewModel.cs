using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;
namespace MyProject2.Models
{
    public class OrderDriverViewModel
    {
        public SelectList? Drivers { get; set; }
        public List<Order> Orders { get; set; }
        public int? DriverID { get; set; }
        public string? SearchString { get; set; }
    }
}
