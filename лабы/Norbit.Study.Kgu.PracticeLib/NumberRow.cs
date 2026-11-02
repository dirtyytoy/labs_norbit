namespace Norbit.Study.Kgu.PracticeLib;

/// <summary>
/// Построение ряда натуральных чисел в виде текста.
/// </summary>
public static class NumberRow
{
	/// <summary>
	/// Чем отделяются числа друг от друга.
	/// </summary>
	private const string Delimiter = ", ";

	/// <summary>
	/// Собирает строку из чисел от единицы до заданного числа, например "1, 2, 3".
	/// </summary>
	/// <param name="lastNumber">Число, которым ряд заканчивается. Должно быть больше нуля.</param>
	/// <returns>Готовая строка.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Число меньше единицы.</exception>
	public static string CreateRow(int lastNumber)
	{
		if (lastNumber < 1)
		{
			throw new ArgumentOutOfRangeException(nameof(lastNumber), lastNumber, "Ряд можно построить только до положительного числа.");
		}

		var numbers = new int[lastNumber];

		for (var i = 0; i < lastNumber; i++)
		{
			numbers[i] = i + 1;
		}

		return string.Join(Delimiter, numbers);
	}
}
