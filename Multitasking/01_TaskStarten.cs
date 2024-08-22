namespace Multitasking;

internal class TaskStarten
{
	static void Main(string[] args)
	{
		Task t = new Task(Run); //Parallele Arbeit anlegen
		t.Start(); //Parallele Arbeit starten

		Task t2 = Task.Factory.StartNew(Run); //ab .NET Framework 4.0

		Task t3 = Task.Run(Run); //ab .NET Framework 4.5

		for (int i = 0; i < 100; i++)
			Console.WriteLine($"Main: {i}");

		//Wenn der Main Thread fertig ist, werden alle Tasks abgebrochen
		//Hier muss der Main Thread aufgehalten werden
		Console.ReadKey();
	}

	static void Run()
	{
		for (int i = 0; i < 100; i++)
            Console.WriteLine($"Run: {i}");
    }
}
