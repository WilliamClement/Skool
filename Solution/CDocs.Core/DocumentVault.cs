using System.Collections;
using System.ComponentModel;

namespace CDocs.Core
{
    /// <summary>
    /// Хранилище документов, представляемое каталогом на локальном компьютере.
    /// </summary>
    /// <remarks>
    /// Используйте перечисление экземпляра настоящего класса для получения обработчиков
    /// документов, расположенных в переданном хранилище, а также обрабатывайте событие
    /// <see cref="ErrorsChanged"/> для получения информации об возникших исключениях в
    /// ходе создания обработчиков.
    /// <code>
    /// var vault = new DocumentVault(path);
    /// 
    /// // Определяем обработчик ошибок по мере их поступления:
    /// vault.ErrorsChanged += (sender, e) =>
    /// {
    ///     foreach (string error in vault.GetErrors())
    ///     {
    ///         // Выводим ошибку пользователю:
    ///         // ...
    ///     }
    /// }
    /// 
    /// // Обрабатываем документы:
    /// foreach (var handler in vault)
    /// {
    ///     // ...
    /// }
    /// 
    /// // Проверим наличие ошибок после обработки:
    /// foreach (string error in vault.GetErrors())
    /// {
    ///     // Выводим ошибку пользователю:
    ///     // ...
    /// }
    /// </code>
    /// </remarks>
    public class DocumentVault : IEnumerable<DocumentHandler>, INotifyDataErrorInfo
    {
        /// <summary>
        /// Создает экземпляр для входного каталога.
        /// </summary>
        /// <param name="path">Расположение хранилища.</param>
        public DocumentVault(Uri path)
        {
            Location = path.AbsolutePath;
        }

        /// <summary>
        /// Абсолютное расположение хранилища документов.
        /// </summary>
        public string Location { get; }

        /// <summary>
        /// Список ссылок на созданные обработчики документов.
        /// </summary>
        /// <remarks>
        /// Представляет собой некий кэш загрузки с файловой системы, который используется при
        /// повторном перечислении хранилища.
        /// </remarks>
        protected List<DocumentHandler> Handlers = new();

        /// <summary>
        /// Список исключений, возникших при создании обработчиков документов.
        /// </summary>
        protected List<Exception> Exceptions = new();

        /// <summary>
        /// Получает наличие ошибок после первого перечисления хранилища.
        /// </summary>
        public bool HasErrors => Exceptions.Count > 0;

        /// <summary>
        /// Событие, возникающее при ошибке создания обработчика документа.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        /// <summary>
        /// Получает перечисление обработчиков документов настоящего хранилища.
        /// </summary>
        /// <returns>Возвращает обработчики документов.</returns>
        public IEnumerator<DocumentHandler> GetEnumerator()
        {
            if (Handlers.Count != 0)
            {
                foreach (var cached in Handlers)
                {
                    yield return cached;
                }
                yield break;
            }

            DocumentHandler handler = null;
            var documents = Directory.GetFiles(Location, searchPattern: "*.doc?");
            foreach (var path in documents)
            {
                try
                {
                    handler = new DocumentHandler(new Uri(path));
                    Handlers.Add(handler);
                }
                // TODO: IOException - можно поймать когда документ открыт в Microsoft Word,
                // чтобы уведомить пользователя и отложить загрузку path на некоторое время.
                // Или лучше сделать метод повторной загрузки неудавшихся?
                catch (Exception exception)
                {
                    var message = $"Не удалось обработать документ '{path}': ";
                    Exceptions.Add(new Exception(message, exception));
                    ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(null));
                    continue;
                }
                yield return handler;
            }
        }

        /// <summary>
        /// Получает описание ошибок при создании обработчиков документов.
        /// </summary>
        /// <param name="propertyName">Не используется.</param>
        /// <returns>Возвращает перечисление строк исключений.</returns>
        public IEnumerable GetErrors(string? propertyName = null)
        {
            foreach (var exception in Exceptions)
            {
                yield return exception.ToString() + exception.InnerException?.ToString();
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}