namespace Norbit.Study.Kgu.PracticeApp.Entities;

/// <summary>
/// Преподаватель. Класс никак не связан со студентом и сравнение не переопределяет.
/// </summary>
internal class Teacher
{
	/// <summary>
	/// Создаёт преподавателя.
	/// </summary>
	/// <param name="fullName">Фамилия и имя.</param>
	/// <exception cref="ArgumentException">Имя пустое.</exception>
	public Teacher(string fullName)
	{
		if (string.IsNullOrEmpty(fullName))
		{
			throw new ArgumentException("Не указано имя преподавателя.", nameof(fullName));
		}

		FullName = fullName;
	}

	/// <summary>
	/// Фамилия и имя.
	/// </summary>
	public string FullName { get; }
}
