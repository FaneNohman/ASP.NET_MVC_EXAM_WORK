using Microsoft.EntityFrameworkCore;

namespace CORE_MVC_EXAM.Models
{
    public class SeedData { 
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new AppDbContext(serviceProvider.GetRequiredService<DbContextOptions<AppDbContext>>()))
            {
                var instr = new List<Instrument>()
                    {
                        new Instrument()
                        {
                            InstrumentName="Electro guitar"
                        },new Instrument()
                        {
                            InstrumentName="Classic guitar"
                        },
                        new Instrument()
                        {
                            InstrumentName="Vocal"
                        },
                        new Instrument()
                        {
                            InstrumentName="Saxophone"
                        }
                    };
                if (!context.Instruments.Any()) {
                    
                    context.AddRange(instr);
                }

                var roles = new List<Role>
                    {
                       new Role()
                       {
                           RoleName="admin"
                       },
                       new Role()
                       {
                           RoleName="teacher"
                       }
                       ,
                       new Role()
                       {
                           RoleName="student"
                       }
                    };
                if (!context.Roles.Any())
                {
                    
                    context.Roles.AddRange(roles);
                }

                var users = new List<User>
                    {
                        new User()
                        {
                            Login="admin",
                            Password="123456",
                            FirstName="admin",
                            LastName="admin",
                            CreatedDate=DateTime.Now,
                            LastModifiedDate=DateTime.Now,
                            Role = roles[0],
                            RoleId = roles[0].RoleId
                        },
                        new User()
                        {
                            Login="teacher1",
                            Password="123456",
                            FirstName="teacher2",
                            LastName="teacher2",
                            CreatedDate=DateTime.Now,
                            LastModifiedDate=DateTime.Now,
                            Role = roles[1],
                            RoleId = roles[1].RoleId

                        },
                        new User()
                        {
                            Login="teacher2",
                            Password="123456",
                            FirstName="teacher1",
                            LastName="teacher1",
                            CreatedDate=DateTime.Now,
                            LastModifiedDate=DateTime.Now,
                            Role = roles[1],
                            RoleId = roles[1].RoleId

                        },
                        new User()
                        {
                            Login="student1",
                            Password="123456",
                            FirstName="student1",
                            LastName="student1",
                            CreatedDate=DateTime.Now,
                            LastModifiedDate=DateTime.Now,
                            Role = roles[2],
                            RoleId = roles[2].RoleId
                        },
                        new User()
                        {
                            Login="student2",
                            Password="123456",
                            FirstName="student2",
                            LastName="student2",
                            CreatedDate=DateTime.Now,
                            LastModifiedDate=DateTime.Now,
                            Role = roles[2],
                            RoleId = roles[2].RoleId
                        }
                    };
                if (!context.Users.Any())
                {
                    context.Users.AddRange(users);
                }
                var lessons = new List<Lesson> 
                { 
                    new Lesson() 
                    { 
                        Instrument = instr[0],
                        User = users[1],
                        UserId = users[1].UserId,
                        LessonAt = DateTime.Now,
                    },
                    new Lesson()
                    {
                        Instrument = instr[1],
                        User = users[2],
                        UserId = users[2].UserId,
                        LessonAt = DateTime.Now,
                    }
                };
                if (!context.Users.Any())
                {
                    context.Lessons.AddRange(lessons);
                }
                var userLesson = new List<UserLesson>()
                {
                    new UserLesson()
                    {
                        User = users[3],
                        UserId = users[3].UserId,
                        Lesson = lessons[0],
                        LessonId = lessons[0].LessonId
                    },
                    new UserLesson()
                    {
                        User = users[4],
                        UserId = users[4].UserId,
                        Lesson = lessons[0],
                        LessonId = lessons[0].LessonId
                    }
                };
                if (!context.UserLessons.Any())
                {
                    context.UserLessons.AddRange(userLesson);
                }

                context.SaveChanges();
            }

        }
    }
}
