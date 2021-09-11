using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class ProductTagView
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please Enter Image Url")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Length Must Be Between 5 To 200")]
        [DataType(DataType.ImageUrl)]
        public String Image { get; set; }

        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        public string  Description{ get; set; }

        [Required(ErrorMessage = "Please Enter Price")]
        [Range(1, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        [DataType(DataType.Currency)]
        public int Price { get; set; }

        public int SellerId { get; set; }
        public Seller Seller { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public List<int> TagIds { get; set; }
        public IEnumerable<SelectListItem> TagsList { get; set; }
    }
}
