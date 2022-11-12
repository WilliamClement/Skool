namespace CDocs.Core
{
    /// <summary>
    /// Представляет документ в форме 34.
    /// </summary>
    public class FixesReport : IReport
    {
        public FixesReport(string number, string violations_number)
        {
            Number = number;
            ViolationsNumber = violations_number;
            Fixes = new();
        }

        public FixesReport(string number, string violations_number, params Fix[] fixes)
        {
            Number = number;
            ViolationsNumber = violations_number;
            Fixes = fixes != null ? fixes.ToList() : new();
        }

        public string Number { get; }

        public string ViolationsNumber { get; }

        public List<Fix> Fixes { get; }
    }
}
