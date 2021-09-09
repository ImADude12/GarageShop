using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GarageShop.Models
{
    public enum UserType
    {
        Client,
        Author,
        Editor,
        Admin
    }
    public class User
    {
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please Enter Username")]
        [StringLength(200, MinimumLength = 2, ErrorMessage ="Length Must Be Between 2 To 200")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Please Enter Password")]
        [StringLength(200, MinimumLength = 6, ErrorMessage = "Length Must Be Between 6 To 200")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please Enter Full Name")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Length Must Be Between 2 To 200")]
        [Display(Name = "Full Name")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Please Enter Email")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Length Must Be Between 5 To 200")]
        [DataType(DataType.EmailAddress)]
        public string Email{ get; set; }

        //TODO: Should I Add Datatype to validate only numbers?
        public UserType UserType { get; set; } = UserType.Client;

        public Cart Cart { get; set; }
    }
}
