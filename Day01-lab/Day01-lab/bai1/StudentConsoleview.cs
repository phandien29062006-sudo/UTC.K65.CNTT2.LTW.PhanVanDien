using System;
using System.Collections.Generic;
using System.Text;

namespace Day01_lab.bai1
{
    public class StudentConsoleview
    {
        public void ShowStudent(Student student)
        {
            Console.WriteLine("------------------------------");
            Console.WriteLine("Ma SV: " + student.MaSinhVien);
            Console.WriteLine("Ho ten: " + student.HoTen);
            Console.WriteLine("Ngay sinh: " +student.NgaySinh.ToString("dd/MM/yyyy"));
            Console.WriteLine("Gioi tinh: " + (student.GioiTinh ? "Nam" : "Nu"));
            Console.WriteLine("Email: " + student.Email);
            Console.WriteLine("SDT: " + student.SoDienThoai);
            Console.WriteLine("Nganh: " + student.NganhHoc);
            Console.WriteLine("Diem TB: " + student.DiemTrungBinh);
            Console.WriteLine("Trang thai: " + student.TrangThaiHocTap);
        }
        public void ShowStudents(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Danh sach sinh vien rong.");
                return;
            }

            foreach (Student student in students)
            {
                ShowStudent(student);
            }
        }
        public Student InputStudent()
        {
            Console.Write("Nhap ma sinh vien: ");
            string maSinhVien = Console.ReadLine() ?? "";

            Console.Write("Nhap ho ten: ");
            string hoTen = Console.ReadLine() ?? "";

            Console.Write("Nhap ngay sinh (yyyy-MM-dd): ");
            DateTime ngaySinh = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Gioi tinh (true = Nam, false = Nu): ");
            bool gioiTinh = bool.Parse(Console.ReadLine() ?? "");

            Console.Write("Nhap email: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Nhap so dien thoai: ");
            string soDienThoai = Console.ReadLine() ?? "";

            Console.Write("Nhap nganh hoc: ");
            string nganhHoc = Console.ReadLine() ?? "";

            Console.Write("Nhap diem trung binh: ");
            double diemTrungBinh = double.Parse(Console.ReadLine() ?? "");

            Console.Write("Nhap trang thai hoc tap: ");
            string trangThaiHocTap = Console.ReadLine() ?? "";

            return new Student(
                maSinhVien,
                hoTen,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                nganhHoc,
                diemTrungBinh,
                trangThaiHocTap
            );
        }
        public string InputStudentId()
        {
            Console.Write("Nhap ma sinh vien: ");
            return Console.ReadLine() ?? "";
        }

        public void ShowStudentSearchResult(Student? student)
        {
            if (student == null)
            {
                Console.WriteLine("Khong tim thay sinh vien.");
                return;
            }

            Console.WriteLine("Tim thay sinh vien!");
            ShowStudent(student);
        }
        public string InputNameKeyword()
        {
            Console.Write("Nhap ten can tim: ");
            return Console.ReadLine() ?? "";
        }
        public void ShowSearchResults(List<Student> students)
        {
            if (students.Count == 0)
            {
                Console.WriteLine("Khong tim thay sinh vien.");
                return;
            }

            Console.WriteLine("Tim thay " + students.Count + " sinh vien:");

            ShowStudents(students);
        }
        public string InputStudentIdForUpdate()
        {
            Console.Write("Nhap ma sinh vien can cap nhat: ");
            return Console.ReadLine() ?? "";
        }
        public Student InputUpdateStudent()
        {
            Console.Write("Nhap ho ten moi: ");
            string hoTen = Console.ReadLine() ?? "";

            Console.Write("Nhap ngay sinh moi (yyyy-MM-dd): ");
            DateTime ngaySinh = DateTime.Parse(Console.ReadLine() ?? "");

            Console.Write("Gioi tinh moi (true = Nam, false = Nu): ");
            bool gioiTinh = bool.Parse(Console.ReadLine() ?? "");

            Console.Write("Nhap email moi: ");
            string email = Console.ReadLine() ?? "";

            Console.Write("Nhap so dien thoai moi: ");
            string soDienThoai = Console.ReadLine() ?? "";

            Console.Write("Nhap nganh hoc moi: ");
            string nganhHoc = Console.ReadLine() ?? "";

            Console.Write("Nhap diem trung binh moi: ");
            double diemTrungBinh = double.Parse(Console.ReadLine() ?? "");

            Console.Write("Nhap trang thai hoc tap moi: ");
            string trangThaiHocTap = Console.ReadLine() ?? "";

            return new Student(
                "",
                hoTen,
                ngaySinh,
                gioiTinh,
                email,
                soDienThoai,
                nganhHoc,
                diemTrungBinh,
                trangThaiHocTap
            );
        }
        public void ShowUpdateResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Cap nhat sinh vien thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien.");
            }
        }
        public string InputStudentIdForDelete()
        {
            Console.Write("Nhap ma sinh vien can xoa: ");
            return Console.ReadLine() ?? "";
        }
        public void ShowDeleteResult(bool success)
        {
            if (success)
            {
                Console.WriteLine("Xoa sinh vien thanh cong!");
            }
            else
            {
                Console.WriteLine("Khong tim thay sinh vien.");
            }
        }
        public void ShowHighestScoreStudents(List<Student> students)
        {
            Console.WriteLine("\nSinh vien co diem trung binh cao nhat:");

            if (students.Count == 0)
            {
                Console.WriteLine("Danh sach sinh vien rong.");
                return;
            }

            ShowStudents(students);
        }
        public void ShowAverageScore(double average)
        {
            Console.WriteLine(
                "\nDiem trung binh cua tat ca sinh vien: " + average.ToString("F2"));
        }
        public void ShowStatisticsByMajor(Dictionary<string, int> statistics)
        {
            Console.WriteLine("\nThong ke sinh vien theo nganh:");

            if (statistics.Count == 0)
            {
                Console.WriteLine("Danh sach sinh vien rong.");
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    item.Key + ": " + item.Value + " sinh vien");
            }
        }
        public void ShowStatisticsByStatus(Dictionary<string, int> statistics)
        {
            Console.WriteLine("\nThong ke sinh vien theo trang thai:");

            if (statistics.Count == 0)
            {
                Console.WriteLine("Danh sach sinh vien rong.");
                return;
            }

            foreach (var item in statistics)
            {
                Console.WriteLine(
                    item.Key + ": " + item.Value + " sinh vien");
            }
        }
    }
}
