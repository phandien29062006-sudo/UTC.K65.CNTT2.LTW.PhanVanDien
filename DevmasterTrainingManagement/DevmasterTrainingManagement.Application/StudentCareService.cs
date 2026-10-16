using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Application
{
    public class StudentCareService
    {
        private List<StudentCare> studentCares = new List<StudentCare>();

       
        public List<StudentCare> GetAll()
        {
            return studentCares;
        }

  
        public bool Add(StudentCare studentCare)
        {
            if (studentCares.Any(
                s => s.MaChamSoc == studentCare.MaChamSoc))
            {
                return false;
            }

            studentCares.Add(studentCare);
            return true;
        }


        public List<StudentCare> GetByStudent(string maHocVien)
        {
            return studentCares
                .Where(s => s.MaHocVien == maHocVien)
                .OrderByDescending(s => s.NgayChamSoc)
                .ToList();
        }


        public List<StudentCare> GetTodayAppointments()
        {
            DateTime today = DateTime.Today;

            return studentCares
                .Where(s =>
                    s.NgayHenTiepTheo.HasValue &&
                    s.NgayHenTiepTheo.Value.Date == today)
                .OrderBy(s => s.NgayHenTiepTheo)
                .ToList();
        }

  
        public List<StudentCare> GetOverdueAppointments()
        {
            DateTime today = DateTime.Today;

            return studentCares
                .Where(s =>
                    s.NgayHenTiepTheo.HasValue &&
                    s.NgayHenTiepTheo.Value.Date < today)
                .OrderBy(s => s.NgayHenTiepTheo)
                .ToList();
        }


        public Dictionary<string, int> StatisticsResult()
        {
            return studentCares
                .GroupBy(s => s.KetQua)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
        }
        public void LoadData(List<StudentCare> data)
        {
            studentCares.Clear();

            foreach (StudentCare studentCare in data)
            {
                Add(studentCare);
            }
        }
    }
}