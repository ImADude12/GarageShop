using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        public string Name { get; set; }

        // TODO: Should it be string?
        // (Need to be passed to google maps and show location)
        [Required(ErrorMessage = "Please Enter Url")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [DataType(DataType.PhoneNumber)]
        public string PhoneNumber { get; set; }

        // TODO: Add regex of only numbers
        [Required(ErrorMessage = "Please Enter Latitude")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "Please Enter Longitude")]
        public string Longitude { get; set; }
    }
}
