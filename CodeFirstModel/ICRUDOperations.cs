using ClassModels;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CodeFirstModel.Data
{
    public interface ICrudOperations
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
        public void InsertEmployee(string firstName, string lastName, string email, long phone, DateTime dateAdded,
            decimal salary, string department);

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
        public void UpdateEmployee(int id, string firstName, string lastName, string email, long phone,
            DateTime dateAdded, decimal salary, string department);

        /// <summary>
        /// Deletes an employee data from the database
        /// </summary>
        /// <param name="id">Employee Id</param>
        public void DeleteEmployee(int id);

        /// <summary>
        /// Reads all employee data from the database
        /// </summary>
        public void ReadEmployee();

        /// <summary>
        /// Inserts a department data into the database
        /// </summary>
        /// <param name="departmentName">Department Name</param>
        public void InsertDepartment(string departmentName);

        /// <summary>
        /// Deletes a department data from the database
        /// </summary>
        /// <param name="id">Department Id</param>
        public void DeleteDepartment(int id);

        /// <summary>
        /// Updates a department data from the database
        /// </summary>
        /// <param name="id">Department Id</param>
        /// <param name="departmentName">Department Name</param>
        public void UpdateDepartment(int id, string departmentName);

        /// <summary>
        /// Reads all department from the database
        /// </summary>
        public void ReadDepartment();

        /// <summary>
        /// Reads all record with their department name
        /// </summary>
        /// <returns>The returned record collection</returns>
        public IEnumerable AllRecordsWithDepartmentName();

        /// <summary>
        /// Reads all record grouped by their department name
        /// </summary>
        /// <returns>The returned grouped collection</returns>
        public string RecordsGroupedByDepartment();

        /// <summary>
        /// Reads the record of employee with salary above 150,000
        /// </summary>
        /// <returns>The returned collection on record</returns>
        public IEnumerable EmployeesWWitheSalaryAbove();

        /// <summary>
        /// Reads all departments not assigned to an Employee
        /// </summary>
        /// <returns>The returned collection</returns>
        public IEnumerable DepartmentsNotAssigned();

        /// <summary>
        /// Reads all employees and their Departments, both null and not null
        /// </summary>
        /// <returns></returns>
        public IEnumerable AllEmployeesWithDepartments();
    }
}