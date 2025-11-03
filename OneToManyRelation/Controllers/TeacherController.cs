using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OneToManyRelation.Data.Entities;
using OneToManyRelation.Data;
using OneToManyRelation.ViewModels.Teacher;
using OneToManyRelation.ViewModels;

namespace OneToManyRelation.Controllers
{
    public class TeacherController : Controller
    {
        private readonly ApplicationDbContext _context;
        public TeacherController(ApplicationDbContext context)
        {
            _context = context;
        }
        public IActionResult Index(TeacherTableViewModel model)
        {
            IQueryable<Teacher> query = _context.Set<Teacher>();
            model.Data = query.ToList();
            return View(model);
        }

        public IActionResult Create()
        {
            TeacherViewModel model = new TeacherViewModel();
            return View(model);
        }

        [HttpPost]
        public IActionResult Create(TeacherViewModel viewModel)
        {
            Teacher entity = new Teacher();
            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.MapTo(entity);
                    _context.Teachers.Add(entity);
                    _context.SaveChanges();
                    TempData["success"] = $"Учителят \"{entity.Name}\" е добавен успешно!";
                }
                catch
                {
                    TempData["error"] = "Учителят не може да бъде добавен!";
                }
            }
            return View(viewModel);
        }

        public IActionResult Update(int Id)
        {
            Teacher? entity = _context.Teachers.Find(Id);

            if (entity == null)
            {
                TempData["error"] = "Учителят не може да бъде намерен!";
                return RedirectToAction("Index");
            }
            TeacherViewModel viewModel = new TeacherViewModel();
            viewModel.MapFrom(entity);
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(TeacherViewModel viewModel)
        {
            Teacher entity = _context.Teachers.Find(viewModel.TeacherId);

            if (ModelState.IsValid)
            {
                try
                {
                    viewModel.MapTo(entity);
                    _context.Teachers.Update(entity);
                    _context.SaveChanges();
                    TempData["success"] = $"Учителят \"{entity.Name}\" е записан успешно!";
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
            Teacher? entity = _context.Teachers.Find(Id);
            if (entity == null)
            {
                TempData["error"] = "Такъв учител не съществува!";
                return RedirectToAction("Index");
            }
            try
            {
                _context.Teachers.Remove(entity);
                _context.SaveChanges();
                TempData["success"] = $"Учителят \"{entity.Name}\" е изтрит успешно!";
            }
            catch
            {
                TempData["error"] = $"Възникна грешка при изтриването!";
            }
            return RedirectToAction("Index");
        }
    }
}
