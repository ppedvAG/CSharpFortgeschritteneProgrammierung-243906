namespace Delegates;

public class ActionFunc
{
	/// <summary>
	/// Action, Func
	/// Es gibt in C# vorgegebene Delegates, welche an verschiedenen Stellen in der Sprache vorkommen
	/// Werden in der Fortgeschrittenen Programmierung mit C# immer benötigt
	/// Wichtigste: Action & Func
	/// 
	/// Beispiele: parallele Programmierung, Linq, Reflection, ...
	/// </summary>
	static void Main(string[] args)
	{
		//Action: Delegate mit void als Rückgabewert und bis zu 16 Parametern
		//Über die Generics werden die Parametertypen + Anzahlen vorgegeben
		
		//Action<int> a = new Action<int>(Test);
		Action<int> a = Test; //Kurzform
		a(10);
		a += Test;
		a -= Test;
		a?.Invoke(5);
		foreach (Delegate dg in a.GetInvocationList()) { }
		//Alles von gerade eben ist auch hier möglich

		///////////////////////////////////////////////////////////////////////////////////

		Action<int, int> addiere = Addiere;
		addiere?.Invoke(5, 9);

		DoAction(4, 6, addiere); //Hier kann ich jetzt entscheiden, was diese Methode tun soll
		DoAction(7, 3, Addiere);

		//Task t = new Task(...) //Hier wird auch ein Methodenzeiger benötigt, um eine parallele Arbeit festzulegen

		///////////////////////////////////////////////////////////////////////////////////

		//Func: Delegate mit einem beliebigen Rückgabewert und bis zu 16 Parametern
		//WICHTIG: Das letzte Generic gibt den Rückgabetypen vor
		Func<int, int, double> func = Multipliziere;
		//Alles wie bei der Einführung ist auch hier möglich (+=, -=, ...)
		
		double? d = func?.Invoke(5, 8); //Wenn eine Func ausgeführt wird, kommt ein Ergebnis heraus (hier double)
										//Hier muss ein Nullable Double verwendet werden (double?), weil die Func selbst null sein könnte

		double d2 = (func?.Invoke(5, 8)).Value; //Wert aus dem Nullable Type entnehmen (gefährlich)
		double d3 = func?.Invoke(5, 8) ?? double.NaN; //Wenn die Linke Seite null ist, nimm die rechte Seite, sonst nimm den Wert

		if (func is null)
            Console.WriteLine("...");
		double d4 = func.Invoke(5, 8);

		DoFunc(4, 9, Multipliziere);
		DoFunc(4, 9, Dividiere);
		DoFunc(4, 9, func);

		//Aufgabenstellung: Alle geraden Zahlen finden
		List<int> list = [1, 2, 3, 4, 5];
		foreach (int i in list)
			if (i % 2 == 0)
				Console.WriteLine(i);
		//list.Where(...) //Methodenzeiger in der Klammer übergeben, um die Bedingung für die Filterung anzugeben

		///////////////////////////////////////////////////////////////////////////////////

		//Anonyme Methoden: Methoden, die keine separate Implementation haben (nur einmal verwendet werden)
		func += delegate (int x, int y) { return x + y; }; //Anonyme Methode

		func += (int x, int y) => { return x + y; }; //Kürzere Form

		func += (x, y) => { return x - y; };

		func += (x, y) => (double) x / y; //Kürzeste, häufigste Form

		//Wenn eine anonyme Methode angelegt wird, wird diese als Lambda Expression bezeichnet (=>)
		//Aufbau: (Par1, Par2, Par3, ...) => Body
		DoAction(4, 9, (x, y) => Console.WriteLine(x + y));
		DoFunc(5, 8, (x, y) => x % y);

		//Aufgabenstellung: Alle geraden Zahlen finden
		list.Where((i) => i % 2 == 0); //Where legt eine Schleife über die Liste, wobei i das derzeitige Element ist

		//Aufgabenstellung: Alle Zahlen ausgeben
		list.ForEach(i => Console.WriteLine(i));
		list.ForEach(Console.WriteLine); //Action: Methode mit void und bis zu 16 Parametern
	}

	#region Action
	static void Test(int x) { }

	static void Addiere(int x, int y) => Console.WriteLine($"{x} + {y} = {x + y}");

	/// <summary>
	/// Methoden können Delegates als Parameter empfangen, um das Verhalten der Methoden zu verändern
	/// 
	/// Der User kann jetzt selbst entscheiden, was diese Methode tun soll
	/// </summary>
	static void DoAction(int x, int y, Action<int, int> a)
	{
		if (x > 0 && y > 0)
			a?.Invoke(x, y);
	}
	#endregion

	#region	Func
	public static double Multipliziere(int x, int y) => x * y;

	public static double Dividiere(int x, int y) => (double) x / y;

	public static double DoFunc(int x, int y, Func<int, int, double> f)
	{
		if (f is null)
			return double.NaN;
		return f.Invoke(x, y);
	}
	#endregion
}