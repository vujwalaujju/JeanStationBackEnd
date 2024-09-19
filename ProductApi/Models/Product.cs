using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductApi.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public  int Code{ get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public  string Image { get; set; }
     public  int StoreId { get; set; }
        public string Description { get; set; }  
        public string Gender { get; set; }

    }
}
