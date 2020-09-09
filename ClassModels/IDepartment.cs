namespace ClassModels
{
    public interface IDepartment
    {
        public int DepartmentId { get; set; }

        public string DepartmentName { get; set; }

        //public ICollection<Employee> Employees { get; set; }
    }
}