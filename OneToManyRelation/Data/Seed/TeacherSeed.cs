using Microsoft.EntityFrameworkCore;
using OneToManyRelation.Data.Entities;

namespace OneToManyRelation.Data.Seed
{
    public class TeacherSeed
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new ApplicationDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<ApplicationDbContext>>());

            if (context.Teachers.Any())
                return;

            var teacher1 = new Teacher
            {
                Name = "Иван Петров",
                Subject = "Начален учител",
                Students = new List<Student>
                {
                    new Student { Name = "Петър Иванов", Age = 8 },
                    new Student { Name = "Анна Георгиева", Age = 8 }
                }
            };

            var teacher2 = new Teacher
            {
                Name = "Мария Стоинова",
                Subject = "Начален учител",
                Students = new List<Student>
                {
                    new Student { Name = "Георги Тодоров", Age = 7 }
                }
            };

            context.Teachers.AddRange(teacher1, teacher2);
            context.SaveChanges();
        }
    }
}
