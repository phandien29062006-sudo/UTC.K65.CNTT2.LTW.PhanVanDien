using System;
namespace PVD_241230675
{

    public class Program{
        static void Main(string[] args)
        {
            Class1 sv = new Class1();

            sv.masv = "241230675";
            sv.hoten = "Phan Van Dien";
            sv.email = "dien123@gmail.com";
            sv.gioitinh =true;
            sv.ngaysinh = new DateTime(2006, 06, 06);
            sv.diachi = "Nam Dinh";

            Console.WriteLine("Ma sinh viên: " +sv.masv);
            Console.WriteLine("Ho tên: "+sv.hoten);
            Console.WriteLine("Email: "+sv.email);
            Console.WriteLine("Gioi tinh: (true : Nam, false : Nu ) "+sv.gioitinh);
            Console.WriteLine("Ngay sinh: " +sv.ngaysinh.ToString("dd/MM/yyyy"));
            Console.WriteLine("Quê quán:" + sv.diachi);
            Console.ReadKey();
        }
    }
}