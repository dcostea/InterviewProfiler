using System;
using System.Collections.Generic;

namespace InterviewProfiler.Models
{
    public partial class Question
    {
        public int QuestionId { get; set; }
        public string Title { get; set; }
        public int SkillId { get; set; }
        public string Answer50 { get; set; }
        public string Answer100 { get; set; }
        public string Source { get; set; }
        public string Meta { get; set; }
        public DateTime Modified { get; set; }

        public Skill Skill { get; set; }
    }
}
