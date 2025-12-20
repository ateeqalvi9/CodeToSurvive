using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeToSurvive.DLL
{
    public class Exam
    {
        public int PassingScore { get; set; }
        public string Title { get; set; }
        public string Question { get; set; }
        public string CorrectAnswer { get; set; }

        // Optional difficulty / time
        public int TimeLimitSeconds { get; set; }
    }
}
