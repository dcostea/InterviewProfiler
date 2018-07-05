using System;
using System.Collections.Generic;

namespace InterviewProfiler.Models
{
    public partial class Skill
    {
        public Skill()
        {
            Question = new HashSet<Question>();
        }

        public int SkillId { get; set; }
        public string Name { get; set; }
        public int Level { get; set; }
        public string Tech { get; set; }

        public ICollection<Question> Question { get; set; }
    }
}
