using DevmasterTrainingManagement.Domain;

namespace DevmasterTrainingManagement.Application
{
    public class CourseService
    {
        private List<Course> courses = new List<Course>();

        public List<Course> GetAll()
        {
            return courses;
        }

        public bool Add(Course course)
        {
            if (courses.Any(c => c.MaKhoaHoc == course.MaKhoaHoc))
            {
                return false;
            }

            courses.Add(course);
            return true;
        }

   
        public Course? GetById(string maKhoaHoc)
        {
            return courses.FirstOrDefault(
                c => c.MaKhoaHoc == maKhoaHoc);
        }

        public bool Update(Course course)
        {
            Course? oldCourse = GetById(course.MaKhoaHoc);

            if (oldCourse == null)
            {
                return false;
            }

            oldCourse.TenKhoaHoc = course.TenKhoaHoc;
            oldCourse.HocPhi = course.HocPhi;
            oldCourse.ThoiLuong = course.ThoiLuong;
            oldCourse.MoTa = course.MoTa;
            oldCourse.TrangThai = course.TrangThai;

            return true;
        }


        public bool Delete(string maKhoaHoc)
        {
            Course? course = GetById(maKhoaHoc);

            if (course == null)
            {
                return false;
            }

            courses.Remove(course);
            return true;
        }

        public List<Course> Search(string keyword)
        {
            return courses
                .Where(c =>
                    c.MaKhoaHoc.Contains(keyword,
                        StringComparison.OrdinalIgnoreCase)
                    ||
                    c.TenKhoaHoc.Contains(keyword,
                        StringComparison.OrdinalIgnoreCase))
                .ToList();
        }


        public List<Course> SortByFee()
        {
            return courses
                .OrderBy(c => c.HocPhi)
                .ToList();
        }

        public List<Course> FilterByStatus(string status)
        {
            return courses
                .Where(c => c.TrangThai == status)
                .ToList();
        }

        public decimal GetTotalFee()
        {
            return courses.Sum(c => c.HocPhi);
        }
        public void LoadData(List<Course> data)
        {
            courses.Clear();

            foreach (Course course in data)
            {
                Add(course);
            }
        }
    }
}