using System.ComponentModel.DataAnnotations.Schema;

namespace WebApp_Products.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
         
        public int CategoryID { get; set; }
        [ForeignKey("CategoryID")]
        public  Category? Category{ get; set; }
    }
}
