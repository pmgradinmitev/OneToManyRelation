using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToManyRelation.Common.Utils;
using OneToManyRelation.Data;
using OneToManyRelation.Data.Entities;
using OneToManyRelation.ViewModels;
using OneToManyRelation.ViewModels.Student;

namespace OneToManyRelation.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;
        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(StudentTableViewModel model)
        {
            IQueryable<Student> query = _context.Set<Student>();
            query = query.Include(s => s.Teacher);
            model.Data = query.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            StudentViewModel model = new StudentViewModel();
            model.Teachers = DropdownHelper.GetTeacherList(_context);
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(StudentViewModel viewModel)
        {
            Student entity = new Student();
            viewModel.Teachers = DropdownHelper.GetTeacherList(_context);
            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.MapTo(entity);
                    _context.Students.Add(entity);
                    _context.SaveChanges();
                    TempData["success"] = $"Ученикът \"{entity.Name}\" е добавен успешно!";
                }
                catch
                {
                    TempData["error"] = "Ученикът не може да бъде добавен!";
                }
            }
            return View(viewModel);
        }

        public IActionResult Update(int Id)
        {
            Student? entity = _context.Students.Find(Id);
            if (entity == null)
            {
                TempData["error"] = "Ученикът не може да бъде намерен!";
                return RedirectToAction("Index");
            }
            StudentViewModel viewModel = new StudentViewModel();
            viewModel.Teachers = DropdownHelper.GetTeacherList(_context);
            viewModel.MapFrom(entity);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(StudentViewModel viewModel)
        {
            Student entity = _context.Students.Find(viewModel.StudentId);
            viewModel.Teachers = DropdownHelper.GetTeacherList(_context);
            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.MapTo(entity);
                    _context.Students.Update(entity);
                    _context.SaveChanges();
                    TempData["success"] = $"Ученикът \"{entity.Name}\" е записан успешно!";
                }
                catch
                {
                    TempData["error"] = "Промените не бяха записани!";
                }
            }
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            Student? entity = _context.Students.Find(Id);
            if (entity == null)
            {
                TempData["error"] = "Такъв ученик не съществува!";
                return RedirectToAction("Index");
            }
            try
            {
                _context.Students.Remove(entity);
                _context.SaveChanges();
                TempData["success"] = $"Ученикът \"{entity.Name}\" е изтрит успешно!";
            }
            catch
            {
                TempData["error"] = $"Възникна грешка при изтриването!";
            }
            return RedirectToAction("Index");
        }
    }
}
