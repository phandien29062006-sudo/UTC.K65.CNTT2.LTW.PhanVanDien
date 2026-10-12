using System;
using System.Collections.Generic;
namespace Day01_lab.bai1
{
    public class StudentService
    {
        private List<Student> students = new List<Student>();

        public bool AddStudent(Student student)
        {
            foreach (Student a in students)
            {
                if (a.MaSinhVien == student.MaSinhVien)
                {
                    return false;
                }
            }
            students.Add(student);
            return true;
        }
        public List<Student> GetAllStudents()
        {
            return new List<Student>(students);
        }
        public Student? FindById(string maSinhVien)
        {
            foreach (Student student in students)
            {
                if (student.MaSinhVien == maSinhVien)
                {
                    return student;
                }
            }
            return null;
        }
        public List<Student> FindByName(string tuKhoa)
        {
            List<Student> ketQua = new List<Student>();

            foreach (Student student in students)
            {
                if (student.HoTen.Contains(tuKhoa))
                {
                    ketQua.Add(student);
                }
            }

            return ketQua;
        }
        public bool UpdateStudent(
            string maSinhVien,Student newStudent)
        {
            Student? student = FindById(maSinhVien);

            if (student == null)
            {
                return false;
            }
            if (!StudentValidator.IsValidEmail(newStudent.Email))
            {
                return false;
            }
            student.HoTen = newStudent.HoTen;
            student.NgaySinh = newStudent.NgaySinh;
            student.GioiTinh = newStudent.GioiTinh;
            student.Email = newStudent.Email;
            student.SoDienThoai = newStudent.SoDienThoai;
            student.NganhHoc = newStudent.NganhHoc;
            student.DiemTrungBinh = newStudent.DiemTrungBinh;
            student.TrangThaiHocTap = newStudent.TrangThaiHocTap;
            return true;
        }
        public bool DeleteStudent(string maSinhVien)
        {
            Student? student = FindById(maSinhVien);
            if (student == null)
            {
                return false;
            }
            students.Remove(student);
            return true;
        }
        public void SortByName()
        {
            students.Sort((a, b) =>
                string.Compare(a.HoTen, b.HoTen, StringComparison.OrdinalIgnoreCase));
        }
        public void SortByDiemTrungBinh()
        {
            students.Sort((a, b) =>
                b.DiemTrungBinh.CompareTo(a.DiemTrungBinh));
        }
        public List<Student> GetStudentsByDiem()
        {
            List<Student> ketQua = new List<Student>();

            foreach (Student student in students)
            {
                if (student.DiemTrungBinh >= 8)
                {
                    ketQua.Add(student);
                }
            }

            return ketQua;
        }
        public List<Student> GetStudentsWithHighestScore()
        {
            List<Student> ketQua = new List<Student>();

            if (students.Count == 0)
            {
                return ketQua;
            }

            double diemCaoNhat = students[0].DiemTrungBinh;

            foreach (Student student in students)
            {
                if (student.DiemTrungBinh > diemCaoNhat)
                {
                    diemCaoNhat = student.DiemTrungBinh;
                }
            }

            foreach (Student student in students)
            {
                if (student.DiemTrungBinh == diemCaoNhat)
                {
                    ketQua.Add(student);
                }
            }

            return ketQua;
        }
        public double GetAverageScore()
        {
            if (students.Count == 0)
            {
                return 0;
            }

            double tong = 0;

            foreach (Student student in students)
            {
                tong += student.DiemTrungBinh;
            }

            return tong / students.Count;
        }
        public Dictionary<string, int> CountStudentsByMajor()
        {
            Dictionary<string, int> ketQua = new Dictionary<string, int>();

            foreach (Student student in students)
            {
                string nganh = student.NganhHoc;

                if (ketQua.ContainsKey(nganh))
                {
                    ketQua[nganh]++;
                }
                else
                {
                    ketQua.Add(nganh, 1);
                }
            }

            return ketQua;
        }
        public Dictionary<string, int> CountStudentsByStatus()
        {
            Dictionary<string, int> ketQua = new Dictionary<string, int>();

            foreach (Student student in students)
            {
                string trangThai = student.TrangThaiHocTap;

                if (ketQua.ContainsKey(trangThai))
                {
                    ketQua[trangThai]++;
                }
                else
                {
                    ketQua.Add(trangThai, 1);
                }
            }

            return ketQua;
        }
    }
}