namespace CDocs.Core
{
    /// <summary>
    /// Интерфейс, являющийся базовым для всех документов (отчетов).
    /// </summary>
    public interface IReport
    {
        /// <summary>
        /// Получает номер отчета.
        /// </summary>
        string Number { get; }

        // TODO: public List<IRenderable> Elements { get; }
    }
}
