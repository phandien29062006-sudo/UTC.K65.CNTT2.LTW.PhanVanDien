using System;
using System.Collections.Generic;

namespace Day01_lab.Bai2
{
    public class EmployeeConsoleView
    {
        public void ShowEmployees(List<Employee> employees)
        {
            if (employees.Count == 0)
            {
                Console.WriteLine("Danh sach nhan vien rong.");
                return;
            }

            foreach (Employee employee in employees)
            {
                Console.WriteLine("------------------------------");
                Console.WriteLine("Ma NV: " + employee.MaNhanVien);
                Console.WriteLine("Ho ten: " + employee.HoTen);
                Console.WriteLine("Phong ban: " + employee.PhongBan);
                Console.WriteLine("Ngay vao lam: " +
                    employee.NgayVaoLam.ToString("dd/MM/yyyy"));
                Console.WriteLine("Luong: " +
                    employee.CalculateSalary().ToString("N0"));
            }
        }
        public string InputEmployeeId()
        {
            Console.Write("Nhap ma nhan vien: ");
            return Console.ReadLine() ?? "";
        }
        public void ShowEmployee(Employee? employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Khong tim thay nhan vien.");
                return;
            }

            Console.WriteLine("------------------------------");
            Console.WriteLine("Ma NV: " + employee.MaNhanVien);
            Console.WriteLine("Ho ten: " + employee.HoTen);
            Console.WriteLine("Phong ban: " + employee.PhongBan);
            Console.WriteLine("Ngay vao lam: " +
                employee.NgayVaoLam.ToString("dd/MM/yyyy"));
            Console.WriteLine("Luong: " +
                employee.CalculateSalary().ToString("N0"));
        }
        public void ShowSalary(string maNhanVien, double salary)
        {
            if (salary == 0)
            {
                Console.WriteLine("Khong tim thay nhan vien.");
                return;
            }

            Console.WriteLine(
                "Luong cua nhan vien " +
                maNhanVien + ": " +
                salary.ToString("N0"));
        }
        public void ShowTotalSalary(double totalSalary)
        {
            Console.WriteLine(
                "Tong quy luong: " +
                totalSalary.ToString("N0"));
        }
        public void ShowHighestSalaryEmployee(Employee? employee)
        {
            if (employee == null)
            {
                Console.WriteLine("Danh sach nhan vien rong.");
                return;
            }

            Console.WriteLine("\nNHAN VIEN CO LUONG CAO NHAT:");
            Console.WriteLine("Ma NV: " + employee.MaNhanVien);
            Console.WriteLine("Ho ten: " + employee.HoTen);
            Console.WriteLine("Phong ban: " + employee.PhongBan);
            Console.WriteLine("Luong: " +
                employee.CalculateSalary().ToString("N0"));
        }
        public void ShowStatisticsByDepartment(
    Dictionary<string, int> statistics)
        {
            Console.WriteLine("\nTHONG KE THEO PHONG BAN:");

            if (statistics.Count == 0)
            {
                Console.WriteLine("Khong co du lieu.");
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    item.Key + " - " +
                    item.Value + " nhan vien");
            }
        }
        public void ShowEmployeesOver3Years(
    List<Employee> employees)
        {
            Console.WriteLine("\nNHAN VIEN LAM VIEC TREN 3 NAM:");

            if (employees.Count == 0)
            {
                Console.WriteLine("Khong co nhan vien nao.");
                return;
            }

            foreach (Employee employee in employees)
            {
                Console.WriteLine(
                    employee.MaNhanVien + " - " +
                    employee.HoTen + " - " +
                    employee.NgayVaoLam.ToString("dd/MM/yyyy"));
            }
        }
        public void ShowSalaryTable(List<Employee> employees)
        {
            Console.WriteLine(
                "\n================ BANG LUONG THANG ================");

            Console.WriteLine(
                "{0,-10} {1,-25} {2,-20} {3,15}",
                "Ma NV",
                "Ho Ten",
                "Phong Ban",
                "Luong");

            foreach (Employee employee in employees)
            {
                Console.WriteLine(
                    "{0,-10} {1,-25} {2,-20} {3,15:N0}",
                    employee.MaNhanVien,
                    employee.HoTen,
                    employee.PhongBan,
                    employee.CalculateSalary());
            }

            Console.WriteLine(
                "==================================================");
        }

    }
}