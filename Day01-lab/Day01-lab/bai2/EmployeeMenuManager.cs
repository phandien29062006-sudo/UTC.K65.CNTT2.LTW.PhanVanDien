using System;
using System.Collections.Generic;
namespace Day01_lab.Bai2
{
    public class EmployeeMenuManager
    {
        private EmployeeService service;
        private EmployeeConsoleView view;

        public EmployeeMenuManager(
            EmployeeService service,
            EmployeeConsoleView view)
        {
            this.service = service;
            this.view = view;
        }

        public void Run()
        {
            int choice;

            do
            {
                Console.WriteLine("\n========== QUAN LY NHAN VIEN ==========");
                Console.WriteLine("1. Them nhan vien");
                Console.WriteLine("2. Hien thi danh sach nhan vien");
                Console.WriteLine("3. Tinh luong nhan vien");
                Console.WriteLine("4. Tinh tong quy luong");
                Console.WriteLine("5. Tim nhan vien luong cao nhat");
                Console.WriteLine("6. Sap xep theo luong");
                Console.WriteLine("7. Thong ke theo phong ban");
                Console.WriteLine("8. Loc nhan vien lam tren 3 nam");
                Console.WriteLine("9. Xuat bang luong theo thang");
                Console.WriteLine("0. Thoat");

                Console.Write("Nhap lua chon: ");
                int.TryParse(Console.ReadLine(), out choice);

                switch (choice)
                {
                    case 1:
                        AddEmployee();
                        break;

                    case 2:
                        ShowEmployees();
                        break;

                    case 3:
                        CalculateSalary();
                        break;

                    case 4:
                        ShowTotalSalary();
                        break;

                    case 5:
                        ShowHighestSalary();
                        break;

                    case 6:
                        SortBySalary();
                        break;

                    case 7:
                        ShowStatisticsByDepartment();
                        break;

                    case 8:
                        ShowEmployeesOver3Years();
                        break;

                    case 9:
                        ShowSalaryTable();
                        break;

                    case 0:
                        Console.WriteLine("Ket thuc chuong trinh.");
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }

            } while (choice != 0);
        }

        private void AddEmployee()
        {
            Console.WriteLine("\n========== THEM NHAN VIEN ==========");
            Console.WriteLine("1. Full Time");
            Console.WriteLine("2. Probation");
            Console.WriteLine("3. Part Time");
            Console.WriteLine("4. Sales");

            Console.Write("Chon loai nhan vien: ");
            int loai;

            int.TryParse(Console.ReadLine(), out loai);

            Console.Write("Ma nhan vien: ");
            string maNhanVien = Console.ReadLine() ?? "";

            Console.Write("Ho ten: ");
            string hoTen = Console.ReadLine() ?? "";

            Console.Write("Phong ban: ");
            string phongBan = Console.ReadLine() ?? "";

            Console.Write("Ngay vao lam (dd/MM/yyyy): ");
            DateTime ngayVaoLam =
                DateTime.Parse(Console.ReadLine() ?? "");

            Employee employee;

            switch (loai)
            {
                case 1:
                    Console.Write("Luong co ban: ");
                    double luongCoBan =
                        double.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Phu cap: ");
                    double phuCap =
                        double.Parse(Console.ReadLine() ?? "0");

                    employee = new FullTimeEmployee(
                        maNhanVien,
                        hoTen,
                        phongBan,
                        ngayVaoLam,
                        luongCoBan,
                        phuCap);

                    break;

                case 2:
                    Console.Write("Luong thu viec: ");
                    double luongThuViec =
                        double.Parse(Console.ReadLine() ?? "0");

                    employee = new ProbationEmployee(
                        maNhanVien,
                        hoTen,
                        phongBan,
                        ngayVaoLam,
                        luongThuViec);

                    break;

                case 3:
                    Console.Write("So gio lam: ");
                    int soGio =
                        int.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Luong theo gio: ");
                    double luongTheoGio =
                        double.Parse(Console.ReadLine() ?? "0");

                    employee = new PartTimeEmployee(
                        maNhanVien,
                        hoTen,
                        phongBan,
                        ngayVaoLam,
                        soGio,
                        luongTheoGio);

                    break;

                case 4:
                    Console.Write("Luong co ban: ");
                    double luongSales =
                        double.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Doanh so: ");
                    double doanhSo =
                        double.Parse(Console.ReadLine() ?? "0");

                    Console.Write("Ty le hoa hong: ");
                    double tyLeHoaHong =
                        double.Parse(Console.ReadLine() ?? "0");

                    employee = new SalesEmployee(
                        maNhanVien,
                        hoTen,
                        phongBan,
                        ngayVaoLam,
                        luongSales,
                        doanhSo,
                        tyLeHoaHong);

                    break;

                default:
                    Console.WriteLine("Loai nhan vien khong hop le.");
                    return;
            }

            service.AddEmployee(employee);

            Console.WriteLine("Them nhan vien thanh cong!");
        }
        private void ShowEmployees()
        {
            List<Employee> danhSach = service.GetAllEmployees();

            Console.WriteLine("\nDANH SACH NHAN VIEN:");
            view.ShowEmployees(danhSach);
        }
        private void CalculateSalary()
        {
            string maNhanVien = view.InputEmployeeId();

            double salary = service.CalculateSalary(maNhanVien);

            view.ShowSalary(maNhanVien, salary);
        }
        private void ShowTotalSalary()
        {
            double totalSalary = service.GetTotalSalary();

            view.ShowTotalSalary(totalSalary);
        }
        private void ShowHighestSalary()
        {
            Employee? employee =
                service.GetEmployeeWithHighestSalary();

            view.ShowHighestSalaryEmployee(employee);
        }
        private void SortBySalary()
        {
            service.SortBySalary();

            List<Employee> danhSach = service.GetAllEmployees();

            Console.WriteLine("\nDANH SACH NHAN VIEN SAP XEP THEO LUONG:");
            view.ShowEmployees(danhSach);
        }
        private void ShowStatisticsByDepartment()
        {
            Dictionary<string, int> statistics =
                service.CountEmployeesByDepartment();

            view.ShowStatisticsByDepartment(statistics);
        }
        private void ShowEmployeesOver3Years()
        {
            List<Employee> danhSach =
                service.GetEmployeesWorkingOver3Years();

            view.ShowEmployeesOver3Years(danhSach);
        }
        private void ShowSalaryTable()
        {
            List<Employee> danhSach =
                service.GetSalaryTable();

            view.ShowSalaryTable(danhSach);
        }
       
    }
}
