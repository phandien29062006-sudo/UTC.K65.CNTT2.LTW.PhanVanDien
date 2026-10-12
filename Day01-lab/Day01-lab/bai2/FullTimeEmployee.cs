using System;

namespace Day01_lab.Bai2
{
    public class FullTimeEmployee : Employee
    {
        public double PhuCap { get; set; }

        public FullTimeEmployee(
            string maNhanVien,
            string hoTen,
            string phongBan,
            DateTime ngayVaoLam,
            double luongCoBan,
            double phuCap)
            : base(
                maNhanVien,
                hoTen,
                phongBan,
                ngayVaoLam,
                luongCoBan)
        {
            PhuCap = phuCap;
        }

        public override double CalculateSalary()
        {
            return LuongCoBan + PhuCap;
        }
    }
}