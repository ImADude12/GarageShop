using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public class Branch
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Name")]
        [StringLength(10, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 10")]
        [DisplayName("Branch name")]
        public string Name { get; set; }

        // TODO: Should it be string?
        // (Need to be passed to google maps and show location)
        [Required(ErrorMessage = "Please Enter Adress")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        public string Address { get; set; }

        [Required(ErrorMessage = "Please Enter Phone Number")]
        [RegularExpression(@"^([0-9]{10})$", ErrorMessage = "Invalid Phone Number.")]
        [DataType(DataType.PhoneNumber)]
        [DisplayName("Phone Number")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "Please Enter Latitude")]
        [Range(-90.00, 90.00, ErrorMessage = "Must be between -90 to 90")]
        public string Latitude { get; set; }

        [Required(ErrorMessage = "Please Enter Longitude")]
        [Range(-180.00,180.00, ErrorMessage = "Must be between -180 to 180")]
        public string Longitude { get; set; }
    }
}
