using System;
using System.Collections.Generic;

namespace Day01_lab.bai1
{
    public class MenuManager
    {
        private StudentService service;
        private StudentConsoleview view;
        private void LoadSampleData()
        {
            Student sv1 = new Student(
                "SV01",
                "Nguyen Van A",
                new DateTime(2006, 06, 06),
                true,
                "nguyenvana@gmail.com",
                "0123456789",
                "Cong nghe thong tin",
                7.8,
                "Dang hoc"
            );

            Student sv2 = new Student(
                "SV02",
                "Tran Thi B",
                new DateTime(2005, 03, 04),
                true,
                "tranthib@gmail.com",
                "0213432343",
                "ngon ngu anh",
                8.4,
                "Bao Luu"
            );

            Student sv3 = new Student(
                "SV03",
                "Phan Van D",
                new DateTime(2004, 06, 14),
                true,
                "phanvand@gmail.com",
                "0239041234",
                "kinh te van tai",
                8.6,
                "Dang hoc"
            );

            service.AddStudent(sv1);
            service.AddStudent(sv2);
            service.AddStudent(sv3);
        }

        public MenuManager(StudentService service, StudentConsoleview view)
        {
            this.service = service;
            this.view = view;
            LoadSampleData();
        }

        public void Run()
        {
            int choice;

            do
            {
                ShowMenu();

                Console.Write("Nhap lua chon: ");
                int.TryParse(Console.ReadLine(), out choice);

                Console.WriteLine();

                switch (choice)
                {
                    case 1:
                        AddStudent();
                        break;

                    case 2:
                        ShowStudents();
                        break;

                    case 3:
                        FindById();
                        break;

                    case 4:
                        FindByName();
                        break;

                    case 5:
                        UpdateStudent();
                        break;

                    case 6:
                        DeleteStudent();
                        break;

                    case 7:
                        SortByName();
                        break;

                    case 8:
                        SortByScore();
                        break;

                    case 9:
                        ShowStudentsByScore();
                        break;

                    case 10:
                        ShowHighestScoreStudents();
                        break;

                    case 11:
                        ShowAverageScore();
                        break;

                    case 12:
                        ShowStatisticsByMajor();
                        break;

                    case 13:
                        ShowStatisticsByStatus();
                        break;

                    case 0:
                        Console.WriteLine("Thoat chuong trinh.");
                        break;

                    default:
                        Console.WriteLine("Lua chon khong hop le.");
                        break;
                }

                if (choice != 0)
                {
                    Console.WriteLine("\nNhan Enter de tiep tuc...");
                    Console.ReadLine();
                }

            } while (choice != 0);
        }

        private void ShowMenu()
        {
            Console.Clear();

            Console.WriteLine("======================================");
            Console.WriteLine("       QUAN LY SINH VIEN");
            Console.WriteLine("======================================");
            Console.WriteLine("1. Them sinh vien");
            Console.WriteLine("2. Hien thi danh sach");
            Console.WriteLine("3. Tim sinh vien theo ma");
            Console.WriteLine("4. Tim gan dung theo ho ten");
            Console.WriteLine("5. Cap nhat sinh vien");
            Console.WriteLine("6. Xoa sinh vien");
            Console.WriteLine("7. Sap xep theo ho ten");
            Console.WriteLine("8. Sap xep theo diem trung binh");
            Console.WriteLine("9. Hien thi sinh vien co diem tu 8 tro len");
            Console.WriteLine("10. Hien thi sinh vien co diem cao nhat");
            Console.WriteLine("11. Tinh diem trung binh toan bo");
            Console.WriteLine("12. Thong ke sinh vien theo nganh");
            Console.WriteLine("13. Thong ke sinh vien theo trang thai");
            Console.WriteLine("0. Thoat");
            Console.WriteLine("======================================");
        }
        private void AddStudent()
        {
            Console.WriteLine("=== THEM SINH VIEN ===");

            Student student = view.InputStudent();

            bool result = service.AddStudent(student);

            if (result)
            {
                Console.WriteLine("Them sinh vien thanh cong!");
            }
            else
            {
                Console.WriteLine("Them sinh vien that bai!");
            }
        }
        private void ShowStudents()
        {
            Console.WriteLine("=== DANH SACH SINH VIEN ===");

            List<Student> danhSach = service.GetAllStudents();

            view.ShowStudents(danhSach);
        }
        private void FindById()
        {
            Console.WriteLine("=== TIM SINH VIEN THEO MA ===");

            string maSinhVien = view.InputStudentId();

            Student? student = service.FindById(maSinhVien);

            if (student == null)
            {
                Console.WriteLine("Khong tim thay sinh vien.");
            }
            else
            {
                view.ShowStudent(student);
            }
        }
        private void FindByName()
        {
            Console.WriteLine("=== TIM GAN DUNG THEO HO TEN ===");

            string tuKhoa = view.InputNameKeyword();

            List<Student> ketQua = service.FindByName(tuKhoa);

            view.ShowSearchResults(ketQua);
        }
        private void UpdateStudent()
        {
            Console.WriteLine("=== CAP NHAT SINH VIEN ===");

            string maSinhVien = view.InputStudentIdForUpdate();

            Student newStudent = view.InputUpdateStudent();

            bool result = service.UpdateStudent(
                maSinhVien,
                newStudent);

            view.ShowUpdateResult(result);
        }
        private void DeleteStudent()
        {
            Console.WriteLine("=== XOA SINH VIEN ===");

            string maSinhVien = view.InputStudentIdForDelete();

            bool result = service.DeleteStudent(maSinhVien);

            view.ShowDeleteResult(result);
        }
        private void SortByName()
        {
            Console.WriteLine("=== SAP XEP THEO HO TEN ===");

            service.SortByName();

            List<Student> danhSach = service.GetAllStudents();

            view.ShowStudents(danhSach);
        }
        private void SortByScore()
        {
            Console.WriteLine("=== SAP XEP THEO DIEM TRUNG BINH ===");

            service.SortByDiemTrungBinh();

            List<Student> danhSach = service.GetAllStudents();

            view.ShowStudents(danhSach);
        }
        private void ShowStudentsByScore()
        {
            Console.WriteLine("=== SINH VIEN CO DIEM TU 8 TRO LEN ===");

            List<Student> danhSach =
                service.GetStudentsByDiem();

            view.ShowStudents(danhSach);
        }
        private void ShowHighestScoreStudents()
        {
            Console.WriteLine("=== SINH VIEN CO DIEM CAO NHAT ===");

            List<Student> danhSach =
                service.GetStudentsWithHighestScore();

            view.ShowHighestScoreStudents(danhSach);
        }
        private void ShowAverageScore()
        {
            Console.WriteLine("=== DIEM TRUNG BINH TOAN BO SINH VIEN ===");

            double average = service.GetAverageScore();

            view.ShowAverageScore(average);
        }
        private void ShowStatisticsByMajor()
        {
            Console.WriteLine("=== THONG KE THEO NGANH ===");

            Dictionary<string, int> statistics =
                service.CountStudentsByMajor();

            view.ShowStatisticsByMajor(statistics);
        }
        private void ShowStatisticsByStatus()
        {
            Console.WriteLine("=== THONG KE THEO TRANG THAI ===");

            Dictionary<string, int> statistics =
                service.CountStudentsByStatus();

            view.ShowStatisticsByStatus(statistics);
        }
    }
}