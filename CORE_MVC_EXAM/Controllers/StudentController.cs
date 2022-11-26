using CORE_MVC_EXAM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_EXAM.Controllers
{
    [Authorize(Roles="student")]
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Users.Include(x => x.UserLessons).ThenInclude(x => x.Lesson).FirstOrDefault(x => x.UserId.ToString() == User.Identity.Name);
            return View(appDbContext.UserLessons);
        }
        public async Task<IActionResult> Lesson(Guid id)
        {
            Lesson? lesson = _context.Lessons.Include(x=>x.User).Include(x=>x.Instrument).FirstOrDefault(x => x.LessonId == id);
            if (lesson is null) return NotFound();
            return View(lesson);
        }
    }
}
