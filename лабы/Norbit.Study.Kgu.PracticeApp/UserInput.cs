using System.Globalization;

namespace Norbit.Study.Kgu.PracticeApp;

/// <summary>
/// Вопросы пользователю. Если ответ не подходит, вопрос задаётся заново.
/// </summary>
internal static class UserInput
{
	/// <summary>
	/// Спрашивает строку.
	/// </summary>
	/// <param name="question">Текст вопроса.</param>
	/// <returns>Ответ пользователя.</returns>
	/// <exception cref="ArgumentException">Вопрос пустой.</exception>
	/// <exception cref="EndOfStreamException">Ввод закончился, отвечать некому.</exception>
	public static string AskLine(string question)
	{
		if (string.IsNullOrEmpty(question))
		{
			throw new ArgumentException("Вопрос не может быть пустым.", nameof(question));
		}

		Console.Write(question);

		return Console.ReadLine() ?? throw new EndOfStreamException("Ввод закончился раньше, чем получен ответ.");
	}

	/// <summary>
	/// Спрашивает целое число из диапазона.
	/// </summary>
	/// <param name="question">Текст вопроса.</param>
	/// <param name="min">Наименьший допустимый ответ.</param>
	/// <param name="max">Наибольший допустимый ответ.</param>
	/// <returns>Число от <paramref name="min"/> до <paramref name="max"/> включительно.</returns>
	/// <exception cref="ArgumentException">Границы перепутаны местами.</exception>
	public static int AskInteger(string question, int min, int max)
	{
		if (min > max)
		{
			throw new ArgumentException($"Нижняя граница {min} больше верхней {max}.", nameof(min));
		}

		while (true)
		{
			var answer = AskLine(question);

			if (int.TryParse(answer, out var value) && (value >= min) && (value <= max))
			{
				return value;
			}

			Console.WriteLine($"Введите целое число от {min} до {max}.");
		}
	}

	/// <summary>
	/// Спрашивает положительное число, можно дробное. Понимает и запятую, и точку.
	/// </summary>
	/// <param name="question">Текст вопроса.</param>
	/// <returns>Число больше нуля.</returns>
	public static double AskPositiveNumber(string question)
	{
		while (true)
		{
			var answer = AskLine(question);
			var isParsed = double.TryParse(answer, NumberStyles.Float, CultureInfo.CurrentCulture, out var value)
				|| double.TryParse(answer, NumberStyles.Float, CultureInfo.InvariantCulture, out value);

			if (isParsed && (value > 0))
			{
				return value;
			}

			Console.WriteLine("Нужно положительное число.");
		}
	}
}
