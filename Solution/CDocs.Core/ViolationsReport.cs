namespace CDocs.Core
{
    /// <summary>
    /// Представляет документ в форме 33.
    /// </summary>
    public class ViolationsReport : IReport
    {
        public ViolationsReport(string number)
        {
            Number = number;
            Violations = new();
        }

        public ViolationsReport(string number, params Violation[] violations)
        {
            Number = number;
            Violations = violations != null ? violations.ToList() : new();
        }

        public string Number { get; }

        public List<Violation> Violations { get; }
    }
}
