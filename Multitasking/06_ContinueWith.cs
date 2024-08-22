namespace Multitasking;

public class ContinueWith
{
    static void Main(string[] args)
    {
		//Demo1();

		//////////////////////////////////////////////////////////

		Task t = new Task(Run);
		t.ContinueWith(vorherigerTask => Console.WriteLine("Erfolg"), TaskContinuationOptions.OnlyOnRanToCompletion);
		t.ContinueWith(vorherigerTask =>
		{
			Console.WriteLine("Fehler");
            Console.WriteLine(vorherigerTask.Exception?.Message);
        }, TaskContinuationOptions.OnlyOnFaulted);
		t.Start();

		Task<int> t2 = new Task<int>(Random.Shared.Next);
		t2.ContinueWith(vorherigerTask => Console.WriteLine($"Ergebnis: {vorherigerTask.Result}"));

		for (int i = 0; i < 200; i++)
			Console.WriteLine($"Main: {i}");

		Console.ReadKey();
	}

	static void Demo1()
	{
		Task t = new Task(Run);
		//Vor dem Start können Tasks noch konfiguriert werden (u.a. mit ContinueWith)
		//ContinueWith: Wenn der Task fertig ist, erstelle einen neuen Task und starte diesen
		t.ContinueWith(vorherigerTask => Console.WriteLine("Fertig"));
		t.Start();

		//"Fertig" wird nun mittendrin erscheinen
		for (int i = 0; i < 200; i++)
			Console.WriteLine($"Main: {i}");

		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Run: {i}");
			switch (Random.Shared.Next() % 3)
			{
				case 0: throw new FieldAccessException();
				case 1: throw new ArgumentException();
				case 2: throw new InvalidDataException();
			}
		}
	}
}