using System;

namespace Day01_lab.Bai2
{
    public class ProbationEmployee : Employee
    {
        public ProbationEmployee(
            string maNhanVien,
            string hoTen,
            string phongBan,
            DateTime ngayVaoLam,
            double luongCoBan)
            : base(maNhanVien, hoTen, phongBan, ngayVaoLam, luongCoBan)
        {
        }

        public override double CalculateSalary()
        {
            return LuongCoBan * 0.85;
        }
    }
}