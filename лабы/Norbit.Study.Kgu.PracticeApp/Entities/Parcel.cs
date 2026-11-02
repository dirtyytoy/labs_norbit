namespace Norbit.Study.Kgu.PracticeApp.Entities;

/// <summary>
/// Посылка. Класс, ссылочный тип.
/// </summary>
internal class Parcel
{
	// Вес в килограммах. Значимый тип, лежит внутри объекта в куче.
	private double _weight;

	/// <summary>
	/// Создаёт посылку.
	/// </summary>
	/// <param name="address">Куда доставить.</param>
	/// <param name="weight">Вес в килограммах.</param>
	/// <exception cref="ArgumentException">Адрес не указан.</exception>
	/// <exception cref="ArgumentOutOfRangeException">Вес не положительный.</exception>
	public Parcel(string address, double weight)
	{
		if (string.IsNullOrEmpty(address))
		{
			throw new ArgumentException("У посылки должен быть адрес.", nameof(address));
		}

		Address = address;
		Weight = weight;
	}

	/// <summary>
	/// Адрес доставки. Строка — ссылочный тип.
	/// </summary>
	public string Address { get; set; }

	/// <summary>
	/// Вес в килограммах.
	/// </summary>
	/// <exception cref="ArgumentOutOfRangeException">Вес не положительный.</exception>
	public double Weight
	{
		get
		{
			return _weight;
		}

		set
		{
			if (value <= 0)
			{
				throw new ArgumentOutOfRangeException(nameof(value), value, "Вес посылки должен быть больше нуля.");
			}

			_weight = value;
		}
	}

	/// <summary>
	/// Габариты. Структура хранится в объекте целиком, а не по ссылке.
	/// </summary>
	public Dimensions Size { get; set; }

	/// <summary>
	/// Хрупкая ли посылка.
	/// </summary>
	public bool IsFragile { get; set; }

	/// <inheritdoc />
	public override string ToString()
	{
		var fragility = IsFragile
			? "хрупкая"
			: "обычная";

		return $"{Address}, {Weight} кг, {Size} см, {fragility}";
	}
}
