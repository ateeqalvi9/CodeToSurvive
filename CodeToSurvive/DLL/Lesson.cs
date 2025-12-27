using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class Lesson
    {
        public int LessonId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string task { get; set; }
        public Assignment Assignment { get; set; }
    }
}
