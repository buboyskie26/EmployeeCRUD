using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS.ViewModel
{
    public class EditEmployeeViewModel
    {

        public int ID { get; set; }
        [StringLength(6, ErrorMessage = "Employee number must be exactly 6 characters.")]
        /* This removed the spaces */
        [RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Employee number must be alphanumeric.")]
        [Required(ErrorMessage = "Employee number is required.")]
        [DisplayName("* Employee No.")]
        public string EmpNo { get; set; }

        [StringLength(15, ErrorMessage = "Firstname must not be exceed of 15 characters.")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Firstname must be alphanumeric with maximum 15 characters.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Firstname must contain only alphabetical characters.")]
        [Required(ErrorMessage = "Firstname is required.")]
        [DisplayName("* Firstname")]

        public string FirstName { get; set; }

        [StringLength(15, ErrorMessage = "Lastname must not be exceed of 15 characters.")]
        //[RegularExpression("^[a-zA-Z0-9]*$", ErrorMessage = "Lastname must be alphanumeric with maximum 15 characters.")]
        [RegularExpression("^[a-zA-Z ]*$", ErrorMessage = "Lastname must contain only alphabetical characters.")]
        [Required(ErrorMessage = "Lastname is required.")]

        [DisplayName("* Lastname")]
        public string LastName { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Birthdate field is required.")]
        [DisplayName("* Birthdate")]
        public DateTime BirthDate { get; set; }

        // [RegularExpression("^[0-9]*$", ErrorMessage = "Contact number must be numeric with 11 characters.")]
        [RegularExpression("^09[0-9]*$", ErrorMessage = "Contact number must start with '09' and should be numeric.")]
        [StringLength(11, ErrorMessage = "Contact number must be exactly 11 digits.")]
        [MinLength(11, ErrorMessage = "Contact number should be exactly 11 digits.")]
        [Required(ErrorMessage = "Contact number is required.")]
        [DisplayName("* Contact number")]
        public string ContactNo { get; set; }


        [Required(ErrorMessage = "Email address is required.")]
        [RegularExpression(@"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$", ErrorMessage = "Invalid Email address")]
        [DisplayName("* Email address")]
        public string EmailAddress { get; set; }

    }
}