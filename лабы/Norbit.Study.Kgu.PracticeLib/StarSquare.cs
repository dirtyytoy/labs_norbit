namespace Norbit.Study.Kgu.PracticeLib;

/// <summary>
/// Квадрат из звёздочек с пустой серединой.
/// </summary>
public static class StarSquare
{
	/// <summary>
	/// Символ заполнения.
	/// </summary>
	private const char Star = '*';

	/// <summary>
	/// Символ вместо центральной звёздочки.
	/// </summary>
	private const char Hole = ' ';

	/// <summary>
	/// Проверяет, можно ли нарисовать квадрат с такой стороной.
	/// </summary>
	/// <param name="side">Длина стороны.</param>
	/// <returns>True для положительных нечётных чисел.</returns>
	public static bool CanDraw(int side)
	{
		return (side > 0) && ((side % 2) == 1);
	}

	/// <summary>
	/// Рисует квадрат в консоли.
	/// </summary>
	/// <param name="side">Длина стороны, положительное нечётное число.</param>
	/// <exception cref="ArgumentException">Сторона чётная или не положительная.</exception>
	public static void Draw(int side)
	{
		if (!CanDraw(side))
		{
			throw new ArgumentException($"Сторона должна быть положительной и нечётной, а получено {side}.", nameof(side));
		}

		var half = side / 2;
		var fullLine = new string(Star, side);
		var middleLine = new string(Star, half) + Hole + new string(Star, half);

		for (var row = 0; row < side; row++)
		{
			var line = row == half
				? middleLine
				: fullLine;

			Console.WriteLine(line);
		}
	}
}
