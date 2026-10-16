using DevmasterTrainingManagement.Application;
using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Tests
{
    public class StudentCareServiceTests
    {
        // Tạo service có dữ liệu mẫu
        private StudentCareService CreateService()
        {
            StudentCareService service =
                new StudentCareService();

            service.Add(new StudentCare
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV01",
                NgayChamSoc = DateTime.Today.AddDays(-2),
                KenhLienHe = "Điện thoại",
                NoiDung = "Tư vấn khóa học",
                KetQua = "Đã liên hệ",
                NgayHenTiepTheo = DateTime.Today
            });

            service.Add(new StudentCare
            {
                MaChamSoc = "CS02",
                MaHocVien = "HV01",
                NgayChamSoc = DateTime.Today.AddDays(-5),
                KenhLienHe = "Email",
                NoiDung = "Tư vấn học phí",
                KetQua = "Đang chờ",
                NgayHenTiepTheo = DateTime.Today.AddDays(3)
            });

            service.Add(new StudentCare
            {
                MaChamSoc = "CS03",
                MaHocVien = "HV02",
                NgayChamSoc = DateTime.Today.AddDays(-10),
                KenhLienHe = "Điện thoại",
                NoiDung = "Nhắc thanh toán",
                KetQua = "Đã liên hệ",
                NgayHenTiepTheo = DateTime.Today.AddDays(-2)
            });

            return service;
        }


        // 1. Thêm lịch chăm sóc thành công
        [Fact]
        public void Add_StudentCare_ThanhCong()
        {
            StudentCareService service =
                new StudentCareService();

            StudentCare studentCare = new StudentCare
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV01",
                NgayChamSoc = DateTime.Today,
                KenhLienHe = "Điện thoại",
                NoiDung = "Tư vấn khóa học",
                KetQua = "Đã liên hệ",
                NgayHenTiepTheo = DateTime.Today
            };

            bool result =
                service.Add(studentCare);

            Assert.True(result);
            Assert.Single(service.GetAll());
        }


        // 2. Không cho thêm trùng mã chăm sóc
        [Fact]
        public void Add_StudentCare_TrungMa()
        {
            StudentCareService service =
                new StudentCareService();

            StudentCare studentCare1 = new StudentCare
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV01",
                NgayChamSoc = DateTime.Today,
                KenhLienHe = "Điện thoại",
                NoiDung = "Tư vấn",
                KetQua = "Đã liên hệ"
            };

            StudentCare studentCare2 = new StudentCare
            {
                MaChamSoc = "CS01",
                MaHocVien = "HV02",
                NgayChamSoc = DateTime.Today,
                KenhLienHe = "Email",
                NoiDung = "Tư vấn khác",
                KetQua = "Đang chờ"
            };

            bool result1 =
                service.Add(studentCare1);

            bool result2 =
                service.Add(studentCare2);

            Assert.True(result1);
            Assert.False(result2);
            Assert.Single(service.GetAll());
        }


        // 3. Lấy tất cả lịch chăm sóc
        [Fact]
        public void GetAll_TraVeDanhSach()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetAll();

            Assert.Equal(3, result.Count);
        }


        // 4. Lấy lịch chăm sóc theo học viên
        [Fact]
        public void GetByStudent_TimDungHocVien()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetByStudent("HV01");

            Assert.Equal(2, result.Count);

            Assert.All(
                result,
                x => Assert.Equal(
                    "HV01",
                    x.MaHocVien));
        }


        // 5. Học viên không có lịch chăm sóc
        [Fact]
        public void GetByStudent_KhongCoDuLieu()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetByStudent("HV999");

            Assert.Empty(result);
        }


        // 6. Kiểm tra sắp xếp lịch chăm sóc mới nhất trước
        [Fact]
        public void GetByStudent_SapXepMoiNhatTruoc()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetByStudent("HV01");

            Assert.Equal(
                "CS01",
                result[0].MaChamSoc);

            Assert.Equal(
                "CS02",
                result[1].MaChamSoc);
        }


        // 7. Lấy lịch hẹn hôm nay
        [Fact]
        public void GetTodayAppointments_TimDung()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetTodayAppointments();

            Assert.Single(result);

            Assert.Equal(
                "CS01",
                result[0].MaChamSoc);
        }


        // 8. Lấy lịch hẹn quá hạn
        [Fact]
        public void GetOverdueAppointments_TimDung()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> result =
                service.GetOverdueAppointments();

            Assert.Single(result);

            Assert.Equal(
                "CS03",
                result[0].MaChamSoc);
        }


        // 9. Thống kê kết quả chăm sóc
        [Fact]
        public void StatisticsResult_ThongKeDung()
        {
            StudentCareService service =
                CreateService();

            Dictionary<string, int> result =
                service.StatisticsResult();

            Assert.Equal(
                2,
                result["Đã liên hệ"]);

            Assert.Equal(
                1,
                result["Đang chờ"]);
        }


        // 10. LoadData
        [Fact]
        public void LoadData_NapDuLieuThanhCong()
        {
            StudentCareService service =
                new StudentCareService();

            List<StudentCare> data =
                new List<StudentCare>
                {
                    new StudentCare
                    {
                        MaChamSoc = "CS01",
                        MaHocVien = "HV01",
                        NgayChamSoc = DateTime.Today,
                        KenhLienHe = "Điện thoại",
                        NoiDung = "Tư vấn",
                        KetQua = "Đã liên hệ"
                    },

                    new StudentCare
                    {
                        MaChamSoc = "CS02",
                        MaHocVien = "HV02",
                        NgayChamSoc = DateTime.Today,
                        KenhLienHe = "Email",
                        NoiDung = "Hỏi học phí",
                        KetQua = "Đang chờ"
                    }
                };

            service.LoadData(data);

            Assert.Equal(
                2,
                service.GetAll().Count);

            Assert.NotNull(
                service.GetAll()
                    .FirstOrDefault(
                        x => x.MaChamSoc == "CS01"));

            Assert.NotNull(
                service.GetAll()
                    .FirstOrDefault(
                        x => x.MaChamSoc == "CS02"));
        }


        // 11. LoadData xóa dữ liệu cũ
        [Fact]
        public void LoadData_XoaDuLieuCu()
        {
            StudentCareService service =
                CreateService();

            List<StudentCare> data =
                new List<StudentCare>
                {
                    new StudentCare
                    {
                        MaChamSoc = "CS99",
                        MaHocVien = "HV99",
                        NgayChamSoc = DateTime.Today,
                        KenhLienHe = "Email",
                        NoiDung = "Test",
                        KetQua = "Đang chờ"
                    }
                };

            service.LoadData(data);

            Assert.Single(
                service.GetAll());

            Assert.Equal(
                "CS99",
                service.GetAll()[0].MaChamSoc);
        }
    }
}