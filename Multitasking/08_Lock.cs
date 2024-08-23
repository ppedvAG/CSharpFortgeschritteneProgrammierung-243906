namespace Multitasking;

public class LockDemo
{
	public static int Counter = 0;

	public static object Lock = new object();

	static void Main(string[] args)
	{
		for (int i = 0; i < 50; i++)
		{
			Task.Run(CounterIncrement);
		}
		Console.ReadKey();
	}

	public static void CounterIncrement()
	{
		for (int i = 0; i < 100; i++)
		{
			//Hier warten alle Tasks, bis der jetztige Task mit dem Block fertig ist
			lock (Lock)
			{
				Counter++;
				Console.WriteLine(Counter);
			}
			//Hier wird das Lock wieder freigegeben

			//Monitor: 1:1 gleicher Code wie Lock, nur mit Methoden
			Monitor.Enter(Lock);
			Counter++;
			Console.WriteLine(Counter);
			Monitor.Exit(Lock); //Exit nicht vergessen

			////////////////////////////////////////////////
			
			Interlocked.Increment(ref Counter); //Lock Block intern
		}
	}
}