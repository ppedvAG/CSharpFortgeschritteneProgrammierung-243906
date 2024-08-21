using System.Diagnostics;

namespace LinqErweiterungsmethoden;

internal class Program
{
	static void Main(string[] args)
	{
		List<int> list = Enumerable.Range(1, 20).ToList();

		#region Listentheorie
		//Alle geraden Zahlen finden
		IEnumerable<int> where = list.Where(e => e % 2 == 0);

		//IEnumerable
		//Jede Linq Funktion gibt immer ein IEnumerable zurück
		//IEnumerable ist nur eine Anleitung
		//Mit einer Konvertierungsfunktion (ToArray, ToList, foreach-Schleife) werden die Daten erstellt (=> Resourcen verwendet)

		//Wie lange dauert dieses Statement?
		//1 Mrd. Zahlen * 4 Byte = 4GB
		Enumerable.Range(1, (int) 1E9); //1ms, weil Anleitung

		//Enumerable.Range(1, (int) 1E9).ToList(); //3s, weil konkret Daten erzeugt werden

		//IEnumerator
		//Grundlegende Listenkomponente
		//Zeiger, welcher auf die Elemente der Liste zeigt
		//Kann um ein Element weiterbewegt werden
		foreach (int i in where) //Hier wird nur der Enumerator verwendet
			Console.WriteLine(i);

		IEnumerator<int> enumerator = where.GetEnumerator();
		enumerator.MoveNext();
		start:
        Console.WriteLine(enumerator.Current);
		if (enumerator.MoveNext())
			goto start;
        Console.WriteLine("Fertig");
        #endregion

        #region Einfaches Linq
        Console.WriteLine(list.Average());
        Console.WriteLine(list.Min());
        Console.WriteLine(list.Max());
        Console.WriteLine(list.Sum());

        Console.WriteLine(list.First()); //Gibt das erste Element zurück, Exception wenn kein Element gefunden wird
        Console.WriteLine(list.Last());

		Console.WriteLine(list.FirstOrDefault()); //Gibt das erste Element zurück, null wenn kein Element gefunden wird
		Console.WriteLine(list.LastOrDefault());

        //Console.WriteLine(list.First(e => e % 50 == 0));
        Console.WriteLine(list.FirstOrDefault(e => e % 50 == 0));

		//Predicate & Selector
		//Manche Funktionen nehmen ein Predicate (= Bedingung), Func welche einen Boolean zurückgibt
		//z.B.: Where, First, Count, All, Any, ...
		//Manche Funktionen nehmen einen Selector (= Zeiger auf ein Feld), Func welche ein Generic zurückgibt
		//z.B.: Average, Min, Max, Sum, GroupBy, Select, ...
		#endregion

		List<Fahrzeug> fahrzeuge = new List<Fahrzeug>
		{
			new Fahrzeug(251, FahrzeugMarke.BMW),
			new Fahrzeug(274, FahrzeugMarke.BMW),
			new Fahrzeug(146, FahrzeugMarke.BMW),
			new Fahrzeug(208, FahrzeugMarke.Audi),
			new Fahrzeug(189, FahrzeugMarke.Audi),
			new Fahrzeug(133, FahrzeugMarke.VW),
			new Fahrzeug(253, FahrzeugMarke.VW),
			new Fahrzeug(304, FahrzeugMarke.BMW),
			new Fahrzeug(151, FahrzeugMarke.VW),
			new Fahrzeug(250, FahrzeugMarke.VW),
			new Fahrzeug(217, FahrzeugMarke.Audi),
			new Fahrzeug(125, FahrzeugMarke.Audi)
		};

		#region Linq mit Objekten
		//Aufgabenstellung: Alle Fahrzeuge, welche mind. 250km/h fahren können
		fahrzeuge.Where(f => f.MaxV >= 250);

		//Aufgabenstellung: Alle VWs, welche mind. 250km/h fahren können
		fahrzeuge.Where(f => f.Marke == FahrzeugMarke.VW && f.MaxV >= 250);
		fahrzeuge
			.Where(f => f.Marke == FahrzeugMarke.VW)
			.Where(f => f.MaxV >= 250);

		//Nach Marke sortieren
		fahrzeuge.OrderBy(f => f.Marke);

		//Nach Marke & Geschwindigkeit sortieren
		fahrzeuge.OrderBy(f => f.Marke).ThenBy(f => f.MaxV);

		//Absteigend
		fahrzeuge.OrderByDescending(f => f.Marke).ThenByDescending(f => f.MaxV);

		//All & Any
		//Prüfen ob die gesamte Liste oder mindestens ein Element einer Bedingung entsprechen
		if (fahrzeuge.All(e => e.MaxV > 250)) //Fahren alle Fahrzeuge mind. 250km/h?
		{
			//...
		}

		if (fahrzeuge.Any(e => e.MaxV > 250)) //Fährt mind. ein Fahrzeug mind. 250km/h?
		{
			//...
		}

		//Prüfen, ob alle Zeichen in einem String Buchstaben sind
		string text = "Hallo Welt";
		foreach (char c in text)
			if (!char.IsLetter(c))
                Console.WriteLine("Kein Buchstabe");
		text.All(char.IsLetter);
		text.Any(c => !char.IsLetter(c));

		//Wieviele BMWs haben wir?
		fahrzeuge.Count(e => e.Marke == FahrzeugMarke.BMW); //Ergebnis: int (4)

		//Average, Min, Max, Sum
		//Wie schnell fahren unsere Fahrzeuge im Durchschnitt?
		fahrzeuge.Average(e => e.MaxV); //208.41666666666666

		//Welcher ist der schnellste VW?
		int hoch = fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.VW)
			.Max(e => e.MaxV); //Die höchste Geschwindigkeit

		Fahrzeug schnell = fahrzeuge
			.Where(e => e.Marke == FahrzeugMarke.VW)
			.MaxBy(e => e.MaxV); //Das Fahrzeug mit der höchsten Geschwindigkeit

		//Skip & Take
		//Teile einer Liste überspringen/nehmen

		//Beispiel: Webshop, 10 Artikel pro Seite
		int seite = 0;
		fahrzeuge.Skip(seite * 10).Take(10);

		fahrzeuge.Skip(0 * 10).Take(10); //Seite 1
		fahrzeuge.Skip(1 * 10).Take(10); //Seite 2

		//Finde die 3 schnellsten Autos
		fahrzeuge
			.OrderByDescending(e => e.MaxV)
			.Take(3);

		//Select
		//Formt eine Liste um
		//Die Select Funktion nimmt eine Func als Parameter
		//Jedes Listenelement wird in die Func hineingeworfen, das Ergebnis der Func kommt dann beim Select als zurück

		//Beispiele: Einen Ordner einlesen und alle Pfade und Endungen entfernen
		string[] files = Directory.GetFiles("C:\\Windows");
		List<string> filesOhnePfade = [];
		foreach (string file in files)
		{
			filesOhnePfade.Add(Path.GetFileNameWithoutExtension(file));
		}

		List<string> filesLinq = 
			Directory.GetFiles("C:\\Windows")
				.Select(file => Path.GetFileNameWithoutExtension(file)) //Schleife über alle Elemente, wendet die Funktion auf jedes Element an
				.ToList();

		//Beispiel: Liste casten
		//Enumerable.Range(1, 20).Cast<int>(); //Funktioniert nicht
		Enumerable.Range(1, 20).Select(e => (double) e);

		//Einzelnes Feld aus einer Liste extrahieren
		IEnumerable<int> g = fahrzeuge.Select(e => e.MaxV);
		IEnumerable<FahrzeugMarke> m = fahrzeuge.Select(e => e.Marke);

		//Welche Marken haben wir?
		fahrzeuge
			.Select(e => e.Marke)
			.Distinct();

		//SelectMany
		//Glättet eine Liste
		List<List<int>> zahlen = [[1, 2, 3], [4, 5, 6], [7, 8, 9]];
		List<int> flach = [];
		foreach (List<int> inner in zahlen)
			foreach (int x in inner)
				flach.Add(x);

		zahlen.SelectMany(e => e);

		//Chunk
		fahrzeuge.Chunk(10); //Teilt die Liste in 10 große Teile auf

		//GroupBy
		//Erstellt Gruppen anhand eines Kriteriums und steckt jedes Element in seine Gruppe
		fahrzeuge.GroupBy(e => e.Marke); //Audi-Gruppe, BMW-Gruppe, VW-Gruppe

		//Wie bekommen wir die Daten aus dem GroupBy hinaus?
		var lookup = fahrzeuge.GroupBy(e => e.Marke).ToLookup(e => e.Key);
		var bmws = lookup[FahrzeugMarke.BMW]
			.Single() //Bei einem Lookup sind die Keys nicht eindeutig, hier muss die erste (und einzige) Gruppe entnommen werden
			.ToList();

		fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.ToList());

		//Was ist das schnellste Fahrzeug pro Marke?
		fahrzeuge
			.GroupBy(e => e.Marke)
			.ToDictionary(e => e.Key, e => e.MaxBy(x => x.MaxV));
		#endregion

		#region Erweiterungsmethoden
		//Erweiterungsmethoden
		//Alle Linq Methoden sind selbst Erweiterungsmethoden auf IEnumerable<T>
		//Dadurch ist es möglich, auf Listentypen in C# Linq anzuwenden
		int i = 3284;
		i.Quersumme();
        Console.WriteLine(28395798.Quersumme());

		//Wie funktionieren Erweiterungsmethoden im Hintergrund?
		i.Quersumme(); //->
		ExtensionMethods.Quersumme(i);

		//Linq Umbau
		fahrzeuge.Where(f => f.MaxV >= 250);
		Enumerable.Where(fahrzeuge, f => f.MaxV >= 250);
		#endregion
	}
}

[DebuggerDisplay("Marke: {Marke}, MaxV: {MaxV}")]
public class Fahrzeug
{
	public Fahrzeug(int maxV, FahrzeugMarke marke)
	{
		MaxV = maxV;
		Marke = marke;
	}

	public int MaxV { get; set; }

	public FahrzeugMarke Marke { get; set; }
}

public enum FahrzeugMarke { Audi, BMW, VW }