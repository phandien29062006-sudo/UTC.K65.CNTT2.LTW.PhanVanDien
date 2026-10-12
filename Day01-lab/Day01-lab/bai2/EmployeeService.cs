using System;
using System.Collections.Generic;
using System.Linq;

namespace Day01_lab.Bai2
{
    public class EmployeeService
    {
        private List<Employee> employees = new List<Employee>();
        public void AddEmployee(Employee employee)
        {
            employees.Add(employee);
        }
        public List<Employee> GetAllEmployees()
        {
            return employees;
        }
        public double CalculateSalary(string maNhanVien)
        {
            Employee? employee = employees.FirstOrDefault(
                e => e.MaNhanVien == maNhanVien);

            if (employee == null)
            {
                return 0;
            }

            return employee.CalculateSalary();
        }
        public double GetTotalSalary()
        {
            double tongLuong = 0;

            foreach (Employee employee in employees)
            {
                tongLuong += employee.CalculateSalary();
            }

            return tongLuong;
        }
        public Employee? GetEmployeeWithHighestSalary()
        {
            if (employees.Count == 0)
            {
                return null;
            }

            Employee employeeMax = employees[0];

            foreach (Employee employee in employees)
            {
                if (employee.CalculateSalary() >
                    employeeMax.CalculateSalary())
                {
                    employeeMax = employee;
                }
            }

            return employeeMax;
        }
        public void SortBySalary()
        {
            employees.Sort(
                (e1, e2) =>
                    e2.CalculateSalary().CompareTo(e1.CalculateSalary())
            );
        }
        public Dictionary<string, int> CountEmployeesByDepartment()
        {
            Dictionary<string, int> result = new Dictionary<string, int>();

            foreach (Employee employee in employees)
            {
                if (result.ContainsKey(employee.PhongBan))
                {
                    result[employee.PhongBan]++;
                }
                else
                {
                    result[employee.PhongBan] = 1;
                }
            }

            return result;
        }
        public List<Employee> GetEmployeesWorkingOver3Years()
        {
            List<Employee> result = new List<Employee>();

            foreach (Employee employee in employees)
            {
                int soNamLamViec =
                    DateTime.Now.Year - employee.NgayVaoLam.Year;

                if (DateTime.Now < employee.NgayVaoLam.AddYears(soNamLamViec))
                {
                    soNamLamViec--;
                }

                if (soNamLamViec >= 3)
                {
                    result.Add(employee);
                }
            }

            return result;
        }
        
       
    

        public List<Employee> GetSalaryTable()
        {
            return new List<Employee>(employees);
        }
    }
}