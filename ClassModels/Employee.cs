using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ClassModels
{
    public class Employee : IEmployee
    {
        public int EmployeeId { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        [Required]
        [Column(TypeName = "datetime2")]
        public DateTime HireDate { get; set; }

        [Required]
        [Column(TypeName = "money")]
        public decimal Salary { get; set; }

        public int? DepartmentId { get; set; }

        //public Department Department { get; set; }
    }
}