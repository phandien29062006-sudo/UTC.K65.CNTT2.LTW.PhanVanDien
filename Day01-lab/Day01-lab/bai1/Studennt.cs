using System;
namespace Day01_lab.bai1
{
    public class Student
    {
        public string MaSinhVien { get; set; } = ""; 
        private string hoTen = "";
        public string HoTen {
            get
            {
                return hoTen;
            }
            set {
                if (!string.IsNullOrWhiteSpace(value))
                {
                    hoTen = value;
                }
            }
        }
        public DateTime NgaySinh { get; set; }
        public bool GioiTinh { get; set; }
        public string Email { get; set; } = "";
        public string SoDienThoai { get; set; } = "";
        public string NganhHoc { get; set; } = "";
        private double diemTrungBinh;
        public double DiemTrungBinh {
            get{
                return diemTrungBinh;
            }
            set
            {
                if(value >=0 && value <= 10)
                {
                    diemTrungBinh = value;
                }
            }
        }
        public string TrangThaiHocTap { get; set; } = "";

        public Student(string maSinhVien, string hoTen, DateTime ngaySinh, bool gioiTinh, string email, string soDienThoai, string nganhHoc, double diemTrungBinh, string trangThaiHocTap)
        {
            MaSinhVien = maSinhVien;
            HoTen = hoTen;
            NgaySinh = ngaySinh;
            GioiTinh = gioiTinh;
            Email = email;
            SoDienThoai = soDienThoai;
            NganhHoc = nganhHoc;
            DiemTrungBinh = diemTrungBinh;
            TrangThaiHocTap = trangThaiHocTap;

        }

    }
}