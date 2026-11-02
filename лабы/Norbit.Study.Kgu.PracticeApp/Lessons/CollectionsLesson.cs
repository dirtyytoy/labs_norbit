using Norbit.Study.Kgu.PracticeApp.Entities;
using Norbit.Study.Kgu.PracticeLib;

namespace Norbit.Study.Kgu.PracticeApp.Lessons;

/// <summary>
/// Задания темы "Коллекции, сравнение объектов".
/// </summary>
internal static class CollectionsLesson
{
	/// <summary>
	/// Текст для подсчёта частоты слов.
	/// </summary>
	private const string EnglishText =
		"It is a truth universally acknowledged that a single man in possession " +
		"of a good fortune must be in want of a wife. However little known the feelings " +
		"of such a man may be. This truth is so well fixed in the minds of the families.";

	/// <summary>
	/// Сколько квадратов запрашивается у итератора.
	/// </summary>
	private const int SquareCount = 4;

	/// <summary>
	/// Слово, на котором останавливается перебор в примере с yield break.
	/// </summary>
	private const string StopWord = "стоп";

	/// <summary>
	/// Разделители слов: по заданию только пробел и точка.
	/// </summary>
	private static readonly char[] WordSeparators = { ' ', '.' };

	/// <summary>
	/// Считает, как часто слова встречаются в тексте, и печатает их от частых к редким.
	/// </summary>
	public static void ShowWordFrequency()
	{
		Console.WriteLine(EnglishText);
		Console.WriteLine();

		var counts = CountWords(EnglishText);
		var sortedCounts = counts
			.OrderByDescending(pair => pair.Value)
			.ThenBy(pair => pair.Key)
			.ToArray();

		foreach (var pair in sortedCounts)
		{
			Console.WriteLine($"{pair.Key,-14}{pair.Value}");
		}
	}

	/// <summary>
	/// Считает количество повторений каждого слова без учёта регистра.
	/// </summary>
	/// <param name="text">Текст на английском.</param>
	/// <returns>Словарь "слово - сколько раз встретилось".</returns>
	/// <exception cref="ArgumentNullException">Вместо текста передан null.</exception>
	public static Dictionary<string, int> CountWords(string text)
	{
		ArgumentNullException.ThrowIfNull(text);

		var counts = new Dictionary<string, int>(StringComparer.OrdinalIgnoreCase);

		foreach (var word in text.Split(WordSeparators, StringSplitOptions.RemoveEmptyEntries))
		{
			counts.TryGetValue(word, out var count);
			counts[word] = count + 1;
		}

		return counts;
	}

	/// <summary>
	/// Проверяет на примерах все возможности динамического массива.
	/// </summary>
	public static void ShowDynamicArray()
	{
		var cities = new DynamicArray<string>(2);
		Describe("Создан с ёмкостью 2", cities);

		cities.Add("Кострома");
		cities.Add("Ярославль");
		cities.Add("Иваново");
		Describe("Три Add, ёмкость удвоилась", cities);

		cities.AddRange(new[] { "Владимир", "Суздаль", "Ростов", "Углич", "Рыбинск", "Плёс" });
		Describe("AddRange на шесть элементов", cities);

		cities.Insert(1, "Москва");
		Describe("Insert на место 1", cities);

		Console.WriteLine($"Remove(\"Москва\"): {cities.Remove("Москва")}");
		Console.WriteLine($"Remove(\"Париж\"): {cities.Remove("Париж")}");
		Describe("После удаления", cities);

		cities[0] = cities[0].ToUpper();
		Console.WriteLine($"Первый элемент после замены: {cities[0]}");

		var copy = new DynamicArray<string>(cities);
		Describe("Копия через конструктор от IEnumerable", copy);

		var empty = new DynamicArray<int>();
		Describe("Без параметров", empty);

		try
		{
			Console.WriteLine(cities[cities.Length]);
		}
		catch (ArgumentOutOfRangeException exception)
		{
			Console.WriteLine($"Обращение за границу: {exception.GetType().Name}, номер {exception.ActualValue}");
		}
	}

	/// <summary>
	/// Пробует в деле список, словарь, очередь и стек.
	/// </summary>
	public static void ShowStandardCollections()
	{
		var group = new List<string> { "Соколова", "Иванов", "Петрова" };
		group.Add("Абрамов");
		group.Sort();
		group.RemoveAt(group.Count - 1);
		Console.WriteLine($"List: {string.Join(", ", group)}; есть ли Иванов: {group.Contains("Иванов")}");

		var capitals = new Dictionary<string, string>
		{
			["Россия"] = "Москва",
			["Франция"] = "Париж",
			["Италия"] = "Рим"
		};

		capitals.Remove("Италия");

		var capitalDescription = capitals.TryGetValue("Франция", out var capital)
			? capital
			: "неизвестна";

		Console.WriteLine($"Dictionary: стран {capitals.Count}, столица Франции: {capitalDescription}, есть ли Италия: {capitals.ContainsKey("Италия")}");

		var visitors = new Queue<string>();
		visitors.Enqueue("первый");
		visitors.Enqueue("второй");
		visitors.Enqueue("третий");
		Console.WriteLine($"Queue: вышел {visitors.Dequeue()}, следующий {visitors.Peek()}, в очереди ещё {visitors.Count}");

		var pages = new Stack<string>();
		pages.Push("главная");
		pages.Push("каталог");
		pages.Push("товар");
		Console.WriteLine($"Stack: назад со страницы \"{pages.Pop()}\", теперь открыта \"{pages.Peek()}\"");
	}

	/// <summary>
	/// Сравнивает студента с другим студентом и с объектом постороннего класса.
	/// </summary>
	public static void ShowComparison()
	{
		var student = new Student("Смирнова Анна", "21-ИС-014");
		var sameStudent = new Student("Смирнова Анна", "21-ИС-014");
		var anotherStudent = new Student("Смирнова Анна", "21-ИС-027");
		var teacher = new Teacher("Смирнова Анна");

		Console.WriteLine($"Два объекта с одной зачёткой, Equals: {student.Equals(sameStudent)}");
		Console.WriteLine($"Они же через ==: {student == sameStudent}");
		Console.WriteLine($"Они же по ссылке: {ReferenceEquals(student, sameStudent)}");
		Console.WriteLine($"Тёзка с другой зачёткой: {student.Equals(anotherStudent)}");
		Console.WriteLine($"Преподаватель с тем же именем: {student.Equals(teacher)}");

		var marks = new Dictionary<Student, int>
		{
			[student] = 5
		};

		Console.WriteLine($"Поиск в словаре по равному объекту: {marks.ContainsKey(sameStudent)}");
		Console.WriteLine($"Поиск по другому студенту: {marks.ContainsKey(anotherStudent)}");
	}

	/// <summary>
	/// Показывает, что код итератора выполняется по частям, только когда просят следующий элемент.
	/// </summary>
	public static void ShowYield()
	{
		var squares = GetSquares(SquareCount);
		Console.WriteLine("Итератор получен, но ни один квадрат ещё не посчитан.");

		foreach (var square in squares)
		{
			Console.WriteLine($"  в цикл пришло {square}");
		}

		var words = new[] { "раз", "два", StopWord, "три" };
		var wordsBeforeStop = TakeBeforeStopWord(words);
		Console.WriteLine($"До слова \"{StopWord}\": {string.Join(", ", wordsBeforeStop)}");
	}

	/// <summary>
	/// Возвращает квадраты чисел от единицы. Каждый квадрат считается в момент запроса.
	/// </summary>
	/// <param name="count">Сколько квадратов выдать.</param>
	/// <returns>Последовательность квадратов.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Количество отрицательное.</exception>
	public static IEnumerable<int> GetSquares(int count)
	{
		if (count < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(count), count, "Количество не может быть отрицательным.");
		}

		return Iterate();

		IEnumerable<int> Iterate()
		{
			for (var number = 1; number <= count; number++)
			{
				Console.WriteLine($"  считается квадрат числа {number}");

				yield return number * number;
			}
		}
	}

	/// <summary>
	/// Возвращает слова по одному, пока не встретится стоп-слово.
	/// </summary>
	/// <param name="words">Исходные слова.</param>
	/// <returns>Слова, которые шли до стоп-слова.</returns>
	private static IEnumerable<string> TakeBeforeStopWord(IEnumerable<string> words)
	{
		foreach (var word in words)
		{
			if (word == StopWord)
			{
				yield break;
			}

			yield return word;
		}
	}

	/// <summary>
	/// Печатает подпись, длину, ёмкость и элементы динамического массива.
	/// </summary>
	/// <typeparam name="T">Тип элементов.</typeparam>
	/// <param name="caption">Подпись.</param>
	/// <param name="array">Массив.</param>
	private static void Describe<T>(string caption, DynamicArray<T> array)
	{
		Console.WriteLine($"{caption}: длина {array.Length}, ёмкость {array.Capacity}");

		foreach (var item in array)
		{
			Console.Write($"  {item}");
		}

		Console.WriteLine();
	}
}
