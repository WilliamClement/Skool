using System.Text.RegularExpressions;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace CDocs.Core
{
    /// <summary>
    /// Обработчик документа, представляющий его как объект, производный от <see cref="IReport"/>.
    /// </summary>
    public class DocumentHandler
    {
        /// <summary>
        /// Создает экземпляр и синхронно читает и обрабатывает содержание документа.
        /// </summary>
        /// <param name="path">Расположение документа.</param>
        public DocumentHandler(Uri path)
        {
            Path = path.AbsolutePath;
            Document = DocX.Load(Path);
            Report = ParseDocument();
        }

        /// <summary>
        /// Абсолютное расположение документа.
        /// </summary>
        public string Path { get; }

        /// <summary>
        /// Низкоуровневое представление документа.
        /// </summary>
        public DocX Document { get; }

        /// <summary>
        /// Представление документа как отчета.
        /// </summary>
        public IReport Report { get; }

        /// <summary>
        /// В зависимости от формы документа, разбирает его как <see cref="ViolationsReport"/>
        /// или как <see cref="FixesReport"/>.
        /// </summary>
        /// <returns>Возвращает созданный экземпляр через базовый интерфейс.</returns>
        protected IReport ParseDocument()
        {
            var form_raw = ParseDocumentForm();

            if (string.IsNullOrEmpty(form_raw))
            {
                throw new Exception($"Не удалось обработать документ '{Path}' в неизвестном формате.");
            }

            var form = form_raw.ToLower();
            if (form == "форма 33")
            {
                return ParseViolationsReport();
            }
            else if (form == "форма 34")
            {
                return ParseFixesReport();
            }
            throw new Exception($"Не удалось обработать документ '{Path}' в неизвестной форме '{form}'.");
        }

        /// <summary>
        /// Получает форму документа из последней ячейки первой строки первой таблицы.
        /// </summary>
        /// <returns>Возвращает текст абзацев данной ячейки или null.</returns>
        protected string? ParseDocumentForm()
        {
            var table = Document.Tables.FirstOrDefault();
            if (table == default(Table))
            {
                return null;
            }

            var row = table.Rows.FirstOrDefault();
            if (row == default(Row))
            {
                return null;
            }

            var cell = row.Cells.LastOrDefault();
            if (cell == default(Cell))
            {
                return null;
            }

            var form = "";
            foreach (var paragraph in cell.Paragraphs)
            {
                form += paragraph.Text;
            }
            return form;
        }

        /// <summary>
        /// Разбирает документ как <see cref="ViolationsReport"/>.
        /// </summary>
        /// <returns>Возвращает созданный экземпляр через базовый интерфейс.</returns>
        protected IReport ParseViolationsReport()
        {
            var number = ParseDocumentNumber();
            var result = new ViolationsReport(number);

            foreach (var table in Document.Tables.Skip(1))
            {
                foreach (var row in table.Rows.Skip(1))
                {
                    var violation = new Violation(
                        number: GetText(row, 0),
                        kind: GetText(row, 1),
                        description: GetText(row, 3)
                    );
                    result.Violations.Add(violation);
                }
            }

            return result;
        }

        /// <summary>
        /// Разбирает документ как <see cref="FixesReport"/>.
        /// </summary>
        /// <returns>Возвращает созданный экземпляр через базовый интерфейс.</returns>
        protected IReport ParseFixesReport()
        {
            var number = ParseDocumentNumber();
            var violations_number = ParseDocumentViolationsNumber();
            var result = new FixesReport(number, violations_number);

            foreach (var table in Document.Tables.Skip(1))
            {
                foreach (var row in table.Rows.Skip(1))
                {
                    var fix = new Fix(
                        number: GetText(row, 0),
                        responsible: GetText(row, 1),
                        notified: GetText(row, 2) == "\u2612",
                        done: GetText(row, 3) == "\u2612"
                    );
                    result.Fixes.Add(fix);
                }
            }

            return result;
        }

        /// <summary>
        /// Получает номер документа для форм 33 и 34, расположенный на первой
        /// (объединенной) ячейки последней строки первой таблицы.
        /// </summary>
        /// <returns>Возвращает полученный номер.</returns>
        protected string ParseDocumentNumber()
        {
            var table = Document.Tables.First();
            var row = table.Rows.Last();
            var raw = "";
            foreach (var cell in row.Cells)
            {
                foreach (var paragraph in cell.Paragraphs)
                {
                    raw += paragraph.Text;
                }
            }
            return raw;
        }

        /// <summary>
        /// Получает номер отчета об нарушениях из первых абзацев после первой
        /// таблицы.
        /// </summary>
        /// <returns>Возвращает полученный номер.</returns>
        protected string ParseDocumentViolationsNumber()
        {
            var table = Document.Tables.First();
            var blah_paragraphs = table.Paragraphs.Count;

            var start = "обозначенные нарушения заявлены";
            Regex regex = new Regex(@"№\d{4,8}[_\-/]\w+");
            foreach (var paragraph in Document.Paragraphs.Skip(blah_paragraphs).Take(4))
            {
                if (paragraph.Text.ToLower().Trim().StartsWith(start))
                {
                    return regex.Match(paragraph.Text).Value;
                }
            }
            throw new Exception("В отчете об устранении нарушений не указан номер отчета об нарушениях.");
        }

        /// <summary>
        /// Получает текст абзацев определенной ячейки строки таблицы.
        /// </summary>
        /// <remarks>
        /// Не учитывает наличие вложенных в абзац таблиц.
        /// </remarks>
        /// <param name="row">Исходная строка.</param>
        /// <param name="col">Индекс ячейки.</param>
        /// <returns>Возвращает объединенный текст.</returns>
        protected string GetText(Row row, int col)
        {
            string result = "";
            foreach (var paragraph in row.Cells[col].Paragraphs)
            {
                result += paragraph.Text;
            }
            return result;
        }
    }
}
