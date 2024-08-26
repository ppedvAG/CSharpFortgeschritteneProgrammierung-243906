namespace Uebung;

internal class Program
{
	public static void ForEach<T>(IEnumerable<T> list, Action<T> a)
	{
		if (list is null ||  a is null)
		{
			throw new ArgumentNullException();
		}

		foreach (T item in list)
		{
			a?.Invoke(item);
		}
	}

	public static List<TReturn> ForEachReturn<T, TReturn>(IEnumerable<T> list, Func<T, TReturn> f)
	{
		if (list is null || f is null)
			throw new ArgumentNullException();

		List<TReturn> ret = [];
		foreach (T item in list)
		{
			TReturn value = f.Invoke(item);
			if (value != null)
				ret.Add(value);
		}
		return ret;
	}

	public static IEnumerable<TReturn> ForEachYield<T, TReturn>(IEnumerable<T> list, Func<T, TReturn> f)
	{
		if (list is null || f is null)
			throw new ArgumentNullException();

		foreach (T item in list)
		{
			TReturn value = f.Invoke(item);
			if (value != null)
				yield return value;
		}
	}
}
