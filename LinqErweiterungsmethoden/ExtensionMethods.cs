namespace LinqErweiterungsmethoden;

public static class ExtensionMethods
{
	public static int Quersumme(this int x)
	{
		return (int) x.ToString().Sum(char.GetNumericValue); //½, ¾
	}

	//Eigene Linq-Funktion
	//Flatten() statt SelectMany(e => e)
	//public static IEnumerable<TResult> Flatten<TSource, TResult>(this IEnumerable<TSource> list)
	//{
	//	return list.SelectMany(e => e);
	//}
}