using Norbit.Study.Kgu.PracticeApp.Entities;

namespace Norbit.Study.Kgu.PracticeApp.Lessons;

/// <summary>
/// Задания темы "Типы данных в .NET".
/// </summary>
internal static class DataTypesLesson
{
	/// <summary>
	/// Нижняя граница нормального индекса массы тела.
	/// </summary>
	private const double NormalIndexFrom = 18.5;

	/// <summary>
	/// Верхняя граница нормального индекса массы тела.
	/// </summary>
	private const double NormalIndexTo = 25.0;

	/// <summary>
	/// Размер случайного массива.
	/// </summary>
	private const int RandomArraySize = 12;

	/// <summary>
	/// Случайные числа берутся от нуля до этого значения.
	/// </summary>
	private const double RandomValueLimit = 100;

	/// <summary>
	/// Знаков после запятой у случайных чисел.
	/// </summary>
	private const int RandomValueDigits = 1;

	/// <summary>
	/// Разделитель ширины и высоты в записи габаритов.
	/// </summary>
	private const char DimensionsSeparator = 'x';

	/// <summary>
	/// Из скольких чисел состоит запись габаритов.
	/// </summary>
	private const int DimensionsPartCount = 2;

	/// <summary>
	/// Во сколько раз увеличиваются габариты в примерах.
	/// </summary>
	private const int EnlargeFactor = 2;

	/// <summary>
	/// Размер временного массива для опыта со сборщиком мусора.
	/// </summary>
	private const int GarbageSize = 1_000_000;

	/// <summary>
	/// Спрашивает вес и рост, печатает индекс массы тела.
	/// </summary>
	public static void ShowBodyMassIndex()
	{
		var weight = UserInput.AskPositiveNumber("Вес в килограммах: ");
		var height = UserInput.AskPositiveNumber("Рост в метрах, например 1,68: ");
		var index = CalculateBodyMassIndex(weight, height);

		Console.WriteLine($"Индекс массы тела: {index:F2} ({DescribeBodyMassIndex(index)})");
	}

	/// <summary>
	/// Вычисляет индекс массы тела по формуле "вес / рост в квадрате".
	/// </summary>
	/// <param name="weight">Вес в килограммах.</param>
	/// <param name="height">Рост в метрах.</param>
	/// <returns>Индекс массы тела.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Вес или рост меньше либо равны нулю.</exception>
	public static double CalculateBodyMassIndex(double weight, double height)
	{
		if (weight <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(weight), weight, "Вес должен быть больше нуля.");
		}

		if (height <= 0)
		{
			throw new ArgumentOutOfRangeException(nameof(height), height, "Рост должен быть больше нуля.");
		}

		return weight / (height * height);
	}

	/// <summary>
	/// Создаёт массив случайных чисел, находит в нём наименьшее и наибольшее, сортирует и печатает.
	/// </summary>
	public static void ShowRandomArray()
	{
		var values = CreateRandomValues(RandomArraySize);
		Console.WriteLine($"Сгенерировано: {string.Join("  ", values)}");

		FindMinAndMax(values, out var min, out var max);
		Console.WriteLine($"Наименьшее: {min}, наибольшее: {max}");

		SortBySelection(values);
		Console.WriteLine($"По возрастанию: {string.Join("  ", values)}");
	}

	/// <summary>
	/// Спрашивает строку и печатает среднюю длину слова в ней.
	/// </summary>
	public static void ShowAverageWordLength()
	{
		var text = UserInput.AskLine("Строка: ");
		var average = GetAverageWordLength(text);

		Console.WriteLine($"Средняя длина слова: {average:0.##}");
	}

	/// <summary>
	/// Находит среднюю длину слова. Словом считается всё, что между пробелами,
	/// а знаки препинания и прочие значки из длины вычитаются.
	/// </summary>
	/// <param name="text">Строка со словами.</param>
	/// <returns>Средняя длина. Ноль, если слов не нашлось.</returns>
	public static double GetAverageWordLength(string text)
	{
		if (string.IsNullOrEmpty(text))
		{
			return 0;
		}

		var wordCount = 0;
		var totalLength = 0;

		foreach (var part in text.Split())
		{
			var length = CountLetters(part);

			if (length > 0)
			{
				wordCount++;
				totalLength += length;
			}
		}

		return wordCount > 0
			? (double)totalLength / wordCount
			: 0;
	}

	/// <summary>
	/// Показывает, чем отличается передача структуры и объекта класса в метод, и работу ref с out.
	/// </summary>
	public static void ShowValueAndReferenceTypes()
	{
		var size = new Dimensions(30, 20);
		var parcel = new Parcel("Кострома, ул. Советская, 1", 2.5)
		{
			Size = size
		};

		Console.WriteLine($"Было: габариты {size}, посылка: {parcel}");

		Enlarge(size);
		Console.WriteLine($"Структура без ref: {size} - метод увеличил свою копию");

		Enlarge(ref size);
		Console.WriteLine($"Структура с ref: {size} - метод увеличил оригинал");

		Console.WriteLine($"Габариты внутри посылки: {parcel.Size} - туда при присваивании тоже попала копия");

		MarkAsFragile(parcel);
		Console.WriteLine($"Объект класса: {parcel} - метод работал с тем же объектом");

		var text = "45x60";

		if (TryParseDimensions(text, out var parsedSize))
		{
			Console.WriteLine($"Через out из строки \"{text}\": {parsedSize}, площадь {parsedSize.Area}");
		}

		object boxedSize = size;
		size.Width = 0;
		var unboxedSize = (Dimensions)boxedSize;
		Console.WriteLine($"После упаковки: в переменной {size}, в упакованной копии {unboxedSize}");
	}

	/// <summary>
	/// Запускает сборку мусора вручную и смотрит, что стало с объектами.
	/// </summary>
	public static void ShowGarbageCollection()
	{
		var parcel = new Parcel("Кострома, ул. Ленина, 10", 1);
		var garbage = CreateGarbage();

		Console.WriteLine($"До сборки: временный массив жив - {garbage.IsAlive}, посылка в поколении {GC.GetGeneration(parcel)}");
		Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");

		GC.Collect();
		GC.WaitForPendingFinalizers();

		Console.WriteLine($"После сборки: временный массив жив - {garbage.IsAlive}, посылка в поколении {GC.GetGeneration(parcel)}");
		Console.WriteLine($"Занято памяти: {GC.GetTotalMemory(false)} байт");
		Console.WriteLine($"Сборок по поколениям: {GC.CollectionCount(0)}, {GC.CollectionCount(1)}, {GC.CollectionCount(2)}");
	}

	/// <summary>
	/// Словесная оценка индекса массы тела.
	/// </summary>
	/// <param name="index">Индекс массы тела.</param>
	/// <returns>Оценка относительно нормы.</returns>
	private static string DescribeBodyMassIndex(double index)
	{
		if (index < NormalIndexFrom)
		{
			return "ниже нормы";
		}

		if (index >= NormalIndexTo)
		{
			return "выше нормы";
		}

		return "в пределах нормы";
	}

	/// <summary>
	/// Создаёт массив случайных дробных чисел.
	/// </summary>
	/// <param name="size">Сколько чисел нужно.</param>
	/// <returns>Заполненный массив.</returns>
	private static double[] CreateRandomValues(int size)
	{
		var random = new Random();
		var values = new double[size];

		for (var i = 0; i < size; i++)
		{
			values[i] = Math.Round(random.NextDouble() * RandomValueLimit, RandomValueDigits);
		}

		return values;
	}

	/// <summary>
	/// За один проход находит наименьшее и наибольшее числа.
	/// </summary>
	/// <param name="values">Массив, в котором есть хотя бы один элемент.</param>
	/// <param name="min">Наименьшее число.</param>
	/// <param name="max">Наибольшее число.</param>
	private static void FindMinAndMax(double[] values, out double min, out double max)
	{
		min = values[0];
		max = values[0];

		foreach (var value in values)
		{
			if (value < min)
			{
				min = value;
			}

			if (value > max)
			{
				max = value;
			}
		}
	}

	/// <summary>
	/// Сортировка выбором: на каждое место по очереди ставится наименьшее из оставшихся чисел.
	/// </summary>
	/// <param name="values">Массив, который нужно упорядочить.</param>
	private static void SortBySelection(double[] values)
	{
		for (var i = 0; i < values.Length - 1; i++)
		{
			var minIndex = i;

			for (var j = i + 1; j < values.Length; j++)
			{
				if (values[j] < values[minIndex])
				{
					minIndex = j;
				}
			}

			if (minIndex != i)
			{
				(values[i], values[minIndex]) = (values[minIndex], values[i]);
			}
		}
	}

	/// <summary>
	/// Считает в слове буквы и цифры, пропуская пунктуацию.
	/// </summary>
	/// <param name="word">Кусок строки между пробелами.</param>
	/// <returns>Длина слова без знаков.</returns>
	private static int CountLetters(string word)
	{
		var count = 0;

		foreach (var symbol in word)
		{
			if (!char.IsPunctuation(symbol) && !char.IsSymbol(symbol))
			{
				count++;
			}
		}

		return count;
	}

	/// <summary>
	/// Увеличивает габариты. Структура пришла по значению, снаружи ничего не изменится.
	/// </summary>
	/// <param name="size">Копия габаритов.</param>
	private static void Enlarge(Dimensions size)
	{
		size.Width *= EnlargeFactor;
		size.Height *= EnlargeFactor;
	}

	/// <summary>
	/// Увеличивает габариты в переменной вызывающего кода.
	/// </summary>
	/// <param name="size">Габариты по ссылке.</param>
	private static void Enlarge(ref Dimensions size)
	{
		size.Width *= EnlargeFactor;
		size.Height *= EnlargeFactor;
	}

	/// <summary>
	/// Помечает посылку хрупкой.
	/// </summary>
	/// <param name="parcel">Посылка.</param>
	private static void MarkAsFragile(Parcel parcel)
	{
		parcel.IsFragile = true;
	}

	/// <summary>
	/// Разбирает запись вида "45x60".
	/// </summary>
	/// <param name="text">Строка с шириной и высотой.</param>
	/// <param name="size">Габариты, если строку удалось разобрать.</param>
	/// <returns>True, если разбор удался.</returns>
	private static bool TryParseDimensions(string text, out Dimensions size)
	{
		size = default;
		var parts = text.Split(DimensionsSeparator);

		if (parts.Length != DimensionsPartCount)
		{
			return false;
		}

		if (!int.TryParse(parts[0], out var width) || !int.TryParse(parts[1], out var height))
		{
			return false;
		}

		size = new Dimensions(width, height);

		return true;
	}

	/// <summary>
	/// Создаёт большой массив и сразу про него забывает, оставляя только слабую ссылку.
	/// </summary>
	/// <returns>Слабая ссылка, по которой видно, собран массив или нет.</returns>
	private static WeakReference CreateGarbage()
	{
		var garbage = new byte[GarbageSize];

		return new WeakReference(garbage);
	}
}
