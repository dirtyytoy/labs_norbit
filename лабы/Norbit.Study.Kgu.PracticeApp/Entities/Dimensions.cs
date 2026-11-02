namespace Norbit.Study.Kgu.PracticeApp.Entities;

/// <summary>
/// Габариты в сантиметрах. Структура, значимый тип.
/// </summary>
internal struct Dimensions
{
	/// <summary>
	/// Ширина.
	/// </summary>
	public int Width;

	/// <summary>
	/// Высота.
	/// </summary>
	public int Height;

	/// <summary>
	/// Создаёт габариты.
	/// </summary>
	/// <param name="width">Ширина.</param>
	/// <param name="height">Высота.</param>
	public Dimensions(int width, int height)
	{
		Width = width;
		Height = height;
	}

	/// <summary>
	/// Площадь.
	/// </summary>
	public int Area => Width * Height;

	/// <inheritdoc />
	public override string ToString()
	{
		return $"{Width}x{Height}";
	}
}
