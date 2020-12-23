namespace ClassLibrary.Models
{
    public static class StringExtensions
    {
        public static string SemCodeResult(this string code, string semesterCode)
        {
            var semester = semesterCode switch
            {
                "SUM" => "Summer",
                "FAL" => "Fall",
                "SPR" => "Spring",
                _ => ""
            };
            return semester;
        }
    }
}