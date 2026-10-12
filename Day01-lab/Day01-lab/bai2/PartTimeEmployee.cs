using System;

namespace Day01_lab.Bai2
{
    public class PartTimeEmployee : Employee
    {
        public double SoGioLam { get; set; }
        public double DonGiaGio { get; set; }

        public PartTimeEmployee(
            string maNhanVien,
            string hoTen,
            string phongBan,
            DateTime ngayVaoLam,
            double soGioLam,
            double donGiaGio)
            : base(maNhanVien, hoTen, phongBan, ngayVaoLam, 0)
        {
            SoGioLam = soGioLam;
            DonGiaGio = donGiaGio;
        }

        public override double CalculateSalary()
        {
            return SoGioLam * DonGiaGio;
        }
    }
}