namespace Delegates;

internal class Program
{
	/// <summary>
	/// Delegates
	/// 
	/// Eigener Typ, welcher Methodenzeiger speichern kann
	/// Das Delegate gibt eine Struktur vor, welche vorgibt, welche Methoden kompatibel sind
	/// </summary>
	public delegate void Vorstellungen(string name);

	static void Main(string[] args)
	{
		Vorstellungen v = new Vorstellungen(VorstellungDE); //Erstellung des Delegates mit Initialmethode
															//WICHTIG: Methodenzeiger werden ohne Klammern mitgegeben

		v("Max"); //Delegate ausführen

        Console.WriteLine("---------------------------------");

        v += VorstellungEN; //Weitere Methode anhängen
		v("Tim"); //Beide Methoden wurden in der entsprechenden Reihenfolge ausgeführt

		Console.WriteLine("---------------------------------");

		//Methoden können mehrmals angehängt werden
		v += VorstellungEN;
		v += VorstellungEN;
		v += VorstellungEN;
		v("Udo");

		Console.WriteLine("---------------------------------");

		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v -= VorstellungEN;
		v("Max");

		Console.WriteLine("---------------------------------");

		v -= VorstellungDE;

		//Wenn die letzte Methode abgenommen wird, ist das Delegate null
		//v("Tim");

		//Vor einem Delegateaufruf sollte immer ein Null-Check gemacht werden
		if (v is not null)
			v("Tim");

		v?.Invoke("Tim"); //Null-Propagation: Wenn v nicht null ist, führe den Code danach aus, sonst überspringe den Code

		List<int> ints = null;
		ints?.Add(1); //Wenn die Liste nicht null ist, füge 1 hinzu

		//Delegate anschauen
		foreach (Delegate dg in v.GetInvocationList())
		{
			Console.WriteLine(dg.Method.Name); //Einzelnen Methoden ausgeben
		}
	}

	public static void VorstellungDE(string s)
	{
        Console.WriteLine($"Hallo mein Name ist {s}");
    }

	public static void VorstellungEN(string s)
	{
        Console.WriteLine($"Hello my name is {s}");
    }
}
