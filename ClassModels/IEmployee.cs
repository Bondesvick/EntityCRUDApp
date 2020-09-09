using System;

namespace ClassModels
{
    public interface IEmployee
    {
        public int EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public long PhoneNumber { get; set; }

        public DateTime HireDate { get; set; }

        public decimal Salary { get; set; }

        public int? DepartmentId { get; set; }

        //public Department Department { get; set; }
    }
}