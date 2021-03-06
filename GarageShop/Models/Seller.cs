using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class Seller
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        [Display(Name = "Seller name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Image Url")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Length Must Be Between 5 To 200")]
        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image url")]
        public string Image { get; set; }

        public List<Product> Products { get; set; }
    }
}
