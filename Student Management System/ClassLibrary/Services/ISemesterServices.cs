using System.Collections.Generic;
using ClassLibrary.Entities;

namespace ClassLibrary.Services
{
    public interface ISemesterServices
    {
        void AddSemester(string id, List<Semester> semester);
    }
}