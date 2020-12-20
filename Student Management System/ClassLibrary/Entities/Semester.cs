using System.Collections.Generic;

namespace ClassLibrary.Entities
{
    public class Semester
    {
        public string SemesterCode { get; set; }
        public string Year { get; set; }
        public List<Course> Courses { get; set; }

        public string SemCodeResult(string code)
        {
            var semester = "";
            switch (code)
            {
                case "SUM":
                    semester= "Summer";
                    break;
                case "FAL":
                    semester = "Fall";
                    break;
                case "SPR":
                    semester= "Spring";
                    break;
            }
            return semester;
        }
    }
}