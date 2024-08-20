using System.Collections;

namespace WeitereFeatures;

internal class Program
{
	static void Main(string[] args)
	{
		string str = "Hallo";
		str += " Welt";

		DateTime dt = DateTime.Now;
		dt += TimeSpan.FromDays(3);

		if (DateTime.Now > dt)
		{ }

		Wagon a = new();
		Wagon b = new();
        Console.WriteLine(a.GetHashCode());
        Console.WriteLine(b.GetHashCode());
        Console.WriteLine(a == b);

		Zug z = new();
		z++;
		z += a;
		z += b;

		Zug z2 = new();
		z += z2;

		double x = 10;
		int y = (int) x;

		Zug z3 = a;

		foreach (Wagon w in z)
		{

		}

		//z[3] = new Wagon();
  //      Console.WriteLine(z[30, "Rot"]);

		//Was passiert im Hintergrund?
		string s = "Hallo"; //Hallo wird im RAM abgelegt
		s += "Welt"; //Welt wird im RAM abgelegt, danach wird eine Kopie erzeugt die die Summe ergibt
					 //3 Strings im RAM (Hallo, Welt, Welt)

		string gesamt = "";
		for (int i =0; i < 100; i++)
		{
			gesamt += i;
			//Console.WriteLine(gesamt); //Ohne StringBuilder
            Console.WriteLine(i); //Mit StringBuilder
		}
		Console.WriteLine(gesamt); //Mit StringBuilder
	}
}

public class Zug : IEnumerable
{
	//++, +
	public List<Wagon> Wagons = [];

	public static Zug operator ++(Zug z)
	{
		z.Wagons.Add(new Wagon());
		return z;
	}

	public static Zug operator +(Zug z, Wagon w)
	{
		z.Wagons.Add(w);
		return z;
	}

	public static Zug operator +(Zug z, Zug z2)
	{
		z.Wagons.AddRange(z2.Wagons);
		return z;
	}

	public static implicit operator Zug(Wagon w)
	{
		return new Zug();
	}

	public IEnumerator GetEnumerator()
	{
		return Wagons.GetEnumerator();
	}

	public Wagon this[int i]
	{
		get => Wagons[i];
		set => Wagons[i] = value;
	}

	public Wagon this[int anz, string f]
	{
		get => Wagons.First(e => e.AnzSitze == anz && e.Farbe == f);
	}
}

public class Wagon
{
	//==, !=

	public int AnzSitze;

	public string Farbe;

	public static bool operator ==(Wagon a, Wagon b)
	{
		return a.AnzSitze == b.AnzSitze && a.Farbe == b.Farbe;
	}

	public static bool operator !=(Wagon a, Wagon b)
	{
		//return a.AnzSitze != b.AnzSitze || a.Farbe != b.Farbe;
		return !(a == b);
	}
}