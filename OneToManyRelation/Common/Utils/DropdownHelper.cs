using Microsoft.AspNetCore.Mvc.Rendering;
using OneToManyRelation.Data;

namespace OneToManyRelation.Common.Utils
{
    public static class DropdownHelper
    {
        public static List<SelectListItem> GetTeacherList(ApplicationDbContext context)
        {
            return context.Teachers
            .Select(t => new SelectListItem
            {
                Value = t.Id.ToString(),
                Text = t.Name
            })
            .ToList();
        }
    }
}
