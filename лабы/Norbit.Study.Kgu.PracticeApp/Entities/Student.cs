namespace Norbit.Study.Kgu.PracticeApp.Entities;

/// <summary>
/// Студент. Одним и тем же студентом считаются объекты с одинаковым номером зачётки.
/// </summary>
internal class Student : IEquatable<Student>
{
	/// <summary>
	/// Создаёт студента.
	/// </summary>
	/// <param name="fullName">Фамилия и имя.</param>
	/// <param name="recordBookNumber">Номер зачётной книжки.</param>
	/// <exception cref="ArgumentException">Имя или номер зачётки пустые.</exception>
	public Student(string fullName, string recordBookNumber)
	{
		if (string.IsNullOrEmpty(fullName))
		{
			throw new ArgumentException("Не указано имя студента.", nameof(fullName));
		}

		if (string.IsNullOrEmpty(recordBookNumber))
		{
			throw new ArgumentException("Не указан номер зачётки.", nameof(recordBookNumber));
		}

		FullName = fullName;
		RecordBookNumber = recordBookNumber;
	}

	/// <summary>
	/// Фамилия и имя.
	/// </summary>
	public string FullName { get; }

	/// <summary>
	/// Номер зачётной книжки. Менять его нельзя, потому что от него зависит хеш.
	/// </summary>
	public string RecordBookNumber { get; }

	/// <summary>
	/// Сравнивает двух студентов оператором ==.
	/// </summary>
	/// <param name="left">Первый студент.</param>
	/// <param name="right">Второй студент.</param>
	/// <returns>True, если это один и тот же студент или оба значения пустые.</returns>
	public static bool operator ==(Student? left, Student? right)
	{
		if (left is null)
		{
			return right is null;
		}

		return left.Equals(right);
	}

	/// <summary>
	/// Сравнивает двух студентов оператором !=.
	/// </summary>
	/// <param name="left">Первый студент.</param>
	/// <param name="right">Второй студент.</param>
	/// <returns>True, если студенты разные.</returns>
	public static bool operator !=(Student? left, Student? right)
	{
		return !(left == right);
	}

	/// <summary>
	/// Сравнивает с другим студентом по номеру зачётки.
	/// </summary>
	/// <param name="other">Другой студент.</param>
	/// <returns>True, если номера зачёток совпали.</returns>
	public bool Equals(Student? other)
	{
		if (other is null)
		{
			return false;
		}

		return string.Equals(RecordBookNumber, other.RecordBookNumber, StringComparison.Ordinal);
	}

	/// <summary>
	/// Сравнивает с объектом любого типа. Всё, что не студент, заведомо не равно.
	/// </summary>
	/// <param name="obj">Объект для сравнения.</param>
	/// <returns>True, если передан тот же студент.</returns>
	public override bool Equals(object? obj)
	{
		if (obj is Student other)
		{
			return Equals(other);
		}

		return false;
	}

	/// <summary>
	/// Хеш строится по номеру зачётки, как и сравнение.
	/// </summary>
	/// <returns>Хеш-код.</returns>
	public override int GetHashCode()
	{
		return RecordBookNumber.GetHashCode();
	}

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{FullName} (зачётка {RecordBookNumber})";
	}
}
