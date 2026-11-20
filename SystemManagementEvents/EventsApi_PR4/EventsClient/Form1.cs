using EventsClient.Models;
using EventsClient.Services;
using System.Net.Http;
using System.Diagnostics;

namespace EventsClient
{
    public partial class MainForm : Form
    {
        private CancellationTokenSource? _loadCts;
        private readonly EventApiClient _apiClient = new();
        private List<EventListItemDTO> _loadedEvents = new();

        private int _currentPage = 1;
        private const int PageSize = 50;
        private int _totalPages = 1;
        private int _totalEvents;
        public MainForm()
        {
            InitializeComponent();

            SetupHelp();

            progressBar.Visible = false;
            cancelButton.Enabled = false;
        }

        private async void loadButton_Click(object sender, EventArgs e)
        {
            var cts = new CancellationTokenSource();
            _loadCts = cts;

            loadButton.Enabled = false;
            addButton.Enabled = false;
            cancelButton.Enabled = true;

            progressBar.Style = ProgressBarStyle.Marquee;
            progressBar.Visible = true;
            statusLabel.Text = "Загрузка мероприятий...";

            try
            {
                var totalTimer = Stopwatch.StartNew();

                var requestTimer = Stopwatch.StartNew();

                _currentPage = 1;
                var result = await _apiClient.GetEventsAsync(
                    _currentPage, PageSize, cts.Token);

                requestTimer.Stop();

                var bindingTimer = Stopwatch.StartNew();

                DisplayPage(result);

                bindingTimer.Stop();
                totalTimer.Stop();

                int first = (_currentPage - 1) * PageSize + 1;
                int last = first + _loadedEvents.Count - 1;

                statusLabel.Text =
                    $"Показано: {first}–{last} из {result.Total} | " +
                    $"API: {requestTimer.Elapsed.TotalMilliseconds:F2} мс | " +
                    $"Таблица: {bindingTimer.Elapsed.TotalMilliseconds:F2} мс | " +
                    $"Всего: {totalTimer.Elapsed.TotalMilliseconds:F2} мс";
            }
            catch (OperationCanceledException) when (cts.IsCancellationRequested)
            {
                statusLabel.Text = "Загрузка отменена пользователем";
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Сервер не ответил за отведённое время.",
                    "Тайм-аут",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                statusLabel.Text = "Превышено время ожидания";
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Нет связи с сервером. Проверьте, запущен ли сервис.",
                    "Ошибка подключения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                statusLabel.Text = "Ошибка подключения к серверу";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось загрузить мероприятия: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                statusLabel.Text = "Ошибка загрузки";
            }
            finally
            {
                if (ReferenceEquals(_loadCts, cts))
                    _loadCts = null;

                cts.Dispose();

                progressBar.Visible = false;
                loadButton.Enabled = true;
                addButton.Enabled = true;
                cancelButton.Enabled = false;
            }
        }

        private async void addButton_Click(object sender, EventArgs e)
        {
            string title = titleTextBox.Text.Trim();
            string room = roomTextBox.Text.Trim();
            int maxParticipants = (int)maxParticipantsNumeric.Value;
            errorProvider.Clear();

            // Локальная проверка
            if (title.Length < 5 || title.Length > 150)
            {
                errorProvider.SetError(
                    titleTextBox,
                    "Название должно содержать от 5 до 150 символов.");

                statusLabel.Text = "Ошибка проверки данных";
                return;
            }

            if (string.IsNullOrWhiteSpace(room))
            {
                errorProvider.SetError(
                    roomTextBox,
                    "Укажите аудиторию.");

                statusLabel.Text = "Ошибка проверки данных";
                return;
            }

            var request = new CreateEventRequestDTO
            {
                Title = title,
                Date = datePicker.Value,
                Room = room,
                MaxParticipants = maxParticipants
            };

            addButton.Enabled = false;
            loadButton.Enabled = false;
            statusLabel.Text = "Создание мероприятия...";

            try
            {
                using var response = await _apiClient.CreateEventAsync(request);

                if (response.StatusCode == System.Net.HttpStatusCode.Created)
                {
                    MessageBox.Show(
                        "Мероприятие успешно создано!",
                        "Успех",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);

                    titleTextBox.Clear();
                    roomTextBox.Clear();

                    statusLabel.Text = "Мероприятие создано";

                    // Обновляем первую страницу
                    _currentPage = 1;
                    await LoadPageAsync();
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    string error = await response.Content.ReadAsStringAsync();

                    ShowServerValidationErrors(error);

                    MessageBox.Show(
                        "Сервер обнаружил ошибки в заполнении данных. " +
                        "Исправьте поля, отмеченные значками.",
                        "Некорректные данные",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    statusLabel.Text = "Ошибка валидации";
                }
                else if (response.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    string error = await response.Content.ReadAsStringAsync();

                    MessageBox.Show(
                        error,
                        "Аудитория занята",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);

                    statusLabel.Text = "Конфликт при создании";
                }
                else
                {
                    MessageBox.Show(
                        $"Ошибка сервера: {(int)response.StatusCode}",
                        "Ошибка",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);

                    statusLabel.Text = "Не удалось создать мероприятие";
                }
            }
            catch (HttpRequestException)
            {
                MessageBox.Show(
                    "Нет связи с сервером. Проверьте, запущен ли сервис.",
                    "Ошибка подключения",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                statusLabel.Text = "Ошибка подключения";
            }
            catch (TaskCanceledException)
            {
                MessageBox.Show(
                    "Превышено время ожидания ответа сервера.",
                    "Тайм-аут",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);

                statusLabel.Text = "Превышено время ожидания";
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    $"Не удалось создать мероприятие: {ex.Message}",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);

                statusLabel.Text = "Ошибка создания";
            }
            finally
            {
                addButton.Enabled = true;
                loadButton.Enabled = true;
            }
        }

        private void cancelButton_Click(object sender, EventArgs e)
        {
            _loadCts?.Cancel();
        }

        private void syncLoadButton_Click(object sender, EventArgs e)
        {
            statusLabel.Text = "Блокировка интерфейса на 8 секунд...";

            System.Threading.Thread.Sleep(8000);

            statusLabel.Text = "Задержка завершена";

            MessageBox.Show("Интерфейс снова доступен.");
        }

        private void searchButton_Click(object sender, EventArgs e)
        {
            string searchQuery = searchTextBox.Text.Trim();

            var filteredEvents = _loadedEvents
                .Where(item =>
                    item.Title.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                    item.Room.Contains(searchQuery, StringComparison.OrdinalIgnoreCase))
                .ToList();

            eventsGrid.DataSource = null;
            eventsGrid.DataSource = filteredEvents;

            statusLabel.Text = $"Найдено мероприятий: {filteredEvents.Count}";
        }

        private void resetSearchButton_Click(object sender, EventArgs e)
        {
            searchTextBox.Clear();

            eventsGrid.DataSource = null;
            eventsGrid.DataSource = _loadedEvents.ToList();

            int first = (_currentPage - 1) * PageSize + 1;
            int last = first + _loadedEvents.Count - 1;

            statusLabel.Text = $"Показано: {first}–{last} из {_totalEvents}";
        }
        private void SetupHelp()
        {
            // Подсказки для кнопок
            toolTip.SetToolTip(loadButton,
                "Загрузить актуальный список мероприятий с сервера.");

            toolTip.SetToolTip(addButton,
                "Создать новое мероприятие на основе заполненных данных.");

            toolTip.SetToolTip(cancelButton,
                "Прервать текущую загрузку мероприятий.");

            toolTip.SetToolTip(searchButton,
                "Найти мероприятия по названию или аудитории.");

            toolTip.SetToolTip(resetSearchButton,
                "Сбросить поиск и показать все мероприятия.");

            toolTip.SetToolTip(syncLoadButton,
                "Проверить поведение интерфейса при блокировке потока.");

            // Подсказки для полей
            toolTip.SetToolTip(titleTextBox,
                "Введите название мероприятия.");

            toolTip.SetToolTip(datePicker,
                "Выберите дату проведения мероприятия.");

            toolTip.SetToolTip(roomTextBox,
                "Укажите аудиторию, в которой будет проходить мероприятие.");

            toolTip.SetToolTip(maxParticipantsNumeric,
                "Укажите максимальное количество участников от 1 до 500.");

            toolTip.SetToolTip(searchTextBox,
                "Введите название мероприятия или аудиторию для поиска.");

            toolTip.SetToolTip(eventsGrid,
                "Таблица содержит список мероприятий, полученных с сервера.");

            // Подсказки-примеры
            titleTextBox.PlaceholderText = "Например: День открытых дверей";
            roomTextBox.PlaceholderText = "Например: Аудитория 214";
            searchTextBox.PlaceholderText = "Введите название или аудиторию";

            // Справка по F1
            helpProvider.SetShowHelp(titleTextBox, true);
            helpProvider.SetHelpString(titleTextBox,
                "Введите название мероприятия длиной от 5 до 150 символов. " +
                "Например: День открытых дверей.");

            helpProvider.SetShowHelp(datePicker, true);
            helpProvider.SetHelpString(datePicker,
                "Выберите дату проведения мероприятия. " +
                "Дата используется при планировании и проверке занятости аудитории.");

            helpProvider.SetShowHelp(roomTextBox, true);
            helpProvider.SetHelpString(roomTextBox,
                "Укажите название аудитории. " +
                "Например: Аудитория 214. В одной аудитории нельзя проводить два мероприятия в одну дату.");

            helpProvider.SetShowHelp(maxParticipantsNumeric, true);
            helpProvider.SetHelpString(maxParticipantsNumeric,
                "Укажите максимально допустимое количество участников. " +
                "Допустимый диапазон значений — от 1 до 500.");

            helpProvider.SetShowHelp(eventsGrid, true);
            helpProvider.SetHelpString(eventsGrid,
                "Здесь отображаются мероприятия, полученные с сервера. " +
                "Для обновления списка нажмите кнопку «Загрузить».");
        }
        private void ShowServerValidationErrors(string json)
        {
            using var document = System.Text.Json.JsonDocument.Parse(json);

            if (!document.RootElement.TryGetProperty(
                "errors", out var errors))
                return;

            foreach (var property in errors.EnumerateObject())
            {
                string message = string.Join(
                    "\n",
                    property.Value.EnumerateArray()
                        .Select(x => x.GetString())
                        .Where(x => !string.IsNullOrWhiteSpace(x))!);

                switch (property.Name.ToLowerInvariant())
                {
                    case "title":
                        errorProvider.SetError(titleTextBox, message);
                        break;

                    case "room":
                        errorProvider.SetError(roomTextBox, message);
                        break;

                    case "date":
                        errorProvider.SetError(datePicker, message);
                        break;

                    case "maxparticipants":
                        errorProvider.SetError(maxParticipantsNumeric, message);
                        break;
                }
            }
        }
        private void aboutMenuItem_Click(object sender, EventArgs e)
        {
            using var aboutForm = new AboutForm();
            aboutForm.ShowDialog(this);
        }

        private void manualMenuItem_Click(object sender, EventArgs e)
        {
            string manualPath = Path.Combine(
                AppContext.BaseDirectory,
                "UserManual.html");

            if (File.Exists(manualPath))
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = manualPath,
                    UseShellExecute = true
                });
            }
            else
            {
                MessageBox.Show(
                    "Файл руководства пользователя не найден.",
                    "Ошибка",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }
        private async Task LoadPageAsync(CancellationToken cancellationToken = default)
        {
            var result = await _apiClient.GetEventsAsync(
                _currentPage, PageSize, cancellationToken);

            DisplayPage(result);
        }
        private void DisplayPage(PagedResult<EventListItemDTO> result)
        {
            _loadedEvents = result.Items;
            _totalEvents = result.Total;

            _totalPages = Math.Max(
                1,
                (int)Math.Ceiling((double)result.Total / result.PageSize));
            eventsGrid.DataSource = null;
            eventsGrid.DataSource = _loadedEvents.ToList();

            eventsGrid.Columns[nameof(EventListItemDTO.Id)].HeaderText = "ID";
            eventsGrid.Columns[nameof(EventListItemDTO.Title)].HeaderText = "Название";
            eventsGrid.Columns[nameof(EventListItemDTO.Date)].HeaderText = "Дата";
            eventsGrid.Columns[nameof(EventListItemDTO.Room)].HeaderText = "Аудитория";
            eventsGrid.Columns[nameof(EventListItemDTO.MaxParticipants)].HeaderText =
                "Максимум участников";
            eventsGrid.Columns[nameof(EventListItemDTO.Status)].HeaderText = "Статус";

            pageLabel.Text = $"Страница {_currentPage} из {_totalPages}";

            previousPageButton.Enabled = _currentPage > 1;
            nextPageButton.Enabled = _currentPage < _totalPages;

            int first = (_currentPage - 1) * result.PageSize + 1;
            int last = first + _loadedEvents.Count - 1;

            statusLabel.Text = $"Показано: {first}–{last} из {result.Total}";
        }
        private async void previousPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage > 1)
            {
                _currentPage--;
                await LoadPageAsync();
            }
        }

        private async void nextPageButton_Click(object sender, EventArgs e)
        {
            if (_currentPage < _totalPages)
            {
                _currentPage++;
                await LoadPageAsync();
            }
        }
    }
}
