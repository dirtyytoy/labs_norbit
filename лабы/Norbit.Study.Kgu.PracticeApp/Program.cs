using Norbit.Study.Kgu.PracticeApp.Lessons;

namespace Norbit.Study.Kgu.PracticeApp;

/// <summary>
/// Консольное приложение с практическими заданиями курса.
/// </summary>
internal static class Program
{
	/// <summary>
	/// Номер, по которому приложение завершается.
	/// </summary>
	private const int ExitChoice = 0;

	/// <summary>
	/// Все задания по порядку тем. Номер в меню на единицу больше индекса.
	/// </summary>
	private static readonly MenuEntry[] Entries =
	{
		new MenuEntry("Среда разработки: числа от 1 до N", EnvironmentLesson.ShowNumberRow),
		new MenuEntry("Среда разработки: квадрат из звёздочек", EnvironmentLesson.ShowStarSquare),
		new MenuEntry("Типы данных: индекс массы тела", DataTypesLesson.ShowBodyMassIndex),
		new MenuEntry("Типы данных: случайный массив", DataTypesLesson.ShowRandomArray),
		new MenuEntry("Типы данных: средняя длина слова", DataTypesLesson.ShowAverageWordLength),
		new MenuEntry("Типы данных: структура и класс, ref и out", DataTypesLesson.ShowValueAndReferenceTypes),
		new MenuEntry("Типы данных: сборка мусора", DataTypesLesson.ShowGarbageCollection),
		new MenuEntry("Коллекции: частота слов", CollectionsLesson.ShowWordFrequency),
		new MenuEntry("Коллекции: динамический массив", CollectionsLesson.ShowDynamicArray),
		new MenuEntry("Коллекции: список, словарь, очередь, стек", CollectionsLesson.ShowStandardCollections),
		new MenuEntry("Коллекции: сравнение объектов", CollectionsLesson.ShowComparison),
		new MenuEntry("Коллекции: yield", CollectionsLesson.ShowYield)
	};

	/// <summary>
	/// Крутит меню, пока не выбран выход.
	/// </summary>
	private static void Main()
	{
		while (true)
		{
			ShowMenu();
			var choice = UserInput.AskInteger("Ваш выбор: ", ExitChoice, Entries.Length);

			if (choice == ExitChoice)
			{
				break;
			}

			var entry = Entries[choice - 1];

			Console.WriteLine();
			Console.WriteLine($"=== {entry.Title} ===");
			entry.Run();
		}
	}

	/// <summary>
	/// Печатает пронумерованный список заданий.
	/// </summary>
	private static void ShowMenu()
	{
		Console.WriteLine();

		for (var i = 0; i < Entries.Length; i++)
		{
			Console.WriteLine($"{i + 1,2} - {Entries[i].Title}");
		}

		Console.WriteLine($"{ExitChoice,2} - Выход");
	}
}
