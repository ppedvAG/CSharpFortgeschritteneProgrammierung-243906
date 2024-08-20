using TestTupel = (int, string);
using PersonPerCountry = System.Collections.Generic.Dictionary<string, System.Collections.Generic.List<Sprachfeatures.Person>>;

namespace Sprachfeatures;

internal class Program
{
	static void Main(string[] args)
	{
		//int x = 1;
		if (int.TryParse("abc", out int x))
		{
			Console.WriteLine(x);
		}
		Console.WriteLine(x);

		//Typvergleiche

		//Vererbungshierarchietypvergleich
		object y = 1;
		if (y is int)
		{
			//IComparable
			//object
			//int
		}

		//Genauer Typvergleich
		if (y.GetType() == typeof(int))
		{
			//object
		}

		(int, string) t = (123, "Hallo");
		Console.WriteLine(t.Item1);
		Console.WriteLine(t.Item2);

		//Tupel in Einzelteile zerlegen
		int z;
		string s;
		(z, s) = t;

		void Test()
		{
			Console.WriteLine("Hallo");
		}

		Test();

		double d = 2_398_579_325.18_257_983_214;

		//class vs. struct

		//class
		//Referenztyp
		//Wenn ein Referenztyp zugewiesen wird, wird eine Referenz erzeugt
		//Wenn zwei Objekte eines Referenztypens verglichen werden, werden die Speicheradressen verglichen

		Test original = new Test(10);
		original.Zahl = 10;
		Test neu = original; //Hier wird eine Referenz auf das Objekt unter original gelegt
		neu.Zahl = 20; //Beide Objekte werden verändert -> selbes Objekt

		Console.WriteLine(original.GetHashCode());
		Console.WriteLine(neu.GetHashCode());
		Console.WriteLine(original == neu); //Hier werden die HashCodes verglichen

		//struct
		//Wertetyp
		//Wenn ein Wertetyp zugewiesen wird, wird eine Kopie erzeugt (zwei Objekte im RAM)
		//Wenn zwei Objekte eines Wertetypens verglichen werden, werden die Inhalte verglichen

		int originalStruct = 10;
		int neuStruct = originalStruct; //Kopie
		neuStruct = 20; //Unterschiedliche Werte

		Console.WriteLine(originalStruct.GetHashCode());
		Console.WriteLine(neuStruct.GetHashCode());
		Console.WriteLine(originalStruct == neuStruct); //Hier werden die HashCodes verglichen

		int a = 10;
		RefTest(ref a);
		ref int b = ref a;

		Console.WriteLine(default(double));

		string str = a switch
		{
			0 => "Null",
			1 => "Eins",
			2 => "Zwei",
			> 3 and < 10 => "zw. 3 und 10",
			_ => "Andere Zahl"
		};

		string str2 = original switch
		{
			{ Zahl: 10 } => "Zehn",
			_ => "andere Zahl"
		};

		//Null-Coalescing Operator (??-Operator)
		//Nimm die Linke Seite wenn diese nicht null ist, sonst die rechte Seite
		string text;
		int? zahl = 1;
		if (zahl != null)
			text = zahl.ToString();
		else
			text = "Keine Zahl";

		//Mit ?-Operator
		text = zahl != null ? zahl.ToString() : "Keine Zahl";

		//Mit ??-Operator
		text = zahl.ToString() ?? "Keine Zahl";

		Person p = new(0, "Max", "Mustermann", new DateTime(1990, 1, 1));
		Console.WriteLine(p);

		text = p switch
		{
			var (id, vn, nn, gd) when id == 0 => "Hallo",
			_ => ""
		};

		string i1 = $"Hallo \"Welt\"";
		string i2 = $"""Hallo "Welt""";
		string i3 = $$"""Hallo {{{p}}} {Welt}""";
		string i4 = $"Hallo {{{p}}} {{Welt}}";

		int[] arr1 = { 1, 2, 3 };
		int[] arr2 = [1, 2, 3];

		List<int> ints1 = new List<int>();
		List<int> ints2 = new();
		List<int> ints3 = [];

		TestTupel tt = (123, "ABC");
		PersonPerCountry ppc = [];
	}

	public static void RefTest(ref int x)
	{
		x = 100;
	}
}

public class Test(int Zahl)
{
	public int Zahl = Zahl;
}

//Kann nicht verändert werden
public record Person(int ID, string Vorname, string Nachname, DateTime GebDat)
{
	public void Test()
	{

	}
}