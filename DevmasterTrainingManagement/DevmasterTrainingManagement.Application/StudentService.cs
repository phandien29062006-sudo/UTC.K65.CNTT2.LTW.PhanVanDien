using DevmasterTrainingManagement.Domain;
using System.Text;
using System.Text.Json;

namespace DevmasterTrainingManagement.Application
{
    public class StudentService
    {
        private List<Student> students = new List<Student>();


        public List<Student> GetAll()
        {
            return students;
        }

        public Student? GetById(string maHocVien)
        {
            return students.FirstOrDefault(
                s => s.MaHocVien == maHocVien);
        }

    
        public bool Add(Student student)
        {
            if (students.Any(s => s.MaHocVien == student.MaHocVien))
            {
                return false;
            }

            students.Add(student);
            return true;
        }

        public bool Update(Student student)
        {
            Student? oldStudent = GetById(student.MaHocVien);

            if (oldStudent == null)
            {
                return false;
            }

            oldStudent.HoTen = student.HoTen;
            oldStudent.NgaySinh = student.NgaySinh;
            oldStudent.DienThoai = student.DienThoai;
            oldStudent.Email = student.Email;
            oldStudent.DiaChi = student.DiaChi;
            oldStudent.NgayDangKy = student.NgayDangKy;

            return true;
        }

  
        public bool Delete(string maHocVien)
        {
            Student? student = GetById(maHocVien);

            if (student == null)
            {
                return false;
            }

            students.Remove(student);
            return true;
        }

        public bool IsDuplicatePhone(string dienThoai)
        {
            return students.Any(
                s => s.DienThoai == dienThoai);
        }

    
        public List<Student> Search(string keyword)
        {
            keyword = keyword.ToLower();

            return students
                .Where(s =>
                    s.HoTen.ToLower().Contains(keyword) ||
                    s.DienThoai.Contains(keyword) ||
                    s.Email.ToLower().Contains(keyword))
                .ToList();
        }

   

        public bool ImportCSV(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Không tìm thấy file!");
                return false;
            }

            string[] lines = File.ReadAllLines(filePath);

            int soDongThem = 0;

            for (int i = 1; i < lines.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(lines[i]))
                    continue;

                string[] data = lines[i].Split(',');

                if (data.Length < 7)
                {
                    Console.WriteLine($"Dòng {i + 1} không đủ dữ liệu!");
                    continue;
                }

                DateTime ngaySinh;
                DateTime ngayDangKy;

                bool ngaySinhHopLe =
                    DateTime.TryParseExact(
                        data[2].Trim(),
                        "dd/MM/yyyy",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out ngaySinh);

                bool ngayDangKyHopLe =
                    DateTime.TryParseExact(
                        data[6].Trim(),
                        "dd/MM/yyyy",
                        null,
                        System.Globalization.DateTimeStyles.None,
                        out ngayDangKy);

                if (!ngaySinhHopLe || !ngayDangKyHopLe)
                {
                    Console.WriteLine($"Dòng {i + 1}: Ngày không hợp lệ!");
                    continue;
                }

                Student student = new Student
                {
                    MaHocVien = data[0].Trim(),
                    HoTen = data[1].Trim(),
                    NgaySinh = ngaySinh,
                    DienThoai = data[3].Trim(),
                    Email = data[4].Trim(),
                    DiaChi = data[5].Trim(),
                    NgayDangKy = ngayDangKy
                };

                if (Add(student))
                {
                    soDongThem++;

                    Console.WriteLine(
                        $"Đã thêm: {student.MaHocVien} - {student.HoTen}");
                }
                else
                {
                    Console.WriteLine(
                        $"Mã học viên {student.MaHocVien} đã tồn tại!");
                }
            }

            Console.WriteLine(
                $"Đã import thành công {soDongThem} học viên.");

            return soDongThem > 0;
        }


        public bool ExportCSV(string filePath)
        {
            try
            {
                using StreamWriter writer =
                    new StreamWriter(
                        filePath,
                        false,
                        Encoding.UTF8);

                writer.WriteLine(
                    "MaHocVien,HoTen,NgaySinh,DienThoai,Email,DiaChi,NgayDangKy");

                foreach (Student student in students)
                {
                    writer.WriteLine(
                        $"{student.MaHocVien}," +
                        $"{student.HoTen}," +
                        $"{student.NgaySinh:dd/MM/yyyy}," +
                        $"{student.DienThoai}," +
                        $"{student.Email}," +
                        $"{student.DiaChi}," +
                        $"{student.NgayDangKy:dd/MM/yyyy}");
                }

                return true;
            }
            catch
            {
                return false;
            }
        }

 
        public bool SaveJson(string filePath)
        {
            try
            {
                JsonSerializerOptions options =
                    new JsonSerializerOptions
                    {
                        WriteIndented = true
                    };

                string json = JsonSerializer.Serialize(
                    students,
                    options);

                File.WriteAllText(
                    filePath,
                    json,
                    Encoding.UTF8);

                return true;
            }
            catch
            {
                return false;
            }
        }
        public void LoadData(List<Student> data)
        {
            students.Clear();

            foreach (Student student in data)
            {
                Add(student);
            }
        }
    }
}