namespace CDocs.Core
{
    /// <summary>
    /// Представляет устранение нарушения.
    /// </summary>
    public class Fix
    {
        public Fix(string number, string responsible, bool notified, bool done)
        {
            Number = number;
            Responsible = responsible;
            Notified = notified;
            Done = done;
        }

        public string Number { get; }
        public string Responsible { get; }
        public bool Notified { get; }
        public bool Done { get; }
    }
}
