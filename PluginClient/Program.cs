using PluginBase;
using System.Reflection;

namespace PluginClient;

internal class Program
{
	/// <summary>
	/// Plugins laden
	/// </summary>
	static void Main(string[] args)
	{
		string pfad = @"C:\Users\lk3\source\repos\CSharp_Fortgeschritten_2024_08_19\PluginCalculator2\bin\Debug\net8.0\PluginCalculator2.dll";
		Assembly a = Assembly.LoadFrom(pfad);

		//Einfache Methode
		//Plugin erstellen
		//object o = Activator.CreateInstance(a.GetType("PluginCalculator.Calculator"));

		////Methoden auflisten
		//MethodInfo[] array = o.GetType().GetMethods();
  //      Console.WriteLine("Wähle eine Methode aus:");
  //      for (int i = 0; i < array.Length; i++)
		//{
		//	MethodInfo m = array[i];
  //          Console.WriteLine($"{i}: {m.Name}");
  //      }

		////User Input
		//ConsoleKeyInfo info = Console.ReadKey();
		//int x = (int) char.GetNumericValue(info.KeyChar);
  //      Console.WriteLine(array[x].Invoke(o, [1.5, 2.3]));

		//Probleme: Namespace + Klassenname müssen angegeben werden, object Methoden werden angezeigt

		/////////////////////////////////////////////////////////////////

		//Mit PluginBase + Attributen
		IPlugin plugin = (IPlugin) Activator.CreateInstance(a.GetTypes().First(e => e.GetInterface(nameof(IPlugin)) != null));

		//Methoden auflisten
		MethodInfo[] array = plugin.GetType().GetMethods()
			.Where(e => e.GetCustomAttribute<ReflectionVisible>() != null) //Attribute verarbeiten
			.ToArray();
		Console.WriteLine("Wähle eine Methode aus:");
		for (int i = 0; i < array.Length; i++)
		{
			MethodInfo m = array[i];
			Console.WriteLine($"{i}: {m.GetCustomAttribute<ReflectionVisible>().Name}");
		}

		//User Input
		ConsoleKeyInfo info = Console.ReadKey(true);
		int x = (int) char.GetNumericValue(info.KeyChar);
		Console.WriteLine(array[x].Invoke(plugin, [1.5, 2.3]));
	}
}