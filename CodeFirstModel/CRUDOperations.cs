using ClassModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstModel.Data
{
    public class CrudOperations : ICrudOperations
    {
        public List<Employee> EmployeeData { get; set; }
        public List<Department> DepartmentData { get; set; }
        public List<string> DepartmentList { get; set; }

        /// <summary>
        /// Inserts an employee data into the database
        /// </summary>
        /// <param name="firstName">Employee First Name</param>
        /// <param name="lastName">Employee Second Name</param>
        /// <param name="email">Employee Email</param>
        /// <param name="phone">Employee Phone Number</param>
        /// <param name="dateAdded">Date Employee was added</param>
        /// <param name="salary">Employee Salary</param>
        /// <param name="department">Employee Department</param>
        public void InsertEmployee(string firstName, string lastName, string email, long phone, DateTime dateAdded, decimal salary, string department)
        {
            using (var context = new EmployeeContext())
            {
                int? deptId;
                if (department == null)
                {
                    deptId = null;
                }
                else
                {
                    deptId = context.Departments.ToList()
                        .Where(emp => emp.DepartmentName == department)
                        .Select(emp => emp.DepartmentId).Single();
                }

                var employee = new Employee()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = email,
                    PhoneNumber = phone,
                    HireDate = dateAdded,
                    Salary = salary,
                    DepartmentId = deptId
                };

                context.Employees.Add(employee);

                // or
                // context.Add<Student>(std);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes an employee data from the database
        /// </summary>
        /// <param name="id">Employee Id</param>
        public void DeleteEmployee(int id)
        {
            using (var context = new EmployeeContext())
            {
                var employee = context.Employees.ToList().Find(emp => emp.EmployeeId == id);
                //var employee = context.Employees.First<Employee>();
                context.Employees.Remove(employee ?? throw new InvalidOperationException());

                // or
                // context.Remove<Student>(std);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates an employee data in the database
        /// </summary>
        /// <param name="id">Employee Id</param>
        /// <param name="firstName">Employee First Name</param>
        /// <param name="lastName">Employee Second Name</param>
        /// <param name="email">Employee Email</param>
        /// <param name="phone">Employee Phone Number</param>
        /// <param name="dateAdded">Date Employee was added</param>
        /// <param name="salary">Employee Salary</param>
        /// <param name="department">Employee Department</param>
        public void UpdateEmployee(int id, string firstName, string lastName, string email, long phone, DateTime dateAdded, decimal salary, string department)
        {
            using (var context = new EmployeeContext())
            {
                int? deptTd;
                if (department == null)
                {
                    deptTd = null;
                }
                else
                {
                    deptTd = context.Departments.ToList()
                        .Where(emp => emp.DepartmentName == department)
                        .Select(emp => emp.DepartmentId).Single();
                }
                var employee = context.Employees.ToList().Find(emp => emp.EmployeeId == id);

                if (employee != null)
                {
                    employee.FirstName = firstName;
                    employee.LastName = lastName;
                    employee.Email = email;
                    employee.PhoneNumber = phone;
                    employee.HireDate = dateAdded;
                    employee.Salary = salary;
                    employee.DepartmentId = deptTd;
                }

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Reads all employee data from the database
        /// </summary>
        public void ReadEmployee()
        {
            EmployeeContext context = new EmployeeContext();
            this.EmployeeData = context.Employees.ToList();
        }

        /// <summary>
        /// Inserts a department data into the database
        /// </summary>
        /// <param name="departmentName">Department Name</param>
        public void InsertDepartment(string departmentName)
        {
            using (var context = new EmployeeContext())
            {
                var department = new Department()
                {
                    DepartmentName = departmentName
                };

                context.Departments.Add(department);

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Deletes a department data from the database
        /// </summary>
        /// <param name="id">Department Id</param>
        public void DeleteDepartment(int id)
        {
            using (var context = new EmployeeContext())
            {
                var department = context.Departments.ToList().Find(dept => dept.DepartmentId == id);
                context.Departments.Remove(department ?? throw new InvalidOperationException());

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Updates a department data from the database
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <param name="departmentName">Department Name</param>
        public void UpdateDepartment(int id, string departmentName)
        {
            using (var context = new EmployeeContext())
            {
                var department = context.Departments.ToList().Find(dept => dept.DepartmentId == id);
                if (department != null) department.DepartmentName = departmentName;

                context.SaveChanges();
            }
        }

        /// <summary>
        /// Reads all department from the database
        /// </summary>
        public void ReadDepartment()
        {
            EmployeeContext context = new EmployeeContext();
            this.DepartmentData = context.Departments.ToList();
            DepartmentList = context.Departments.Select(dept => dept.DepartmentName).ToList();
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
        /// <summary>
        /// Reads all record with their department name
        /// </summary>
        /// <returns>The returned record collection</returns>
        public IEnumerable AllRecordsWithDepartmentName()
        {
            return EmployeeData.Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Phone = emp.PhoneNumber,
                HireDate = emp.HireDate.ToLongDateString(),
                Salary = emp.Salary,
                Department = (emp.DepartmentId == null) ? null : DepartmentData.First(dept => dept.DepartmentId == emp.DepartmentId).DepartmentName
            });
        }

        /// <summary>
        /// Reads all records grouped by their department name
        /// </summary>
        /// <returns>The returned grouped collection</returns>
        public string RecordsGroupedByDepartment()
        {
            string final = "";
            var toGroup = EmployeeData.Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Email = emp.Email,
                Phone = emp.PhoneNumber,
                HireDate = emp.HireDate.ToLongDateString(),
                Salary = emp.Salary,
                Department = (emp.DepartmentId == null) ? null : DepartmentData.First(dept => dept.DepartmentId == emp.DepartmentId).DepartmentName
            }).GroupBy(item => item.Department).ToList();

            toGroup.ForEach(group =>
            {
                //string key = group.Key == null ? "Null" : group.Key;
                final += "\n Department - " + (@group.Key ?? "Null") + ":\n";
                final += " FirstName || LastName || Email || PhoneNumber || HireDate || Salary || Department\n";

                group.ToList().ForEach(item =>
                {
                    final += $" {item.FirstName}    {item.LastName}     {item.Email}    {item.Phone}    {item.HireDate} {item.Salary}   {item.Department} \n";
                });
            });

            return final;
        }

        /// <summary>
        /// Reads the record of employee with salary above 150,000
        /// </summary>
        /// <returns>The returned collection on record</returns>
        public IEnumerable EmployeesWWitheSalaryAbove()
        {
            return EmployeeData.Where(emp => emp.Salary > 150000).Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Salary = emp.Salary
            });
        }

        /// <summary>
        /// Reads all departments not assigned to an Employee
        /// </summary>
        /// <returns>The returned collection</returns>
        public IEnumerable DepartmentsNotAssigned()
        {
            var allEmployeeIds = EmployeeData.Select(emp => emp.DepartmentId).ToList();
            return DepartmentData.Where(dept => allEmployeeIds.Contains(dept.DepartmentId) == false);
        }

        /// <summary>
        /// Reads all employees and their Departments, both null and not null
        /// </summary>
        /// <returns>The returned collection</returns>
        public IEnumerable AllEmployeesWithDepartments()
        {
            //EmployeeContext context = new EmployeeContext();
            //return context.Employees.Include(d => d.Department.DepartmentName).ToList();
            //return context.Database.ExecuteSqlRaw("SELECT FirstName, DepartmentName FROM Employees FULL JOIN Departments ON Employees.DepartmentId = Departments.DepartmentId").ToString();//.ToList();

            return EmployeeData.Select(emp => new
            {
                FirstName = emp.FirstName,
                LastName = emp.LastName,
                Department = (emp.DepartmentId == null) ? null : DepartmentData.First(dept => dept.DepartmentId == emp.DepartmentId).DepartmentName
            });
        }
    }
}