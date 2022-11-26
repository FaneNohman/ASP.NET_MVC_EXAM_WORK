using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CORE_MVC_EXAM.Models;
using Microsoft.AspNetCore.Authorization;

namespace CORE_MVC_EXAM.Controllers
{
    [Authorize(Roles = "admin")]
    public class UserLessonsController : Controller
    {
        private readonly AppDbContext _context;

        public UserLessonsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: UserLessons
        public async Task<IActionResult> Index()
        {
            var appDbContext = _context.UserLessons.Include(u => u.Lesson).Include(u => u.User);
            return View(await appDbContext.ToListAsync());
        }

        // GET: UserLessons/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null || _context.UserLessons == null)
            {
                return NotFound();
            }

            var userLesson = await _context.UserLessons
                .Include(u => u.Lesson)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userLesson == null)
            {
                return NotFound();
            }

            return View(userLesson);
        }

        // GET: UserLessons/Create
        public IActionResult Create()
        {
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId");
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId");
            return View();
        }

        // POST: UserLessons/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId,LessonId,Grade,Attendance")] UserLesson userLesson)
        {
            if (ModelState.IsValid)
            {
                userLesson.UserId = Guid.NewGuid();
                _context.Add(userLesson);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", userLesson.LessonId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userLesson.UserId);
            return View(userLesson);
        }

        // GET: UserLessons/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null || _context.UserLessons == null)
            {
                return NotFound();
            }

            var userLesson = await _context.UserLessons.FindAsync(id);
            if (userLesson == null)
            {
                return NotFound();
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", userLesson.LessonId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userLesson.UserId);
            return View(userLesson);
        }

        // POST: UserLessons/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("UserId,LessonId,Grade,Attendance")] UserLesson userLesson)
        {
            if (id != userLesson.UserId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userLesson);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserLessonExists(userLesson.UserId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["LessonId"] = new SelectList(_context.Lessons, "LessonId", "LessonId", userLesson.LessonId);
            ViewData["UserId"] = new SelectList(_context.Users, "UserId", "UserId", userLesson.UserId);
            return View(userLesson);
        }

        // GET: UserLessons/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null || _context.UserLessons == null)
            {
                return NotFound();
            }

            var userLesson = await _context.UserLessons
                .Include(u => u.Lesson)
                .Include(u => u.User)
                .FirstOrDefaultAsync(m => m.UserId == id);
            if (userLesson == null)
            {
                return NotFound();
            }

            return View(userLesson);
        }

        // POST: UserLessons/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            if (_context.UserLessons == null)
            {
                return Problem("Entity set 'AppDbContext.UserLessons'  is null.");
            }
            var userLesson = await _context.UserLessons.FindAsync(id);
            if (userLesson != null)
            {
                _context.UserLessons.Remove(userLesson);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserLessonExists(Guid id)
        {
          return _context.UserLessons.Any(e => e.UserId == id);
        }
    }
}
