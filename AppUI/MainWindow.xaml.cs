using ClassModels;
using CodeFirstModel.Data;
using System;
using System.Globalization;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace AppUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private ICrudOperations Operations { get; }

        public MainWindow(ICrudOperations operations)
        {
            InitializeComponent();
            Operations = operations;
        }

        // Window load event
        private async void MainWindow_OnLoaded(object sender, RoutedEventArgs e)
        {
            await Task.Run(() => Operations.ReadEmployee());
            await Task.Run(() => Operations.ReadDepartment());
            //this.EmployeeTable.ItemsSource = Operations.EmployeeData;

            this.DataContext = Operations;
        }

        // Adds new Employee
        private void AddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.FirstNameTxt.Text) ||
                string.IsNullOrWhiteSpace(LastNameTxt.Text) ||
                string.IsNullOrWhiteSpace(EmailTxt.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTxt.Text) ||
                string.IsNullOrWhiteSpace(DateTxt.Text) ||
                string.IsNullOrWhiteSpace(this.SalaryTxt.Text))
            {
                MessageBox.Show("Fill all the Fields first");
                return;
            }

            try
            {
                var department = (this.DepartmentCombo.SelectedIndex == -1) ? null : this.DepartmentCombo.Text;
                Operations.InsertEmployee(this.FirstNameTxt.Text, this.LastNameTxt.Text, this.EmailTxt.Text, long.Parse(this.PhoneNumberTxt.Text), this.DateTxt.DisplayDate, decimal.Parse(SalaryTxt.Text), department);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                MessageBox.Show("Employee added!");
            }
        }

        // Deletes an Employee
        private void DeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.EmployeeTable.SelectedItem == null)
            {
                MessageBox.Show("Select an Employee to delete from the Table");
                return;
            }

            try
            {
                var id = ((IEmployee)this.EmployeeTable.SelectedItem).EmployeeId;
                Operations.DeleteEmployee(id);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                MessageBox.Show("Employee deleted");
            }
        }

        // Updates an Employee record
        private void UpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.EmployeeTable.SelectedItem == null)
            {
                MessageBox.Show("Select an Employee to update from the Table");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.FirstNameTxt.Text) ||
                string.IsNullOrWhiteSpace(LastNameTxt.Text) ||
                string.IsNullOrWhiteSpace(EmailTxt.Text) ||
                string.IsNullOrWhiteSpace(PhoneNumberTxt.Text) ||
                string.IsNullOrWhiteSpace(DateTxt.Text) ||
                string.IsNullOrWhiteSpace(this.SalaryTxt.Text))
            {
                MessageBox.Show("Fill all the Fields first");
                return;
            }

            var id = ((IEmployee)this.EmployeeTable.SelectedItem).EmployeeId;
            try
            {
                var department = (this.DepartmentCombo.SelectedIndex == -1) ? null : this.DepartmentCombo.Text;
                Operations.UpdateEmployee(id, this.FirstNameTxt.Text, this.LastNameTxt.Text, this.EmailTxt.Text, long.Parse(this.PhoneNumberTxt.Text), this.DateTxt.DisplayDate, decimal.Parse(SalaryTxt.Text), department);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                MessageBox.Show("Employee updated!");
            }
        }

        // Refresh Employees Table
        private async void RefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            this.FirstNameTxt.Text = this.LastNameTxt.Text = this.EmailTxt.Text = this.PhoneNumberTxt.Text = this.DateTxt.Text = this.SalaryTxt.Text = string.Empty;
            this.DepartmentCombo.SelectedIndex = -1;
            try
            {
                await Task.Run(() => Operations.ReadEmployee());
                this.EmployeeTable.ItemsSource = Operations.EmployeeData;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Employee Table selection changed event
        private void EmployeeTable_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (this.EmployeeTable.SelectedItem is IEmployee employee)
                {
                    this.FirstNameTxt.Text = employee.FirstName;
                    this.LastNameTxt.Text = employee.LastName;
                    this.EmailTxt.Text = employee.Email;
                    this.PhoneNumberTxt.Text = employee.PhoneNumber.ToString();
                    this.DateTxt.Text = employee.HireDate.ToString(CultureInfo.InvariantCulture);
                    this.SalaryTxt.Text = employee.Salary.ToString(CultureInfo.InvariantCulture);
                    //this.DepartmentCombo.Text =  Operations.DepartmentData
                    //.First(dept => dept.DepartmentId == employee.DepartmentId).DepartmentName;
                    //employee.DepartmentId == null? this.DepartmentCombo.SelectedIndex = -1 :
                    //if (employee.DepartmentId == null)
                    //{
                    //    this.DepartmentCombo.SelectedIndex = -1;
                    //}
                    //else
                    //{
                    //    this.DepartmentCombo.Text = Operations.DepartmentData
                    //        .First(dept => dept.DepartmentId == employee.DepartmentId).DepartmentName;
                    //}
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Adds new Department
        private void DeptAddBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(this.DepartmentNameTxt.Text))
            {
                MessageBox.Show("Input a department name");
                return;
            }

            try
            {
                Operations.InsertDepartment(this.DepartmentNameTxt.Text);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                MessageBox.Show("Department added!");
            }
        }

        // Deletes a Department
        private void DeptDeleteBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.DepartmentTable.SelectedItem == null)
            {
                MessageBox.Show("Select an Department to delete from the Table");
                return;
            }

            try
            {
                int id = ((IDepartment)this.DepartmentTable.SelectedItem).DepartmentId;
                Operations.DeleteDepartment(id);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
            finally
            {
                MessageBox.Show("Department deleted");
            }
        }

        // Updates d Department record
        private void DeptUpdateBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (this.DepartmentTable.SelectedItem == null)
            {
                MessageBox.Show("Select an Department to update from the Table");
                return;
            }

            if (string.IsNullOrWhiteSpace(this.DepartmentNameTxt.Text))
            {
                MessageBox.Show("Input a department name");
                return;
            }

            try
            {
                int id = ((IDepartment)this.DepartmentTable.SelectedItem).DepartmentId;
                Operations.UpdateDepartment(id, this.DepartmentNameTxt.Text);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
                throw;
            }
            finally
            {
                MessageBox.Show("Department updated!");
            }
        }

        // Refreshes Departments Table
        private async void DeptRefreshBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                await Task.Run(() => Operations.ReadDepartment());
                this.DepartmentTable.ItemsSource = Operations.DepartmentData;
                this.DepartmentCombo.ItemsSource = Operations.DepartmentList;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Department Table selection changed event
        private void DepartmentTable_OnSelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            try
            {
                if (this.DepartmentTable.SelectedItem is IDepartment department)
                {
                    this.DepartmentNameTxt.Text = department.DepartmentName;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        ////////////////////////////////////////////////////////////////////////////////////////////////////
        // Fetch record added and display all Employees and their department name
        private void AllEmployeeAndDepartment_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.EmployeeView.Visibility = Visibility.Visible;
                this.GroupedItemTxtBox.Visibility = Visibility.Hidden;
                this.EmployeeView.ItemsSource = Operations.AllRecordsWithDepartmentName();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Display records grouped by departments
        private void RecordsGroupedBtDepartments_OnClick(object sender, RoutedEventArgs e)
        {
            // StringBuilder toDisplay;
            try
            {
                this.EmployeeView.Visibility = Visibility.Hidden;
                this.GroupedItemTxtBox.Visibility = Visibility.Visible;
                this.GroupedItemTxtBox.Text = Operations.RecordsGroupedByDepartment();
                //this.EmployeeView.ItemsSource = Operations.RecordsGroupedByDepartment();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Fetch all Employees with salary above 150,000
        private void EmployeeAbove_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.EmployeeView.Visibility = Visibility.Visible;
                this.GroupedItemTxtBox.Visibility = Visibility.Hidden;
                this.EmployeeView.ItemsSource = Operations.EmployeesWWitheSalaryAbove();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Fetch all departments not assigned any employee
        private void DepartmentsWithoutEmployee_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.EmployeeView.Visibility = Visibility.Visible;
                this.GroupedItemTxtBox.Visibility = Visibility.Hidden;
                this.EmployeeView.ItemsSource = Operations.DepartmentsNotAssigned();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        // Fetch all employees and departments with assigned or not assigned
        private void AllNullAndNotNullEmployees_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                this.EmployeeView.Visibility = Visibility.Visible;
                this.GroupedItemTxtBox.Visibility = Visibility.Hidden;
                this.EmployeeView.ItemsSource = Operations.AllEmployeesWithDepartments();
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }
}