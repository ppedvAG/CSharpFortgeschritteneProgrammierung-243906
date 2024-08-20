namespace Delegates;

public class User
{
	static void Main(string[] args)
	{
		Component comp = new();

		comp.Start += Comp_Start; //User kann jetzt selbst definieren, was beim Prozessstart passieren soll
		comp.Progress += Comp_Progress;
		comp.Stop += Comp_Stop;

		comp.StartProcess();
	}

	/// <summary>
	/// Alternative: Log, DB, Webschnittstelle, ...
	/// </summary>
	private static void Comp_Progress(int obj)
	{
        Console.WriteLine($"Fortschritt: {obj}");
    }

	private static void Comp_Start()
	{
        Console.WriteLine("Prozess gestartet");
    }

	private static void Comp_Stop()
	{
		Console.WriteLine("Prozess gestoppt");
	}
}