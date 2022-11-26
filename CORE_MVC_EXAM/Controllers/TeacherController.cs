using CORE_MVC_EXAM.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_EXAM.Controllers
{
    [Authorize(Roles ="teacher")]
    public class TeacherController : Controller
    {
        private readonly AppDbContext _context;

        public TeacherController(AppDbContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.Lessons.Where(x=>x.UserId.ToString()==User.Identity.Name);
            return View( await appDbContext.ToListAsync());
        }
        public async Task<IActionResult> Students(Guid id)
        {
            var userLessons =  _context.UserLessons.Where(x => x.LessonId == id).Include(x => x.User);
            var students = from s in userLessons select s.User;
            return View(students);
        }
    }
}
