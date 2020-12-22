using System;
using System.Collections.Generic;
using ClassLibrary.Entities;

namespace ClassLibrary
{
    public class CourseComparer : IEqualityComparer<Course>
    {
        public bool Equals(Course courses1, Course courses2)
        {
            return courses2 != null && courses1 != null && courses1.CourseId == courses2.CourseId && string.Equals(courses1.CourseName, courses2.CourseName, StringComparison.CurrentCultureIgnoreCase) && courses1.InstructorName == courses2.InstructorName && courses1.NumberOfCredits == courses2.NumberOfCredits;
        }
        public int GetHashCode(Course obj)
        {
            return obj.CourseId.GetHashCode();
        }
    }
}