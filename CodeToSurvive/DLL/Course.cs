using System.Collections.Generic;

namespace CodeToSurvive.DLL
{
    public class Course
    {
        public int CourseId { get; set; }
        public string Name { get; set; }
        public int SkillReward { get; set; }
        public int DurationDays { get; set; }
        public List<Lesson> Lessons { get; set; } = new List<Lesson>();
        public Assignment Assignment { get; set; }
        public Exam Exam { get; set; }
        public string Description { get; set; }

        public int SkillGain { get; set; }
        public int EnergyCost { get; set; }

        public int Progress { get; set; } = 0;

        public bool LessonCompleted { get; set; }
        public bool AssignmentCompleted { get; set; }
        public bool ExamCompleted { get; set; }
    }
}
