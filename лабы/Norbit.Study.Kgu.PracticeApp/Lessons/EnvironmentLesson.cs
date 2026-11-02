using Norbit.Study.Kgu.PracticeLib;

namespace Norbit.Study.Kgu.PracticeApp.Lessons;

/// <summary>
/// Задания темы "Использование среды разработки".
/// </summary>
internal static class EnvironmentLesson
{
	/// <summary>
	/// Наибольшее N, которое разрешено ввести, чтобы вывод помещался на экране.
	/// </summary>
	private const int MaxNumber = 1000;

	/// <summary>
	/// Наибольшая сторона квадрата, шире консоль обычно не бывает.
	/// </summary>
	private const int MaxSide = 79;

	/// <summary>
	/// Спрашивает N и печатает числа от 1 до N через запятую.
	/// </summary>
	public static void ShowNumberRow()
	{
		var lastNumber = UserInput.AskInteger("N = ", 1, MaxNumber);
		var row = NumberRow.CreateRow(lastNumber);

		Console.WriteLine(row);
	}

	/// <summary>
	/// Спрашивает сторону и рисует квадрат без центральной звёздочки.
	/// </summary>
	public static void ShowStarSquare()
	{
		var side = UserInput.AskInteger("Сторона квадрата, нечётное число: ", 1, MaxSide);

		while (!StarSquare.CanDraw(side))
		{
			Console.WriteLine("У чётного квадрата нет середины, нужно нечётное число.");
			side = UserInput.AskInteger("Сторона квадрата, нечётное число: ", 1, MaxSide);
		}

		StarSquare.Draw(side);
	}
}
