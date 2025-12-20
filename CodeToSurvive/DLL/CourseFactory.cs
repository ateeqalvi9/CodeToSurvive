using System.Collections.Generic;

namespace CodeToSurvive.DLL
{
    public static class CourseFactory
    {
        public static Course CreateCppCourse()
        {
            return new Course
            {
                Name = "C++ Programming",
                Description = "Learn basics of C++ programming.",
                SkillGain = 3,
                EnergyCost = 10,
                DurationDays = 2,

                Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Title = "Variables & Loops",
                        Content = "Learn variables, loops and syntax.",
                        Assignment = new Assignment
                        {
                            Task = "Write a loop that prints numbers 1 to 5.",
                            TimeLimitSeconds = 120
                        }
                    }
                },

                Exam = new Exam
                {
                    Title = "C++ Final Exam",
                    Question = "Write a simple for-loop.",
                    CorrectAnswer = "for"
                }
            };
        }

        public static Course CreateCSharpCourse()
        {
            return new Course
            {
                Name = "C# .NET",
                Description = "Learn OOP and .NET basics.",
                SkillGain = 4,
                EnergyCost = 12,
                DurationDays = 2,

                Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Title = "Classes & Objects",
                        Content = "Understand classes and objects.",
                        Assignment = new Assignment
                        {
                            Task = "Create a class with a method.",
                            TimeLimitSeconds = 150
                        }
                    }
                },

                Exam = new Exam
                {
                    Title = "C# Exam",
                    Question = "What keyword defines a class?",
                    CorrectAnswer = "class"
                }
            };
        }
        public static Course CreateWebCourse()
        {
            return new Course
            {
                Name = "Web Development",
                Description = "Learn HTML, CSS, JavaScript, and basic web logic.",
                SkillGain = 5,
                EnergyCost = 14,
                DurationDays = 2,

                Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Title = "HTML Basics",
                        Content = "Learn how to structure web pages using HTML tags.",
                        Assignment = new Assignment
                        {
                            Task = "Create a basic HTML page with headings and a paragraph.",
                            TimeLimitSeconds = 120
                        }
                    },
                    new Lesson
                    {
                        Title = "CSS Styling",
                        Content = "Learn how to style web pages using CSS.",
                        Assignment = new Assignment
                        {
                            Task = "Style a div using CSS (color, margin, padding).",
                            TimeLimitSeconds = 150
                        }
                    }
                },

                Exam = new Exam
                {
                    Title = "Web Development Exam",
                    Question = "Which HTML tag is used for hyperlinks?",
                    CorrectAnswer = "<a>"
                }
            };
        }

    }
}
