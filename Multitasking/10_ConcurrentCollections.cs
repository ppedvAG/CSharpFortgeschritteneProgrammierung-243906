using System.Collections.Concurrent;
using System.Collections.Generic;

namespace Multitasking;

public class ConcurrentCollections
{
    static void Main(string[] args)
    {
        ConcurrentBag<int> ints = [];
		//Task 1 entfernt ein Element, Task 2 versucht auf dieses Element zuzugreifen
		//Zum Zeitpunkt von Task 2 gab es das Element noch, aber Task 1 war schneller beim entfernen des Elements
		//Bei List<T>: Absturz, bei Concurrent Collection kommt einfach false zurück
		ints.Add(1);
		ints.Add(2);
		ints.Add(3);

		if (ints.TryPeek(out int x))
		{
			//Beliebiges Element entnehmen
		}

		if (ints.TryTake(out int y))
		{
			//Beliebiges Element entfernen
		}

		//Console.WriteLine(ints[1]); //Nicht möglich

		//////////////////////////////////////////////////////////

		SynchronizedCollection<int> zahlen = [];
		//Besser als Bag, hat []
		zahlen.Add(1);
		zahlen.Add(2);
		zahlen.Add(3);
		Console.WriteLine(zahlen[1]);

		ConcurrentDictionary<int, string> dict = [];
		dict.AddOrUpdate(0, "Null", (key, value) => dict[key] = value); //Wenn der Key schon existiert (von einem anderen Task), wird die Func am Ende ausgeführt
		dict.GetOrAdd(0, value => dict[0]); //Wenn der Key schon existiert (von einem anderen Task), wird die Func am Ende ausgeführt
		dict.TryAdd(0, "Null"); //Normales Add, ohne Besonderheiten
    }
}