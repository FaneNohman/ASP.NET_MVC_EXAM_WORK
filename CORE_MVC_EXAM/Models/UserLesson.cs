namespace CORE_MVC_EXAM.Models
{
    public class UserLesson
    {
        public Guid UserId { get; set; }
        public User User { get; set; }
        public Guid LessonId { get; set; }
        public Lesson Lesson { get; set; }
        public int? Grade { get; set; }
        public bool? Attendance { get; set; }
    }
}
