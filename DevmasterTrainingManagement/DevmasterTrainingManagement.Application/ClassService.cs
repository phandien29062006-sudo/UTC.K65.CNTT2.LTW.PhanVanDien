using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Application
{
    public class ClassService
    {
        private List<ClassRoom> classes = new List<ClassRoom>();

        public List<ClassRoom> GetAll()
        {
            return classes;
        }


        public ClassRoom? GetById(string maLop)
        {
            return classes.FirstOrDefault(
                c => c.MaLop == maLop);
        }

        public bool Add(ClassRoom classRoom)
        {
            if (classes.Any(c => c.MaLop == classRoom.MaLop))
            {
                return false;
            }

            classes.Add(classRoom);
            return true;
        }


        public bool Update(ClassRoom classRoom)
        {
            ClassRoom? oldClass = GetById(classRoom.MaLop);

            if (oldClass == null)
            {
                return false;
            }

            oldClass.TenLop = classRoom.TenLop;
            oldClass.MaKhoaHoc = classRoom.MaKhoaHoc;
            oldClass.NgayKhaiGiang = classRoom.NgayKhaiGiang;
            oldClass.LichHoc = classRoom.LichHoc;
            oldClass.SiSoToiDa = classRoom.SiSoToiDa;
            oldClass.TrangThai = classRoom.TrangThai;

            return true;
        }

        public bool Delete(string maLop)
        {
            ClassRoom? classRoom = GetById(maLop);

            if (classRoom == null)
            {
                return false;
            }

            classes.Remove(classRoom);
            return true;
        }

        public bool KiemTraSiSo(string maLop, int soHocVien)
        {
            ClassRoom? classRoom = GetById(maLop);

            if (classRoom == null)
            {
                return false;
            }

            return soHocVien < classRoom.SiSoToiDa;
        }

        public List<ClassRoom> LopSapKhaiGiang()
        {
            DateTime today = DateTime.Now;

            return classes
                .Where(c => c.NgayKhaiGiang >= today)
                .OrderBy(c => c.NgayKhaiGiang)
                .ToList();
        }


        public List<ClassRoom> LopDangHoc()
        {
            return classes
                .Where(c => c.TrangThai == "Đang học")
                .ToList();
        }

        public bool DongLop(string maLop)
        {
            ClassRoom? classRoom = GetById(maLop);

            if (classRoom == null)
            {
                return false;
            }

            classRoom.TrangThai = "Đã đóng";

            return true;
        }

        public bool HuyLop(string maLop)
        {
            ClassRoom? classRoom = GetById(maLop);

            if (classRoom == null)
            {
                return false;
            }

            classRoom.TrangThai = "Đã hủy";

            return true;
        }
        public void LoadData(List<ClassRoom> data)
        {
            classes.Clear();

            foreach (ClassRoom classRoom in data)
            {
                Add(classRoom);
            }
        }
    }
}