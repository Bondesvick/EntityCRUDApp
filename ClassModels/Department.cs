using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ClassModels
{
    public class Department : IDepartment
    {
        public Department()
        {
            //Employees = new HashSet<Employee>();
        }

        public int DepartmentId { get; set; }

        [Required]
        public string DepartmentName { get; set; }

        //public ICollection<Employee> Employees { get; set; }
    }
}