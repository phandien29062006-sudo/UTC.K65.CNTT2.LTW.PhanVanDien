using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class ClassServiceTests
    {
        // Tạo ClassService có dữ liệu mẫu
        private ClassService CreateService()
        {
            ClassService service = new ClassService();

            service.Add(new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(10),
                LichHoc = "T2-T4-T6",
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            });

            service.Add(new ClassRoom
            {
                MaLop = "L02",
                TenLop = "Java K01",
                MaKhoaHoc = "KH02",
                NgayKhaiGiang = DateTime.Today.AddDays(20),
                LichHoc = "T3-T5-T7",
                SiSoToiDa = 25,
                TrangThai = "Đang học"
            });

            return service;
        }


        // 1. Thêm lớp thành công
        [Fact]
        public void Add_Class_ThanhCong()
        {
            // Arrange
            ClassService service = new ClassService();

            ClassRoom classRoom = new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(10),
                LichHoc = "T2-T4-T6",
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            };

            // Act
            bool result = service.Add(classRoom);

            // Assert
            Assert.True(result);
            Assert.Single(service.GetAll());
        }


        // 2. Không cho thêm lớp trùng mã
        [Fact]
        public void Add_Class_TrungMa()
        {
            // Arrange
            ClassService service = new ClassService();

            ClassRoom classRoom1 = new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(10),
                LichHoc = "T2-T4-T6",
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            };

            ClassRoom classRoom2 = new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K02",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today.AddDays(20),
                LichHoc = "T3-T5-T7",
                SiSoToiDa = 25,
                TrangThai = "Sắp khai giảng"
            };

            // Act
            bool result1 = service.Add(classRoom1);
            bool result2 = service.Add(classRoom2);

            // Assert
            Assert.True(result1);
            Assert.False(result2);
            Assert.Single(service.GetAll());
        }


        // 3. Tìm lớp theo mã
        [Fact]
        public void GetById_TimDungLop()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            ClassRoom? result =
                service.GetById("L01");

            // Assert
            Assert.NotNull(result);
            Assert.Equal("L01", result.MaLop);
            Assert.Equal("C# K01", result.TenLop);
        }


        // 4. Không tìm thấy lớp
        [Fact]
        public void GetById_KhongTimThay()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            ClassRoom? result =
                service.GetById("L999");

            // Assert
            Assert.Null(result);
        }


        // 5. Cập nhật lớp
        [Fact]
        public void Update_Class_ThanhCong()
        {
            // Arrange
            ClassService service = CreateService();

            ClassRoom updatedClass = new ClassRoom
            {
                MaLop = "L01",
                TenLop = "C# K01 Updated",
                MaKhoaHoc = "KH02",
                NgayKhaiGiang = DateTime.Today.AddDays(30),
                LichHoc = "T3-T5",
                SiSoToiDa = 40,
                TrangThai = "Đang học"
            };

            // Act
            bool result =
                service.Update(updatedClass);

            // Assert
            Assert.True(result);

            ClassRoom? actual =
                service.GetById("L01");

            Assert.NotNull(actual);

            Assert.Equal(
                "C# K01 Updated",
                actual.TenLop);

            Assert.Equal(
                "KH02",
                actual.MaKhoaHoc);

            Assert.Equal(
                40,
                actual.SiSoToiDa);

            Assert.Equal(
                "Đang học",
                actual.TrangThai);
        }


        // 6. Cập nhật lớp không tồn tại
        [Fact]
        public void Update_Class_KhongTonTai()
        {
            // Arrange
            ClassService service = CreateService();

            ClassRoom classRoom = new ClassRoom
            {
                MaLop = "L999",
                TenLop = "Lớp không tồn tại",
                MaKhoaHoc = "KH01",
                NgayKhaiGiang = DateTime.Today,
                LichHoc = "T2",
                SiSoToiDa = 30,
                TrangThai = "Sắp khai giảng"
            };

            // Act
            bool result =
                service.Update(classRoom);

            // Assert
            Assert.False(result);
        }


        // 7. Kiểm tra sĩ số - lớp còn chỗ
        [Fact]
        public void KiemTraSiSo_LopConCho()
        {
            // Arrange
            ClassService service = CreateService();

            // Lớp có sĩ số tối đa 30
            // Hiện có 10 học viên
            // 10 < 30 => còn chỗ

            // Act
            bool result =
                service.KiemTraSiSo("L01", 10);

            // Assert
            Assert.True(result);
        }


        // 8. Kiểm tra sĩ số - lớp đầy
        [Fact]
        public void KiemTraSiSo_LopDay()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.KiemTraSiSo("L01", 30);

            // Assert
            Assert.False(result);
        }


        // 9. Kiểm tra sĩ số - lớp không tồn tại
        [Fact]
        public void KiemTraSiSo_LopKhongTonTai()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.KiemTraSiSo("L999", 10);

            // Assert
            Assert.False(result);
        }


        // 10. Tìm lớp sắp khai giảng
        [Fact]
        public void LopSapKhaiGiang_TimDung()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            List<ClassRoom> result =
                service.LopSapKhaiGiang();

            // Assert
            Assert.Contains(
                result,
                c => c.MaLop == "L01");
        }


        // 11. Lớp đang học
        [Fact]
        public void LopDangHoc_TimDung()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            List<ClassRoom> result =
                service.LopDangHoc();

            // Assert
            Assert.Single(result);

            Assert.Equal(
                "L02",
                result[0].MaLop);
        }


        // 12. Đóng lớp
        [Fact]
        public void DongLop_ThanhCong()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.DongLop("L01");

            // Assert
            Assert.True(result);

            ClassRoom? classRoom =
                service.GetById("L01");

            Assert.NotNull(classRoom);

            Assert.Equal(
                "Đã đóng",
                classRoom.TrangThai);
        }


        // 13. Đóng lớp không tồn tại
        [Fact]
        public void DongLop_KhongTonTai()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.DongLop("L999");

            // Assert
            Assert.False(result);
        }


        // 14. Hủy lớp
        [Fact]
        public void HuyLop_ThanhCong()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.HuyLop("L01");

            // Assert
            Assert.True(result);

            ClassRoom? classRoom =
                service.GetById("L01");

            Assert.NotNull(classRoom);

            Assert.Equal(
                "Đã hủy",
                classRoom.TrangThai);
        }


        // 15. Hủy lớp không tồn tại
        [Fact]
        public void HuyLop_KhongTonTai()
        {
            // Arrange
            ClassService service = CreateService();

            // Act
            bool result =
                service.HuyLop("L999");

            // Assert
            Assert.False(result);
        }
    }
}