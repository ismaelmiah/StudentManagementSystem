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

        //public bool Equals(Semester x, Semester y)
        //{
        //    if (ReferenceEquals(x, y)) return true;
        //    if (ReferenceEquals(x, null)) return false;
        //    if (ReferenceEquals(y, null)) return false;
        //    if (x.GetType() != y.GetType()) return false;
        //    return x.SemesterCode == y.SemesterCode && x.Year == y.Year && Equals(x.Courses, y.Courses);
        //}

        //public int GetHashCode(Semester obj)
        //{
        //    unchecked
        //    {
        //        var hashCode = (obj.SemesterCode != null ? obj.SemesterCode.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (obj.Year != null ? obj.Year.GetHashCode() : 0);
        //        hashCode = (hashCode * 397) ^ (obj.Courses != null ? obj.Courses.GetHashCode() : 0);
        //        return hashCode;
        //    }
        //}
    }
}