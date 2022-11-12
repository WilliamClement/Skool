namespace CDocs.Core
{
    /// <summary>
    /// Представляет нарушение.
    /// </summary>
    public class Violation
    {
        public Violation(string number, string kind, string description)
        {
            Number = number;
            Kind = kind;
            Description = description;
        }

        public string Number { get; }
        public string Kind { get; }
        public string Description { get; }
    }
}
