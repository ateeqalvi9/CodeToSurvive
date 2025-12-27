using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class CourseProgress
    {
        public int LessonId { get; set; }
        public double ProgressPercent { get; set; }
        public int ProgressId { get; set; }
        public int PlayerId { get; set; }
        public int CourseId { get; set; }
        public int CurrentLessonIndex { get; set; }
        public bool IsCompleted { get; set; }
        public DateTime LastUpdated { get; internal set; }
    }
}
