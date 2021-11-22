using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CommentService.Models
{
    public class User
    {
        //[Required]
        [Display(Name = "User Name")]
        public string UserName { get; set; }

       // [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

      //  [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

      //  [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string EmailId { get; set; }

      //  [Required]
        [Display(Name = "Phone Number")]
        public string ContactNo { get; set; }

      //  [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

       // [Required]
        [Display(Name = "Date Of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }

      //  [Required]
        [Display(Name = "Gender")]
        public string Gender { get; set; }
    }
}
