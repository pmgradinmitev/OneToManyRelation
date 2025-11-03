using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using Entities = OneToManyRelation.Data.Entities;

namespace OneToManyRelation.ViewModels
{
    public class TeacherViewModel
    {
        public int? TeacherId { get; set; }
        [DisplayName("Име")]
        public string Name { get; set; }
        [DisplayName("Предмет")]
        public string Subject { get; set; }

        public void MapTo(Entities.Teacher teacher)
        {
            teacher.Name = this.Name;
            teacher.Subject = this.Subject;
        }
        public void MapFrom(Entities.Teacher teacher)
        {
            TeacherId = teacher.Id;
            Name = teacher.Name;
            Subject = teacher.Subject;
        }
    }
}
