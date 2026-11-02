namespace Norbit.Study.Kgu.PracticeApp;

/// <summary>
/// Строка меню: название задания и метод, который его запускает.
/// </summary>
internal class MenuEntry
{
	// Метод с кодом задания.
	private readonly Action _handler;

	/// <summary>
	/// Создаёт строку меню.
	/// </summary>
	/// <param name="title">Название, которое увидит пользователь.</param>
	/// <param name="handler">Метод задания.</param>
	/// <exception cref="ArgumentException">Название пустое.</exception>
	/// <exception cref="ArgumentNullException">Метод не передан.</exception>
	public MenuEntry(string title, Action handler)
	{
		if (string.IsNullOrEmpty(title))
		{
			throw new ArgumentException("У строки меню должно быть название.", nameof(title));
		}

		ArgumentNullException.ThrowIfNull(handler);

		Title = title;
		_handler = handler;
	}

	/// <summary>
	/// Название задания.
	/// </summary>
	public string Title { get; }

	/// <summary>
	/// Запускает задание.
	/// </summary>
	public void Run()
	{
		_handler();
	}
}
