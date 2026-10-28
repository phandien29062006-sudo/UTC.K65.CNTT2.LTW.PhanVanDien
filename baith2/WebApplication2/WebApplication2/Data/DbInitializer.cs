
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApplication2.Models;

namespace WebApplication2.Data
{
    public class DbInitializer
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new SchoolContext(
                serviceProvider.GetRequiredService<
                    DbContextOptions<SchoolContext>>()))
            {
                context.Database.EnsureCreated();

                if (context.Majors.Any())
                {
                    return;
                }

                // 1. Thêm dữ liệu ngành học
                var majors = new Major[]
                {
                    new Major { MajorName = "IT" },
                    new Major { MajorName = "Economics" },
                    new Major { MajorName = "Mathematics" }
                };

                foreach (var major in majors)
                {
                    context.Majors.Add(major);
                }

                context.SaveChanges();

                // 2. Thêm dữ liệu người học
                var learners = new Learner[]
                {
                    new Learner
                    {
                        FirstMidName = "Carson",
                        LastName = "Alexander",
                        EnrollmentDate = DateTime.Parse("2005-09-01"),
                        MajorID = 1
                    },
                    new Learner
                    {
                        FirstMidName = "Meredith",
                        LastName = "Alonso",
                        EnrollmentDate = DateTime.Parse("2002-09-01"),
                        MajorID = 2
                    }
                };

                foreach (var learner in learners)
                {
                    context.Learners.Add(learner);
                }

                context.SaveChanges();

                // 3. Thêm dữ liệu môn học
                var courses = new Course[]
                {
                    new Course
                    {
                        CourseID = 1050,
                        Title = "Chemistry",
                        Credits = 3
                    },
                    new Course
                    {
                        CourseID = 4022,
                        Title = "Microeconomics",
                        Credits = 3
                    },
                    new Course
                    {
                        CourseID = 4041,
                        Title = "Macroeconomics",
                        Credits = 3
                    }
                };

                foreach (var course in courses)
                {
                    context.Courses.Add(course);
                }

                context.SaveChanges();

                // 4. Thêm dữ liệu đăng ký môn học
                var enrollments = new Enrollment[]
                {
                    new Enrollment
                    {
                        LearnerID = 1,
                        CourseID = 1050,
                        Grade = 5.5f
                    },
                    new Enrollment
                    {
                        LearnerID = 1,
                        CourseID = 4022,
                        Grade = 7.5f
                    },
                    new Enrollment
                    {
                        LearnerID = 2,
                        CourseID = 1050,
                        Grade = 3.5f
                    },
                    new Enrollment
                    {
                        LearnerID = 2,
                        CourseID = 4041,
                        Grade = 7f
                    }
                };

                foreach (var enrollment in enrollments)
                {
                    context.Enrollments.Add(enrollment);
                }

                context.SaveChanges();
            }
        }
    }
}
