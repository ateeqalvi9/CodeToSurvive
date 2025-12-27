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
                Description = "Learn core programming concepts.",
                SkillGain = 3,
                EnergyCost = 15,

                Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Title = "Variables & Loops",
                        Content = "C++ variables, loops, and syntax.",
                        Assignment = new Assignment
                        {
                            Task = "Write a loop using for.",
                            TimeLimitSeconds = 120
                        }
                    }
                },

                Exam = new Exam
                {
                    Title = "C++ Exam",
                    Question = "Which loop repeats a fixed number of times?",
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
                EnergyCost = 20,

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
                Description = "HTML, CSS and basics of backend.",
                SkillGain = 5,
                EnergyCost = 25,

                Lessons = new List<Lesson>
                {
                    new Lesson
                    {
                        Title = "HTML Basics",
                        Content = "HTML structure and tags.",
                        Assignment = new Assignment
                        {
                            Task = "Create a basic HTML page.",
                            TimeLimitSeconds = 180
                        }
                    }
                },

                Exam = new Exam
                {
                    Title = "Web Exam",
                    Question = "What tag defines a hyperlink?",
                    CorrectAnswer = "a"
                }
            };
        }
    }
}
