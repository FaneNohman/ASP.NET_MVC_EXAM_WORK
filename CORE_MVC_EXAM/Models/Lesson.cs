namespace CORE_MVC_EXAM.Models
{
    public class Lesson
    {
        public Guid LessonId { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; }
        public DateTime LessonAt { get; set; }
        public Instrument Instrument { get; set; }
        public ICollection<UserLesson> UserLessons { get; set; }
    }
}
