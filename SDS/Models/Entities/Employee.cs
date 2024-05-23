using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SDS.Models.Entities
{
    public class Employee
    {
        [Key]
        public int ID { get; set; }
        public string EmpNo { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string ContactNo { get; set; }
        public string EmailAddress { get; set; }

    }
}