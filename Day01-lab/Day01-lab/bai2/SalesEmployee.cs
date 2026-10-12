using System;

namespace Day01_lab.Bai2
{
    public class SalesEmployee : Employee
    {
        public double DoanhSo { get; set; }
        public double TyLeHoaHong { get; set; }

        public SalesEmployee(
            string maNhanVien,
            string hoTen,
            string phongBan,
            DateTime ngayVaoLam,
            double luongCoBan,
            double doanhSo,
            double tyLeHoaHong)
            : base(maNhanVien, hoTen, phongBan, ngayVaoLam, luongCoBan)
        {
            DoanhSo = doanhSo;
            TyLeHoaHong = tyLeHoaHong;
        }

        public override double CalculateSalary()
        {
            return LuongCoBan + DoanhSo * TyLeHoaHong;
        }
    }
}