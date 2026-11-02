using System.Collections;

namespace Norbit.Study.Kgu.PracticeLib;

/// <summary>
/// Массив, который сам увеличивается при добавлении элементов.
/// </summary>
/// <typeparam name="T">Тип хранимых элементов.</typeparam>
public class DynamicArray<T> : IEnumerable<T>
{
	/// <summary>
	/// Ёмкость нового массива, если её не указали.
	/// </summary>
	private const int InitialCapacity = 8;

	/// <summary>
	/// Множитель ёмкости при расширении.
	/// </summary>
	private const int CapacityMultiplier = 2;

	/// <summary>
	/// Хранилище элементов. Заполнено от начала до <see cref="_count"/>.
	/// </summary>
	private T[] _buffer;

	/// <summary>
	/// Число занятых ячеек хранилища.
	/// </summary>
	private int _count;

	/// <summary>
	/// Создаёт массив со стандартной ёмкостью.
	/// </summary>
	public DynamicArray()
	{
		_buffer = new T[InitialCapacity];
	}

	/// <summary>
	/// Создаёт массив с заданной ёмкостью.
	/// </summary>
	/// <param name="capacity">Сколько элементов поместится без расширения.</param>
	/// <exception cref="ArgumentOutOfRangeException">Ёмкость меньше нуля.</exception>
	public DynamicArray(int capacity)
	{
		if (capacity < 0)
		{
			throw new ArgumentOutOfRangeException(nameof(capacity), capacity, "Ёмкость массива не бывает отрицательной.");
		}

		_buffer = new T[capacity];
	}

	/// <summary>
	/// Создаёт массив и переносит в него элементы коллекции. Ёмкость совпадает с их числом.
	/// </summary>
	/// <param name="items">Коллекция, из которой берутся элементы.</param>
	/// <exception cref="ArgumentNullException">Вместо коллекции передан null.</exception>
	public DynamicArray(IEnumerable<T> items)
	{
		ArgumentNullException.ThrowIfNull(items);

		_buffer = new T[CountItems(items)];
		CopyItems(items);
	}

	/// <summary>
	/// Сколько элементов сейчас в массиве.
	/// </summary>
	public int Length => _count;

	/// <summary>
	/// Сколько элементов помещается во внутренний массив.
	/// </summary>
	public int Capacity => _buffer.Length;

	/// <summary>
	/// Чтение и запись элемента по его номеру.
	/// </summary>
	/// <param name="index">Номер элемента, счёт идёт с нуля.</param>
	/// <exception cref="ArgumentOutOfRangeException">Элемента с таким номером нет.</exception>
	public T this[int index]
	{
		get
		{
			ThrowIfOutside(index, _count - 1);

			return _buffer[index];
		}

		set
		{
			ThrowIfOutside(index, _count - 1);
			_buffer[index] = value;
		}
	}

	/// <summary>
	/// Кладёт элемент в конец. Если места нет, ёмкость удваивается.
	/// </summary>
	/// <param name="item">Новый элемент.</param>
	public void Add(T item)
	{
		if (_count == Capacity)
		{
			Grow(_count + 1);
		}

		_buffer[_count] = item;
		_count++;
	}

	/// <summary>
	/// Дописывает в конец все элементы коллекции. Внутренний массив при этом
	/// пересоздаётся максимум один раз.
	/// </summary>
	/// <param name="items">Коллекция с новыми элементами.</param>
	/// <exception cref="ArgumentNullException">Вместо коллекции передан null.</exception>
	public void AddRange(IEnumerable<T> items)
	{
		ArgumentNullException.ThrowIfNull(items);

		var newCount = _count + CountItems(items);

		if (newCount > Capacity)
		{
			Grow(newCount);
		}

		CopyItems(items);
	}

	/// <summary>
	/// Убирает из массива первый элемент, равный заданному. Ёмкость остаётся прежней.
	/// </summary>
	/// <param name="item">Что удалить.</param>
	/// <returns>True, если такой элемент был, иначе false.</returns>
	public bool Remove(T item)
	{
		for (var i = 0; i < _count; i++)
		{
			if (!EqualityComparer<T>.Default.Equals(_buffer[i], item))
			{
				continue;
			}

			for (var j = i; j < _count - 1; j++)
			{
				_buffer[j] = _buffer[j + 1];
			}

			_count--;
			_buffer[_count] = default!;

			return true;
		}

		return false;
	}

	/// <summary>
	/// Ставит элемент на указанное место, остальные сдвигаются к концу.
	/// </summary>
	/// <param name="index">Место для элемента: от нуля до <see cref="Length"/>.</param>
	/// <param name="item">Новый элемент.</param>
	/// <returns>True, когда элемент добавлен.</returns>
	/// <exception cref="ArgumentOutOfRangeException">Место находится за пределами массива.</exception>
	public bool Insert(int index, T item)
	{
		ThrowIfOutside(index, _count);

		if (_count == Capacity)
		{
			Grow(_count + 1);
		}

		for (var i = _count; i > index; i--)
		{
			_buffer[i] = _buffer[i - 1];
		}

		_buffer[index] = item;
		_count++;

		return true;
	}

	/// <inheritdoc />
	public IEnumerator<T> GetEnumerator()
	{
		return new Enumerator(this);
	}

	/// <inheritdoc />
	IEnumerator IEnumerable.GetEnumerator()
	{
		return GetEnumerator();
	}

	/// <summary>
	/// Считает элементы коллекции. Если коллекция знает свой размер, перебора не будет.
	/// </summary>
	/// <param name="items">Коллекция.</param>
	/// <returns>Число элементов.</returns>
	private static int CountItems(IEnumerable<T> items)
	{
		if (items is ICollection<T> collection)
		{
			return collection.Count;
		}

		var count = 0;

		foreach (var item in items)
		{
			count++;
		}

		return count;
	}

	/// <summary>
	/// Дописывает элементы коллекции в хранилище. Место под них уже должно быть.
	/// </summary>
	/// <param name="items">Коллекция.</param>
	private void CopyItems(IEnumerable<T> items)
	{
		foreach (var item in items)
		{
			_buffer[_count] = item;
			_count++;
		}
	}

	/// <summary>
	/// Пересоздаёт хранилище так, чтобы в него поместилось нужное число элементов.
	/// </summary>
	/// <param name="requiredCapacity">Минимально необходимая ёмкость.</param>
	private void Grow(int requiredCapacity)
	{
		var newCapacity = Math.Max(Capacity * CapacityMultiplier, requiredCapacity);
		var newBuffer = new T[newCapacity];

		Array.Copy(_buffer, newBuffer, _count);
		_buffer = newBuffer;
	}

	/// <summary>
	/// Бросает исключение, если номер не попадает в диапазон от нуля до заданного.
	/// </summary>
	/// <param name="index">Проверяемый номер.</param>
	/// <param name="lastAllowed">Наибольший допустимый номер.</param>
	private static void ThrowIfOutside(int index, int lastAllowed)
	{
		if ((index < 0) || (index > lastAllowed))
		{
			throw new ArgumentOutOfRangeException(nameof(index), index, "Номер выходит за границы массива.");
		}
	}

	/// <summary>
	/// Перечислитель без yield: сам помнит, на каком элементе остановился.
	/// </summary>
	private sealed class Enumerator : IEnumerator<T>
	{
		/// <summary>
		/// Положение до начала перебора.
		/// </summary>
		private const int StartPosition = -1;

		// Массив, по которому идёт перебор.
		private readonly DynamicArray<T> _source;

		// Длина массива на момент начала перебора.
		private readonly int _length;

		// Номер текущего элемента.
		private int _position = StartPosition;

		/// <summary>
		/// Создаёт перечислитель для массива.
		/// </summary>
		/// <param name="source">Перебираемый массив.</param>
		public Enumerator(DynamicArray<T> source)
		{
			_source = source;
			_length = source.Length;
		}

		/// <inheritdoc />
		public T Current => _source._buffer[_position];

		/// <inheritdoc />
		object? IEnumerator.Current => Current;

		/// <inheritdoc />
		public bool MoveNext()
		{
			if (_position >= _length - 1)
			{
				return false;
			}

			_position++;

			return true;
		}

		/// <inheritdoc />
		public void Reset()
		{
			_position = StartPosition;
		}

		/// <inheritdoc />
		public void Dispose()
		{
		}
	}
}
