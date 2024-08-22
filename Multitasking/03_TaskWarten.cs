namespace Multitasking;

public class TaskWarten
{
	static void Main(string[] args)
	{
		Task t1 = Task.Run(Run1);
		Task t2 = Task.Run(Run2);
		Task t3 = Task.Run(Run3);

		//Task.WaitAll(t1, t2, t3); //Warte bis alle gegebenen Tasks fertig sind
		//Console.WriteLine("Alle Tasks fertig");

		int erster = Task.WaitAny(t1, t2, t3);
        Console.WriteLine($"Erster fertiger Task: {erster}");
    }

	static void Run1()
	{
		for (int i = 0; i < 100;  i++)
		{
            Console.WriteLine($"Run1: {i}");
        }
	}

	static void Run2()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Run2: {i}");
		}
	}

	static void Run3()
	{
		for (int i = 0; i < 100; i++)
		{
			Console.WriteLine($"Run3: {i}");
		}
	}
}