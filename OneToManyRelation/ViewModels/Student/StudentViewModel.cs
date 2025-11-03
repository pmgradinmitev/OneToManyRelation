using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Entities = OneToManyRelation.Data.Entities;

namespace OneToManyRelation.ViewModels
{
    public class StudentViewModel
    {
        public int? StudentId { get; set; }
        [DisplayName("Име")]
        public string Name { get; set; }
        [DisplayName("Години")]
        [Required]
        public int? Age { get; set; }
        [DisplayName("Преподавател")]
        [Required]
        public int? TeacherId { get; set; }
        [ValidateNever]
        public List<SelectListItem> Teachers { get; set; }

        public void MapTo(Entities.Student student)
        {
            student.Name = this.Name;
            student.Age = (int)this.Age;
            student.TeacherId = (int)this.TeacherId;
        }
        public void MapFrom(Entities.Student student)
        {
            StudentId = student.Id;
            Name = student.Name;
            Age = student.Age;
            TeacherId = student.TeacherId;
        }
    }
}
