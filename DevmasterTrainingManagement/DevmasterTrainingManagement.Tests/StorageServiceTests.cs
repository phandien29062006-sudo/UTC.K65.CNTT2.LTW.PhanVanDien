using DevmasterTrainingManagement.Domain;
using DevmasterTrainingManagement.Infrastructure;

namespace DevmasterTrainingManagement.Tests
{
    public class StorageServiceTests
    {
        private readonly StorageService storageService;

        public StorageServiceTests()
        {
            storageService = new StorageService();
        }


        // 1. Lưu dữ liệu JSON thành công
        [Fact]
        public void SaveJson_ThanhCong()
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_students.json");

            List<Student> students = new List<Student>
            {
                new Student
                {
                    MaHocVien = "HV01",
                    HoTen = "Nguyen Van A",
                    NgaySinh = new DateTime(2005, 1, 1),
                    DienThoai = "0900000001",
                    Email = "hv01@gmail.com",
                    DiaChi = "Ha Noi",
                    NgayDangKy = DateTime.Today
                }
            };

            bool result =
                storageService.SaveJson(
                    filePath,
                    students);

            Assert.True(result);
            Assert.True(File.Exists(filePath));

            File.Delete(filePath);
        }


        // 2. Đọc dữ liệu JSON thành công
        [Fact]
        public void LoadJson_ThanhCong()
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_students.json");

            List<Student> students = new List<Student>
            {
                new Student
                {
                    MaHocVien = "HV01",
                    HoTen = "Nguyen Van A",
                    NgaySinh = new DateTime(2005, 1, 1),
                    DienThoai = "0900000001",
                    Email = "hv01@gmail.com",
                    DiaChi = "Ha Noi",
                    NgayDangKy = DateTime.Today
                }
            };

            storageService.SaveJson(
                filePath,
                students);

            List<Student> result =
                storageService.LoadJson<Student>(
                    filePath);

            Assert.Single(result);

            Assert.Equal(
                "HV01",
                result[0].MaHocVien);

            Assert.Equal(
                "Nguyen Van A",
                result[0].HoTen);

            File.Delete(filePath);
        }


        // 3. Lưu rồi đọc lại không mất dữ liệu
        [Fact]
        public void SaveVaLoad_KhongMatDuLieu()
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_students.json");

            List<Student> students = new List<Student>
            {
                new Student
                {
                    MaHocVien = "HV01",
                    HoTen = "Nguyen Van A",
                    NgaySinh = new DateTime(2005, 1, 1),
                    DienThoai = "0900000001",
                    Email = "hv01@gmail.com",
                    DiaChi = "Ha Noi",
                    NgayDangKy = DateTime.Today
                },

                new Student
                {
                    MaHocVien = "HV02",
                    HoTen = "Nguyen Van B",
                    NgaySinh = new DateTime(2005, 2, 2),
                    DienThoai = "0900000002",
                    Email = "hv02@gmail.com",
                    DiaChi = "Ha Noi",
                    NgayDangKy = DateTime.Today
                }
            };

            bool saveResult =
                storageService.SaveJson(
                    filePath,
                    students);

            List<Student> result =
                storageService.LoadJson<Student>(
                    filePath);

            Assert.True(saveResult);

            Assert.Equal(
                2,
                result.Count);

            Assert.Equal(
                "HV01",
                result[0].MaHocVien);

            Assert.Equal(
                "HV02",
                result[1].MaHocVien);

            File.Delete(filePath);
        }


        // 4. Đọc file không tồn tại
        [Fact]
        public void LoadJson_FileKhongTonTai()
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    "file_khong_ton_tai.json");

            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            List<Student> result =
                storageService.LoadJson<Student>(
                    filePath);

            Assert.NotNull(result);
            Assert.Empty(result);
        }


        // 5. Lưu danh sách rỗng
        [Fact]
        public void SaveJson_DanhSachRong()
        {
            string filePath =
                Path.Combine(
                    Path.GetTempPath(),
                    "test_empty.json");

            List<Student> students =
                new List<Student>();

            bool result =
                storageService.SaveJson(
                    filePath,
                    students);

            Assert.True(result);
            Assert.True(File.Exists(filePath));

            List<Student> loaded =
                storageService.LoadJson<Student>(
                    filePath);

            Assert.Empty(loaded);

            File.Delete(filePath);
        }
    }
}