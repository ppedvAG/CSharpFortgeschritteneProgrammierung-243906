using System.Reflection;

namespace Reflection;

internal class Program
{
	static void Main(string[] args)
	{
		//Reflection
		//Zur Laufzeit alle möglichen Informationen über ein Objekt erhalten
		//Geht immer von Type-Objekten aus, jedes Objekt in C# hat einen Type
		//2 Möglichkeiten um einen Type zu bekommen: .GetType(), typeof(...)

		object o = 123;
		Type t1 = o.GetType();
		Type t2 = typeof(object);

		////////////////////////////////

		object p = new Person();
		PropertyInfo[] prop = p.GetType().GetProperties(); //Liste aller Properties der Klasse
		prop[0].SetValue(p, "Max"); //WICHTIG: Reflection bezieht sich immer auf Types, und nicht auf Objekte
		prop[1].SetValue(p, 34);
		MethodInfo[] methods = p.GetType().GetMethods(); //Reflection gibt immer ein Info[] zurück
		methods[0].Invoke(p, null);

		//Activator
		//Kann verwendet werden, um über Typen ein Objekt zu erstellen
		object a = Activator.CreateInstance(typeof(Person));
		object b = Activator.CreateInstance("Reflection", "Reflection.Person");

		//Assembly
		//Ein Projekt, enthält allen Code aus dem entsprenchenden Projekt
		//Gibt Zugriff auf den gesamten Inhalt des Projekts/der DLL
		Assembly assembly = Assembly.GetExecutingAssembly();
		Type[] types = assembly.GetTypes(); //GetTypes: Alle Typen des Projekts entnehmen

		//Aufgabenstellung: PrimeComponent laden und verwenden
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_08_19\UebungDelegates\bin\Debug\net8.0\UebungDelegates.dll";
		Assembly loaded = Assembly.LoadFrom(pfad);
		object comp = Activator.CreateInstance(loaded.GetType("UebungDelegates.PrimeComponent"));
		comp.GetType().GetEvent("Prime").AddEventHandler(comp, (int i) => Console.WriteLine(i));
		//comp.GetType().GetEvent("Prime100").AddEventHandler(comp, (EventHandler<EventArgs> e) => Console.WriteLine(e.GetType().GetProperty("zahl").GetValue(e)));
		//comp.GetType().GetEvent("NotPrime").AddEventHandler(comp,
		//	(EventHandler<EventArgs> e) => Console.WriteLine(e.GetType().GetProperty("zahl").GetValue(e) + ", " + e.GetType().GetProperty("teiler").GetValue(e)));
		comp.GetType().GetMethod("CalculateNumbers").Invoke(comp, null);
	}
}

public class Person
{
	public void PrintPerson()
	{
		Console.WriteLine($"{Vorname} ist {Alter} Jahre alt");
	}

	public string Vorname { get; set; }

	public int Alter { get; set; }
}