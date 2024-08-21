using UebungDelegates;

namespace Uebung;

internal class Program
{
	static void Main(string[] args)
	{
		PrimeComponent comp = new();
		//comp.Prime += Comp_Prime;
		comp.Prime += (zahl) => Console.WriteLine($"Primzahl: {zahl}");
		//comp.Prime100 += Comp_Prime100;
		comp.Prime100 += (sender, args) => Console.WriteLine($"Hundertste Primzahl: {args.zahl}");
		//comp.NotPrime += Comp_NotPrime;
		comp.NotPrime += (sender, args) => Console.WriteLine($"Keine Primzahl: {args.zahl}, Teiler: {args.teiler}");
		comp.CalculateNumbers();
	}

	private static void Comp_NotPrime(object? sender, NotPrimeEventArgs e) => Console.WriteLine($"Keine Primzahl: {e.zahl}, Teiler: {e.teiler}");

	private static void Comp_Prime100(object? sender, Prime100EventArgs e) => Console.WriteLine($"Hundertste Primzahl: {e.zahl}");

	private static void Comp_Prime(int obj) => Console.WriteLine($"Primzahl: {obj}");

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

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
