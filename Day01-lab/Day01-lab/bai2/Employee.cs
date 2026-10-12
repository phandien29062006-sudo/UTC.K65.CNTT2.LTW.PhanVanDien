using System;

namespace Day01_lab.Bai2
{
    public abstract class Employee
    {
        public string MaNhanVien { get; set; }
        public string HoTen { get; set; }
        public string PhongBan { get; set; }
        public DateTime NgayVaoLam { get; set; }
        public double LuongCoBan { get; set; }

        public Employee(
            string maNhanVien,
            string hoTen,
            string phongBan,
            DateTime ngayVaoLam,
            double luongCoBan)
        {
            MaNhanVien = maNhanVien;
            HoTen = hoTen;
            PhongBan = phongBan;
            NgayVaoLam = ngayVaoLam;
            LuongCoBan = luongCoBan;
        }

        public abstract double CalculateSalary();
    }
}