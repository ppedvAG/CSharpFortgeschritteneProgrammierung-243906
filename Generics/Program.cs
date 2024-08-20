using System.Collections;

namespace Generics;

public class Program
{
	static void Main(string[] args)
	{
		//Platzhalter für Typen (T)
		List<int> ints = new List<int>(); //Überall wo sich in dieser Klasse ein T befindet, wird dieses durch int ersetzt
		ints.Add(1); //T wird durch int ersetzt

		List<string> strings = new List<string>();
		strings.Add("Hallo"); //T wird durch string ersetzt

		Test<int> t = [];
		for (int i = 0; i < t.Data.Length; i++)
			t.Add(i, i);

		foreach (int i in t)
		{
			//Hier wird GetEnumerator() ausgeführt
        }
	}

	//Methode mit Generic
	public static void TestGeneric<T>()
	{
        Console.WriteLine(default(T)); //Standardwert von T ermitteln
        Console.WriteLine(nameof(T)); //Name des Typens hinter T ("int", "string", "bool", ...)
        Console.WriteLine(typeof(T)); //Typ hinter T

		if (typeof(T) == typeof(int))
		{

		}

		T field = (T) new object(); //Cast mit T
    }
}

public class Test<T> : IEnumerable<T>
{
	private T[] _data = new T[5]; //T bei einem Feld

	public T[] Data => _data; //T bei Property

	public void Add(T value, int index) //T als Parameter
	{
		_data[index] = value;
	}

	public IEnumerator<T> GetEnumerator()
	{
		foreach (T obj in _data) //T als Schleifenvariable
		{
            //Console.WriteLine(obj);
            yield return obj;
		}
		//Enumerator: Zeiger, welcher auf ein Arrayelement zeigt
		//foreach + yield return: Bewege den Zeiger auf das nächste Element, und gib dieses zurück
	}

	IEnumerator IEnumerable.GetEnumerator()
	{
		foreach (T obj in _data) //T als Schleifenvariable
			yield return obj;
	}

	public T this[int index] => _data[index]; //T als Rückgabewert
}